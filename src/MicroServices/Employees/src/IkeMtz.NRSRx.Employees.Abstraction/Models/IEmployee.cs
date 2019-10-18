using IkeMtz.NRSRx.Core.Models;
using System.Collections.Generic;

namespace IkeMtz.NRSRx.Employees.Models
{
    public interface IEmployee : IAuditable, IIdentifiable, IDisableable
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        string MobilePhone { get; set; }
        string HomePhone { get; set; }
        string Email { get; set; }
        string AddressLine1 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
        string Photo { get; set; }
        ICollection<EmployeeCertification> Certifications { get; set; }
        ICollection<Competency> Competencies { get; set; }
        ICollection<HealthItem> HealthItems { get; set; }
    }
}
