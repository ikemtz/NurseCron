using System;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.WebApi.Data;
using IkeMtz.NRSRx.Core.Models.Validation;
using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Competencies.WebApi.V1.Controllers
{
  [Route("api/v{version:apiVersion}/[controller].{format}"), FormatFilter]
  [ApiVersion(VersionDefinitions.V1_0)]
  [ApiController]
  public class CompetenciesController : ControllerBase
  {
    private readonly ICompetenciesContext _ctx;
    private readonly IPublisher<Competency, CreatedEvent, Message> _createdPublisher;
    private readonly IPublisher<Competency, UpdatedEvent, Message> _updatedPublisher;
    private readonly IPublisher<Competency, DeletedEvent, Message> _deletedPublisher;

    public CompetenciesController(ICompetenciesContext ctx,
        IPublisher<Competency, CreatedEvent, Message> createdPublisher,
        IPublisher<Competency, UpdatedEvent, Message> updatedPublisher,
        IPublisher<Competency, DeletedEvent, Message> deletedPublisher)
    {
      _ctx = ctx;
      _createdPublisher = createdPublisher;
      _updatedPublisher = updatedPublisher;
      _deletedPublisher = deletedPublisher;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(PingResult))]
    public ActionResult Get(ApiVersion apiVersion)
    {
      var result = new PingResult(apiVersion)
      {
        Name = $"NRSRx {nameof(Competency)} API Microservice Controller",
        Build = this.GetBuildNumber()
      };
      return Ok(result);
    }

    // PUT api/competencies
    [HttpPut]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Competency))]
    [ValidateModel]

    public async Task<ActionResult> Put([FromBody] CompetencyInsertRequest value)
    {
      var comp = value.ToCompetency();
      _ = await _ctx.Competencies.AddAsync(comp);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _createdPublisher.PublishAsync(comp);
      }
      return Ok(comp);

    }

    // POST api/competencies
    [HttpPost]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Competency))]
    [ValidateModel]

    public async Task<ActionResult> Post([FromQuery][RequiredNonDefault] Guid id, [FromBody] CompetencyUpdateRequest value)
    {

      if (id == Guid.Empty)
      {
        return BadRequest($"Invalid {nameof(Competency)} Id");
      }
      var comp = await _ctx.Competencies.FirstOrDefaultAsync(t => t.Id == id);
      if (null == comp)
      {
        return NotFound($"{nameof(Competency)} Not Found");
      }
      value.UpdateCompetency(comp);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _updatedPublisher.PublishAsync(comp);
      }
      return Ok(comp);
    }

    // DELETE api/v1/competencies
    [HttpDelete]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Competency))]
    public async Task<ActionResult> Delete([FromQuery][RequiredNonDefault] Guid id)
    {
      var comp = await _ctx.Competencies.FirstOrDefaultAsync(t => t.Id == id);
      if (null == comp)
      {
        return NotFound($"{nameof(Competency)} Not Found");
      }
      comp.IsEnabled = false;
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _deletedPublisher.PublishAsync(comp);
      }
      return Ok(comp);
    }
  }
}
