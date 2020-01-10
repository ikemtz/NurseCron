using System;

namespace IkeMtz.NRSRx.Employees.Models
{
  public partial class EmployeeCertification
  {
    public int Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CertificationId { get; set; }
    public string CertificationName { get; set; }
    public DateTimeOffset? ExpiresOnUtc { get; set; }
  }
}
