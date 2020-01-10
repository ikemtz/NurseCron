using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Core.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Competencies.WebApi.Data
{
  public partial class CompetenciesContext : AuditableDbContext, ICompetenciesContext
  {
    public CompetenciesContext(DbContextOptions<CompetenciesContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options, httpContextAccessor)
    {
    }

    public virtual DbSet<Competency> Competencies { get; set; }
  }
}
