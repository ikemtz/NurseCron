using Microsoft.EntityFrameworkCore;
using NurseCron.Employees.Models;

namespace NurseCron.Employees.OData.Data
{
  public partial class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCertification> EmployeeCertifications { get; set; }

    public virtual DbSet<EmployeeCompetency> EmployeeCompetencies { get; set; }

    public virtual DbSet<EmployeeHealthItem> EmployeeHealthItems { get; set; }
  }
}
