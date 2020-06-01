using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Employees.Models
{
  public partial class Employee
  : IIdentifiable, IAuditable, IEmployee
  {
    public Employee()
    {
      EmployeeCertifications = new HashSet<EmployeeCertification>();
      EmployeeCompetencies = new HashSet<EmployeeCompetency>();
      EmployeeHealthItems = new HashSet<EmployeeHealthItem>();
    }

    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(250)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(250)]
    public string FirstName { get; set; }
    public DateTime? BirthDate { get; set; }
    [MaxLength(10)]
    public string MobilePhone { get; set; }
    [MaxLength(10)]
    public string HomePhone { get; set; }
    [MaxLength(4000)]
    public string Photo { get; set; }
    [Required]
    [MaxLength(250)]
    public string Email { get; set; }
    [MaxLength(250)]
    public string AddressLine1 { get; set; }
    [MaxLength(250)]
    public string AddressLine2 { get; set; }
    [MaxLength(150)]
    public string City { get; set; }
    [MaxLength(2)]
    public string State { get; set; }
    [MaxLength(10)]
    public string Zip { get; set; }
    [Required]
    public bool IsEnabled { get; set; }
    [Required]
    public DateTime HireDate { get; set; }
    public DateTime? FireDate { get; set; }
    public decimal? TotalHoursOfService { get; set; }
    [Required]
    public int CertificationCount { get; set; }
    [Required]
    public int CompetencyCount { get; set; }
    [Required]
    public int HealthItemCount { get; set; }
    [Required]
    [MaxLength(250)]
    public string CreatedBy { get; set; }
    [MaxLength(250)]
    public string UpdatedBy { get; set; }
    [Required]
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
    public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; }
    public virtual ICollection<EmployeeCompetency> EmployeeCompetencies { get; }
    public virtual ICollection<EmployeeHealthItem> EmployeeHealthItems { get; }
  }
}
