using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using IkeMtz.NRSRx.Core.EntityFramework;
using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseCron.Employees.Models;
using NurseCron.Employees.WebApi.Data;

namespace NurseCron.Employees.WebApi.Controllers
{
  [Route("api/v{version:apiVersion}/[controller].{format}"), FormatFilter]
  [ApiVersion(VersionDefinitions.V1_0)]
  [ApiController]
  public class EmployeesController : ControllerBase
  {
    private readonly DatabaseContext _ctx;
    private readonly IPublisher<Employee, CreatedEvent, ServiceBusMessage> _createdPublisher;
    private readonly IPublisher<Employee, UpdatedEvent, ServiceBusMessage> _updatedPublisher;
    private readonly IPublisher<Employee, DeletedEvent, ServiceBusMessage> _deletedPublisher;

    public EmployeesController(DatabaseContext ctx,
        IPublisher<Employee, CreatedEvent, ServiceBusMessage> createdPublisher,
        IPublisher<Employee, UpdatedEvent, ServiceBusMessage> updatedPublisher,
        IPublisher<Employee, DeletedEvent, ServiceBusMessage> deletedPublisher)
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
        Name = $"NurseCRON {nameof(Employee)} API Microservice Controller",
        Build = this.GetBuildNumber()
      };
      return Ok(result);
    }

    [HttpPost]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Employee))]
    [ValidateModel]
    public async Task<ActionResult> Post([FromBody] EmployeeInsertDto value)
    {
      var obj = Employee.FromInsertDto(value);
      ContextCollectionSyncer.SyncCollections(_ctx, value.EmployeeCompetencies, obj.EmployeeCompetencies);
      ContextCollectionSyncer.SyncCollections(_ctx, value.EmployeeCertifications, obj.EmployeeCertifications);
      ContextCollectionSyncer.SyncCollections(_ctx, value.EmployeeHealthItems, obj.EmployeeHealthItems);
      _ = _ctx.Employees.Add(obj);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _createdPublisher.PublishAsync(obj);
      }
      return Ok(obj);
    }

    [HttpPut]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Employee))]
    public async Task<ActionResult> Put([FromQuery] Guid id, [FromBody] EmployeeUpdateDto value)
    {
      if (id == Guid.Empty)
      {
        return BadRequest($"Invalid {nameof(Employee)} Id");
      }
      var obj = await _ctx.Employees
          .Include(t => t.EmployeeCertifications)
          .Include(t => t.EmployeeCompetencies)
          .Include(t => t.EmployeeHealthItems)
          .FirstOrDefaultAsync(t => t.Id == id);
      if (null == obj)
      {
        return NotFound($"{nameof(Employee)} Not Found");
      }
      obj.UpdateFromDto(value);
      ContextCollectionSyncer.SyncCollections(_ctx, value.EmployeeCompetencies, obj.EmployeeCompetencies);
      ContextCollectionSyncer.SyncCollections(_ctx, value.EmployeeCertifications, obj.EmployeeCertifications);
      ContextCollectionSyncer.SyncCollections(_ctx, value.EmployeeHealthItems, obj.EmployeeHealthItems);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _updatedPublisher.PublishAsync(obj);
      }
      return Ok(obj);
    }

    [HttpDelete]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Employee))]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
      var obj = await _ctx.Employees.FirstOrDefaultAsync(t => t.Id == id);
      if (null == obj)
      {
        return NotFound($"{nameof(Employee)} Not Found");
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
