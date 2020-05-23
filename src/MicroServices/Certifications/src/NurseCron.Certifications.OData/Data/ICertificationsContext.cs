using NurseCron.Certifications.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Certifications.OData.Data
{
  public interface ICertificationsContext
  {
    DbSet<Certification> Certifications { get; set; }
  }
}
