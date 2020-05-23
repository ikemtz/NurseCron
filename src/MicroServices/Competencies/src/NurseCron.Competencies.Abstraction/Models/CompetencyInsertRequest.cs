using System.ComponentModel.DataAnnotations;

namespace NurseCron.Competencies.Abstraction.Models
{
  public class CompetencyInsertRequest
  {

    [Required]
    public string Name { get; set; }

    public Competency ToCompetency()
    {
      return new Competency()
      {
        Name = this.Name
      };
    }
  }
}
