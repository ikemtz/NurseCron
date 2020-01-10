using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Certifications.WebApi.Data
{
  public interface ICertificationsContext : IAuditableDbContext
  {
    DbSet<Certification> Certifications { get; set; }
  }
}
