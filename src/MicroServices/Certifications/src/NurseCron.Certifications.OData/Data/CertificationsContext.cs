using NurseCron.Certifications.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Certifications.OData.Data
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
