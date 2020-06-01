using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Events;
using NurseCron.HealthItems.Models;
using NurseCron.HealthItems.WebApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NurseCron.HealthItems.WebApi.Controllers
{
  [Route("api/v{version:apiVersion}/[controller].{format}"), FormatFilter]
  [ApiVersion(VersionDefinitions.V1_0)]
  [ApiController]
  public class HealthItemsController : ControllerBase
  {
    private readonly IHealthItemsContext _ctx;
    private readonly IPublisher<HealthItem, CreatedEvent, Message> _createdPublisher;
    private readonly IPublisher<HealthItem, UpdatedEvent, Message> _updatedPublisher;
    private readonly IPublisher<HealthItem, DeletedEvent, Message> _deletedPublisher;

    public HealthItemsController(IHealthItemsContext ctx,
        IPublisher<HealthItem, CreatedEvent, Message> createdPublisher,
        IPublisher<HealthItem, UpdatedEvent, Message> updatedPublisher,
        IPublisher<HealthItem, DeletedEvent, Message> deletedPublisher)
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
        Name = $"NRSRx {nameof(HealthItem)} API Microservice Controller",
        Build = this.GetBuildNumber()
      };
      return Ok(result);
    }

    // PUT api/competencies
    [HttpPost]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(HealthItem))]
    [ValidateModel]

    public async Task<ActionResult> Post([FromBody] HealthItemInsertDto value)
    {
      var obj = value.ToHealthItem();
      _ = _ctx.HealthItems.Add(obj);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _createdPublisher.PublishAsync(obj);
      }
      return Ok(obj);
    }

    // POST api/competencies
    [HttpPut]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(HealthItem))]
    [ValidateModel]

    public async Task<ActionResult> Put([FromQuery] Guid id, [FromBody] HealthItemUpdateDto value)
    {
      if (id == Guid.Empty)
      {
        return BadRequest($"Invalid {nameof(HealthItem)} Id");
      }
      var obj = await _ctx.HealthItems.FirstOrDefaultAsync(t => t.Id == id);
      if (null == obj)
      {
        return NotFound($"{nameof(HealthItem)} Not Found");
      }
      value.UpdateHealthItem(obj);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _updatedPublisher.PublishAsync(obj);
      }
      return Ok(obj);
    }

    // DELETE api/certifications
    [HttpDelete]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(HealthItem))]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
      var obj = await _ctx.HealthItems.FirstOrDefaultAsync(t => t.Id == id);
      if (null == obj)
      {
        return NotFound($"{nameof(HealthItem)} Not Found");
      }
      obj.IsEnabled = false;
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _deletedPublisher.PublishAsync(obj);
      }
      return Ok(obj);
    }

  }
}
