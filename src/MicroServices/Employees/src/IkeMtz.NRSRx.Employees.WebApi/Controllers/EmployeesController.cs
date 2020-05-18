using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.WebApi.Data;
using IkeMtz.NRSRx.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.Employees.WebApi.Controllers
{
  [Route("api/v{version:apiVersion}/[controller].{format}"), FormatFilter]
  [ApiVersion(VersionDefinitions.V1_0)]
  [ApiController]
  public class EmployeesController : ControllerBase
  {
    private readonly IEmployeesContext _ctx;
    private readonly IPublisher<Employee, CreatedEvent, Message> _createdPublisher;
    private readonly IPublisher<Employee, UpdatedEvent, Message> _updatedPublisher;
    private readonly IPublisher<Employee, DeletedEvent, Message> _deletedPublisher;

    public EmployeesController(IEmployeesContext ctx,
        IPublisher<Employee, CreatedEvent, Message> createdPublisher,
        IPublisher<Employee, UpdatedEvent, Message> updatedPublisher,
        IPublisher<Employee, DeletedEvent, Message> deletedPublisher)
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
        Name = $"NRSRx {nameof(Employee)} API Microservice Controller",
        Build = this.GetBuildNumber()
      };
      return Ok(result);
    }

    // PUT api/competencies
    [HttpPut]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Employee))]
    [ValidateModel]
    public async Task<ActionResult> Put([FromBody] EmployeeInsertRequest value)
    {
      var obj = value.ToEmployee();
      _ = _ctx.Employees.Add(obj);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _createdPublisher.PublishAsync(obj);
      }
      return Ok(obj);
    }

    // POST api/competencies
    [HttpPost]
    [Authorize()]
    [ProducesResponseType(200, Type = typeof(Employee))]
    public async Task<ActionResult> Post([FromQuery] Guid id, [FromBody] EmployeeUpdateRequest value)
    {
      if (id == Guid.Empty)
      {
        return BadRequest($"Invalid {nameof(Employee)} Id");
      }
      var obj = await _ctx.Employees
          .Include(t => t.Certifications)
          .FirstOrDefaultAsync(t => t.Id == id);
      if (null == obj)
      {
        return NotFound($"{nameof(Employee)} Not Found");
      }
      value.UpdateEmployee(obj);
      if (0 < await _ctx.SaveChangesAsync())
      {
        await _updatedPublisher.PublishAsync(obj);
      }
      return Ok(obj);
    }

    // DELETE api/certifications
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
