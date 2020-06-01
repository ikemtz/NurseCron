using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace NurseCron.Competencies.Abstraction.Models
{
  public class CompetencyUpdateDto : IIdentifiable
  {
    [RequiredNonDefault]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }

    public void UpdateCompetency(Competency item)
    {
      item.Name = Name;
    }
  }
}
