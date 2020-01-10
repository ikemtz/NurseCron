using System;

namespace IkeMtz.NRSRx.Employees.Models
{
  public partial class EmployeeCompetency
  {
    public int Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CompetencyId { get; set; }
    public string CompetencyName { get; set; }
    public DateTimeOffset? ExpiresOnUtc { get; set; }
    public bool IsEnabled { get; set; }
  }
}
