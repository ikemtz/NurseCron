using NurseCron.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Certifications.WebApi.Data
{
  public interface ICertificationsContext : IAuditableDbContext
  {
    DbSet<Certification> Certifications { get; set; }
  }
}
