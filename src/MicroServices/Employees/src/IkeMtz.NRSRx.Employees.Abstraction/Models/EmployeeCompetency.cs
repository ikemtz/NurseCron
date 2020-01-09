using System;

namespace IkeMtz.NRSRx.Employees.Models
{
  public partial class EmployeeCompetency
  {
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CompetencyId { get; set; }
    public string CompetencyName { get; set; }
    public bool IsEnabled { get; set; }
  }
}
