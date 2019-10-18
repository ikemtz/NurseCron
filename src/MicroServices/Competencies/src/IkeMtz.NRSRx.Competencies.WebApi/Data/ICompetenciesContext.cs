using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Competencies.WebApi.Data
{
    public interface ICompetenciesContext : IAuditableDbContext
    {
        DbSet<Competency> Competencies { get; set; }
    }
}