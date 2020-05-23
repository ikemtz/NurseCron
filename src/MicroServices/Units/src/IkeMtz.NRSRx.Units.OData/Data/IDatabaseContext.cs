using IkeMtz.NRSRx.Units.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Units.OData.Data
{
  public interface IDatabaseContext
  {
    DbSet<Building> Buildings { get; set; }
    DbSet<Unit> Units { get; set; }
  }
}

