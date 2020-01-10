using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace IkeMtz.NRSRx.Certifications.Abstraction.Models
{
  public class CertificationUpdateRequest : IIdentifiable
  {
    public CertificationUpdateRequest()
    { }
    public CertificationUpdateRequest(Certification cert)
    {
      Id = cert.Id;
      Name = cert.Name;
    }

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
