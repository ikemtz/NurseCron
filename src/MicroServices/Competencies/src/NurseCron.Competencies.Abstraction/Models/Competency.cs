using System;

namespace NurseCron.Competencies.Abstraction.Models
{
  public partial class Competency : ICompetency
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
  }
}
