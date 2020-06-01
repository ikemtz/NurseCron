using System.ComponentModel.DataAnnotations;

namespace NurseCron.Certifications.Abstraction.Models
{
  public class CertificationInsertDto
  {
    [Required]
    public string Name { get; set; }

    public Certification ToCertification()
    {
      return new Certification()
      {
        Name = this.Name,
        IsEnabled = true,
      };
    }
  }
}
