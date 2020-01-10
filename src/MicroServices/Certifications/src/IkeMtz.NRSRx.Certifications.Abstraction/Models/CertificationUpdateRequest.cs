using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace IkeMtz.NRSRx.Certifications.Abstraction.Models
{
  public class CertificationUpdateRequest : IIdentifiable
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
