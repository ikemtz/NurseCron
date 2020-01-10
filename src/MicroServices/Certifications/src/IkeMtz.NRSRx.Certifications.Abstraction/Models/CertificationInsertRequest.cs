using System.ComponentModel.DataAnnotations;

namespace IkeMtz.NRSRx.Certifications.Abstraction.Models
{
  public class CertificationInsertRequest
  {
    public CertificationInsertRequest()
    { }

    public CertificationInsertRequest(Certification value)
    {
      this.Name = value.Name;
    }

    [Required]
    public string Name { get; set; }

    public Certification ToCertification()
    {
      return new Certification()
      {
        Name = this.Name
      };
    }
  }
}
