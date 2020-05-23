using NurseCron.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Employees.OData.Data
{
  public partial class EmployeesContext : DbContext, IEmployeesContext
  {
    public EmployeesContext(DbContextOptions<EmployeesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCertification> EmployeeCertifications { get; set; }

    public virtual DbSet<EmployeeCompetency> EmployeeCompetencies { get; set; }

    public virtual DbSet<EmployeeHealthItem> EmployeeHealthItems { get; set; }
  }
}
