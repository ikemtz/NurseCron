using NurseCron.Units.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Units.OData.Data
{
  public interface IDatabaseContext
  {
    DbSet<Building> Buildings { get; set; }
    DbSet<Unit> Units { get; set; }
  }
}

