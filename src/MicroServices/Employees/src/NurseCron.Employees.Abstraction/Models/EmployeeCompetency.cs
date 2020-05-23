using System;
using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Employees.Models
{
  public partial class EmployeeCompetency : IIdentifiable
  {
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CompetencyId { get; set; }
    public string CompetencyName { get; set; }
    public DateTimeOffset? ExpiresOnUtc { get; set; }
    public bool IsEnabled { get; set; }
  }
}
