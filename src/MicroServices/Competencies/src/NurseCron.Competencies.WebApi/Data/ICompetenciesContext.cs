using NurseCron.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Competencies.WebApi.Data
{
  public interface ICompetenciesContext : IAuditableDbContext
  {
    DbSet<Competency> Competencies { get; set; }
  }
}
