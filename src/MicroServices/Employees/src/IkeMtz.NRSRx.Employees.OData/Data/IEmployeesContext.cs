using IkeMtz.NRSRx.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Employees.OData.Data
{
  public interface IEmployeesContext
  {
    DbSet<Employee> Employees { get; set; }

     DbSet<EmployeeCertification> EmployeeCertifications { get; set; }
     
     DbSet<EmployeeCompetency> EmployeeCompetencies { get; set; }
     
     DbSet<EmployeeHealthItem> EmployeeHealthItems { get; set; }
  }
}
