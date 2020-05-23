using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace NurseCron.Units.Abstraction
{
  public partial class Unit : IIdentifiable
  {
    [RequiredNonDefault]
    public Guid Id { get; set; }
    [Required]
    public Guid BuildingId { get; set; }
    [Required]
    [MaxLength(102)]
    public string Name { get; set; }
    public string del { get; set; }
    [Required]
    public decimal RoomCount { get; set; }
    [Required]
    [MaxLength(250)]
    public string CreatedBy { get; set; }
    [MaxLength(250)]
    public string UpdatedBy { get; set; }
    [MaxLength(250)]
    public string DeletedBy { get; set; }
    [Required]
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
    public DateTimeOffset? DeletedOnUtc { get; set; }
    public virtual Building Building { get; set; }
  }
}
