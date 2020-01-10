using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Competencies.OData.Data
{
  public interface ICompetenciesContext
  {
    DbSet<Competency> Competencies { get; set; }
  }
}
