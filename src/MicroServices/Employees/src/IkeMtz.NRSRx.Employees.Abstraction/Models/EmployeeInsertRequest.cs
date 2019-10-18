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

        public EmployeeInsertRequest(Employee value)
        {
            this.FirstName = value.FirstName;
            this.LastName = value.LastName;
            this.Email = value.Email;
            this.Certifications = value.Certifications;
            this.Competencies = value.Competencies;
            this.HealthItems = value.HealthItems;
            this.BirthDate = value.BirthDate;
            this.HireDate = value.HireDate;
            this.AddressLine1 = value.AddressLine1;
            this.City = value.City;
            this.State = value.State;
            this.Zip = value.Zip;
            this.HomePhone = value.HomePhone;
            this.MobilePhone = value.MobilePhone;
            this.Photo = value.Photo;
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<EmployeeCertification> Certifications { get; set; }

        public ICollection<Competency> Competencies { get; set; }
        public ICollection<HealthItem> HealthItems { get; set; }
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
