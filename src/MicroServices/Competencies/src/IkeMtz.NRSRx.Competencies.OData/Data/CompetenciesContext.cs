using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Competencies.OData.Data
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
