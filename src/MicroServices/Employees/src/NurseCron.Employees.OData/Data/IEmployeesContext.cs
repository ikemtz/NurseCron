using NurseCron.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Employees.OData.Data
{
  public interface IEmployeesContext
  {
    DbSet<Employee> Employees { get; set; }

     DbSet<EmployeeCertification> EmployeeCertifications { get; set; }
     
     DbSet<EmployeeCompetency> EmployeeCompetencies { get; set; }
     
     DbSet<EmployeeHealthItem> EmployeeHealthItems { get; set; }
  }
}
