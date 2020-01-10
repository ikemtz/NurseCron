using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace IkeMtz.NRSRx.Competencies.Abstraction.Models
{
  public class CompetencyUpdateRequest : IIdentifiable
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
