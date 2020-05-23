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
    public ICollection<EmployeeCertification> Certifications { get; set; }

    public ICollection<EmployeeCompetency> Competencies { get; set; }
    public ICollection<EmployeeHealthItem> HealthItems { get; set; }
    public DateTime? FireDate { get; set; }
    public bool IsEnabled { get; set; }

    public void UpdateEmployee(Employee item)
    {
      item.FirstName = this.FirstName;
      item.Email = this.Email;
      item.LastName = this.LastName;
      item.Certifications = this.Certifications;
      item.Competencies = this.Competencies;
      item.HealthItems = this.HealthItems;
      item.BirthDate = this.BirthDate;
      item.HireDate = this.HireDate;
      item.AddressLine1 = this.AddressLine1;
      item.City = this.City;
      item.State = this.State;
      item.Zip = this.Zip;
      item.HomePhone = this.HomePhone;
      item.MobilePhone = this.MobilePhone;
      item.Photo = this.Photo;
      item.FireDate = this.FireDate;
      item.IsEnabled = this.IsEnabled;
    }
  }
}
