using Microsoft.EntityFrameworkCore;
using NurseCron.Services.Schedules.Abstraction;

namespace NurseCron.Services.Schedules.OData.Data
{
  public interface IDatabaseContext
  {
    DbSet<Schedule> Schedules { get; set; }
  }
}

