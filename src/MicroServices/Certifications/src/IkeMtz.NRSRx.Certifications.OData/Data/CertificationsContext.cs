using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Certifications.OData.Data
{
  public partial class CertificationsContext : DbContext, ICertificationsContext
  {
    public CertificationsContext(DbContextOptions<CertificationsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Certification> Certifications { get; set; }
  }
}
