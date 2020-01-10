using IkeMtz.NRSRx.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Employees.OData.Data
{
  public partial class EmployeesContext : DbContext, IEmployeesContext
  {
    public EmployeesContext(DbContextOptions<EmployeesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCertification> EmployeeCertifications { get; set; }
  }
}
