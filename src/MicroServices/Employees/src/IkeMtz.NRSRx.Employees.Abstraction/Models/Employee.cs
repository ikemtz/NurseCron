using System;
using System.Collections.Generic;

namespace IkeMtz.NRSRx.Employees.Models
{
    public partial class Employee : IEmployee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Photo { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? FireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? UpdatedOnUtc { get; set; }
        public virtual ICollection<EmployeeCertification> Certifications { get; set; }
        public virtual ICollection<EmployeeCompetency> Competencies { get; set; }
        public virtual ICollection<EmployeeHealthItem> HealthItems { get; set; }
        public Guid Id { get; set; }
    }
}
