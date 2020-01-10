using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkeMtz.NRSRx.Employees.Models
{
  public class EmployeeInsertRequest
  {
    public EmployeeInsertRequest()
    {
    }

    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public ICollection<EmployeeCertification> Certifications { get; set; }

    public ICollection<EmployeeCompetency> Competencies { get; set; }
    public ICollection<EmployeeHealthItem> HealthItems { get; set; }
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
      return new Employee()
      {
        FirstName = this.FirstName,
        Email = this.Email,
        LastName = this.LastName,
        Certifications = this.Certifications,
        Competencies = this.Competencies,
        HealthItems = this.HealthItems,
        BirthDate = this.BirthDate,
        HireDate = this.HireDate ?? DateTime.UtcNow,
        AddressLine1 = this.AddressLine1,
        City = this.City,
        State = this.State,
        Zip = this.Zip,
        HomePhone = this.HomePhone,
        MobilePhone = this.MobilePhone,
        Photo = this.Photo,
      };
    }
  }
}
