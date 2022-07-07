using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.WebApi.Data;

namespace NurseCron.Certifications.WebApi.Controllers
{
  [Route("api/v{version:apiVersion}/[controller].{format}"), FormatFilter]
  [ApiVersion(VersionDefinitions.V1_0)]
  [ApiController]
  public class CertificationsController : ControllerBase
  {
    private readonly CertificationsContext _ctx;
    private readonly IPublisher<Certification, CreatedEvent, ServiceBusMessage> _createdPublisher;
    private readonly IPublisher<Certification, UpdatedEvent, ServiceBusMessage> _updatedPublisher;
    private readonly IPublisher<Certification, DeletedEvent, ServiceBusMessage> _deletedPublisher;

    public CertificationsController(CertificationsContext ctx,
        IPublisher<Certification, CreatedEvent, ServiceBusMessage> createdPublisher,
        IPublisher<Certification, UpdatedEvent, ServiceBusMessage> updatedPublisher,
        IPublisher<Certification, DeletedEvent, ServiceBusMessage> deletedPublisher)
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
        Name = $"NurseCRON {nameof(Certification)} API Microservice Controller",
        Build = this.GetBuildNumber()
      };
      return Ok(result);
    }

    // PUT api/certifications
    [HttpPost]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Certification))]
    [ValidateModel]
    public async Task<ActionResult> Post([FromBody] CertificationInsertDto value)
    {
      var obj = value.ToCertification();
      _ = await _ctx.Certifications.AddAsync(obj);
      _ = await _ctx.SaveChangesAsync();
      await _createdPublisher.PublishAsync(obj);
      return Ok(obj);
    }

    // POST api/certifications
    [HttpPut]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Certification))]
    [ValidateModel]

    public async Task<ActionResult> HttpPut([FromQuery] Guid id, [FromBody] CertificationUpdateDto value)
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
