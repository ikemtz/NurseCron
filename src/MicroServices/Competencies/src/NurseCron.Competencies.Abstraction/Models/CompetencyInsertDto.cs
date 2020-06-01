using System.ComponentModel.DataAnnotations;

namespace NurseCron.Competencies.Abstraction.Models
{
  public class CompetencyInsertDto
  {

    [Required]
    public string Name { get; set; }

    public Competency ToCompetency()
    {
      return new Competency()
      {
        Name = this.Name,
        IsEnabled = true,
      };
    }
  }
}
