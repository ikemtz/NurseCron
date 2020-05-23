using NurseCron.Competencies.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Competencies.OData.Data
{
  public interface ICompetenciesContext
  {
    DbSet<Competency> Competencies { get; set; }
  }
}
