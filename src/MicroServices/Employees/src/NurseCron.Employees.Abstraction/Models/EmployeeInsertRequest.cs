using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Employees.Models
{
  public class EmployeeInsertRequest
  {
    public EmployeeInsertRequest()
    {
      EmployeeCertifications = new HashSet<EmployeeCertification>();
      EmployeeCompetencies = new HashSet<EmployeeCompetency>();
      EmployeeHealthItems = new HashSet<EmployeeHealthItem>();
    }
    public Guid? Id { get; set; }

    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public ICollection<EmployeeCertification> EmployeeCertifications { get; }
    public ICollection<EmployeeCompetency> EmployeeCompetencies { get; }
    public ICollection<EmployeeHealthItem> EmployeeHealthItems { get; }
    public DateTime? HireDate { get; set; }
    public string AddressLine1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string HomePhone { get; set; }
    public string MobilePhone { get; set; }
    public string Photo { get; set; }
    public DateTime? BirthDate { get; set; }
    public Employee ToEmployee()
    {
      var employee = SimpleMapper<EmployeeInsertRequest, Employee>.Instance.Convert(this);
      employee.Id = this.Id.GetValueOrDefault() != default ? this.Id.Value : Guid.NewGuid();
      return employee;
    }
  }
}
