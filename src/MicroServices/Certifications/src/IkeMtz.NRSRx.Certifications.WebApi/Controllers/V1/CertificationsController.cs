using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Certifications.WebApi.Data;
using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Certifications.WebApi.Controllers
{
  [Route("api/v{version:apiVersion}/[controller].{format}"), FormatFilter]
  [ApiVersion(VersionDefinitions.V1_0)]
  [ApiController]
  public class CertificationsController : ControllerBase
  {
    private readonly ICertificationsContext _ctx;
    private readonly IPublisher<Certification, CreatedEvent, Message> _createdPublisher;
    private readonly IPublisher<Certification, UpdatedEvent, Message> _updatedPublisher;
    private readonly IPublisher<Certification, DeletedEvent, Message> _deletedPublisher;

    public CertificationsController(ICertificationsContext ctx,
        IPublisher<Certification, CreatedEvent, Message> createdPublisher,
        IPublisher<Certification, UpdatedEvent, Message> updatedPublisher,
        IPublisher<Certification, DeletedEvent, Message> deletedPublisher)
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
        Name = $"NRSRx {nameof(Certification)} API Microservice Controller",
        Build = this.GetBuildNumber()
      };
      return Ok(result);
    }

    // PUT api/certifications
    [HttpPut]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Certification))]
    [ValidateModel]
    public async Task<ActionResult> Put([FromBody] CertificationInsertRequest value)
    {
      var obj = value.ToCertification();
      _ = await _ctx.Certifications.AddAsync(obj);
      _ = await _ctx.SaveChangesAsync();
      await _createdPublisher.PublishAsync(obj);
      return Ok(obj);
    }

    // POST api/certifications
    [HttpPost]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Certification))]
    [ValidateModel]

    public async Task<ActionResult> Post([FromQuery] Guid id, [FromBody] CertificationUpdateRequest value)
    {
      if (id == Guid.Empty)
      {
        return BadRequest($"Invalid {nameof(Certification)} Id");
      }
      var obj = await _ctx.Certifications.FirstOrDefaultAsync(t => t.Id == id);
      if (null == obj)
      {
        return NotFound($"{nameof(Certification)} Not Found");
      }
      value.UpdateCertification(obj);
      _ = await _ctx.SaveChangesAsync();
      await _updatedPublisher.PublishAsync(obj);
      return Ok(obj);
    }

    // DELETE api/certifications
    [HttpDelete]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Certification))]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
      var obj = await _ctx.Certifications.FirstOrDefaultAsync(t => t.Id == id);
      if (null == obj)
      {
        return NotFound($"{nameof(Certification)} Not Found");
      }
      obj.IsEnabled = false;
      _ = await _ctx.SaveChangesAsync();
      await _deletedPublisher.PublishAsync(obj);
      return Ok(obj);
    }

  }
}
