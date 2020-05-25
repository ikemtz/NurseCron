using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace NurseCron.Employees.Models
{
  public class EmployeeUpdateRequest : IIdentifiable
  {
    [RequiredNonDefault]
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [RequiredNonDefault]
    public DateTime HireDate { get; set; }
    public string AddressLine1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string HomePhone { get; set; }
    public string MobilePhone { get; set; }
    public string Photo { get; set; }
    public DateTime? BirthDate { get; set; }
    public ICollection<EmployeeCertification> EmployeeCertifications { get; set; }

    public ICollection<EmployeeCompetency> EmployeeCompetencies { get; set; }
    public ICollection<EmployeeHealthItem> EmployeeHealthItems { get; set; }
    public DateTime? FireDate { get; set; }
    public bool IsEnabled { get; set; }

    public void UpdateEmployee(Employee item)
    {
      SimpleMapper<EmployeeUpdateRequest, Employee>.Instance.ApplyChanges(this, item);
    }
  }
}
