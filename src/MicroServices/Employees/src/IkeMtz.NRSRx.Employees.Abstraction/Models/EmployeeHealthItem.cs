using System;

namespace IkeMtz.NRSRx.Employees.Models
{
  public partial class EmployeeHealthItem
  {
    public int Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid HealthItemId { get; set; }
    public string HealthItemName { get; set; }
    public DateTimeOffset? ExpiresOnUtc { get; set; }
    public bool IsEnabled { get; set; }
  }
}
