using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Certifications.OData.Data
{
  public interface ICertificationsContext
  {
    DbSet<Certification> Certifications { get; set; }
  }
}
