using System;
using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Certifications.Abstraction.Models
{
  public partial class Certification : ICertification, IIdentifiable, IAuditable
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
    public DateTimeOffset? ExpiresOnUtc { get; set; }
  }
}
