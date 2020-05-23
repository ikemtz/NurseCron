using System;
using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Employees.Models
{
  public partial class EmployeeCertification : IIdentifiable
  {
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CertificationId { get; set; }
    public string CertificationName { get; set; }
    public DateTimeOffset? ExpiresOnUtc { get; set; }
  }
}
