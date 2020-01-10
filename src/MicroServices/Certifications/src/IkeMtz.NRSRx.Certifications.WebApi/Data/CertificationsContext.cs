using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Core.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Certifications.WebApi.Data
{
  public partial class CertificationsContext : AuditableDbContext, ICertificationsContext
  {
    public CertificationsContext(DbContextOptions<CertificationsContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options, httpContextAccessor)
    {
    }

    public virtual DbSet<Certification> Certifications { get; set; }
  }
}
