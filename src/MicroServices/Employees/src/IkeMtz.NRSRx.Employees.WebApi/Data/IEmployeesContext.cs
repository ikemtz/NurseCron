using IkeMtz.NRSRx.Core.EntityFramework;
using IkeMtz.NRSRx.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Employees.WebApi.Data
{
  public interface IEmployeesContext : IAuditableDbContext
  {
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeCertification> EmployeeCertifications { get; set; }

    DbSet<EmployeeCompetency> EmployeeCompetencies { get; set; }

    DbSet<EmployeeHealthItem> EmployeeHealthItems { get; set; }
  }
}
