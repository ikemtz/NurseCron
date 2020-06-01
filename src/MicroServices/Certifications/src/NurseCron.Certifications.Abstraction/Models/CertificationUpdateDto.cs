using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace NurseCron.Certifications.Abstraction.Models
{
  public class CertificationUpdateDto : IIdentifiable
  {
    [RequiredNonDefault]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }

    public void UpdateCertification(Certification cert)
    {
      cert.Name = Name;
    }
  }
}
