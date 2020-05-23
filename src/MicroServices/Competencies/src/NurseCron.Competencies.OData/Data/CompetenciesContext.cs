using NurseCron.Competencies.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Competencies.OData.Data
{
  public partial class CompetenciesContext : DbContext, ICompetenciesContext
  {
    public CompetenciesContext(DbContextOptions<CompetenciesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Competency> Competencies { get; set; }
  }
}
