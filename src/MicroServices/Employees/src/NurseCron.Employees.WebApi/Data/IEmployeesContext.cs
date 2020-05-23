using IkeMtz.NRSRx.Core.EntityFramework;
using NurseCron.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Employees.WebApi.Data
{
  public interface IEmployeesContext : IAuditableDbContext
  {
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeCertification> EmployeeCertifications { get; set; }

    DbSet<EmployeeCompetency> EmployeeCompetencies { get; set; }

    DbSet<EmployeeHealthItem> EmployeeHealthItems { get; set; }
  }
}
