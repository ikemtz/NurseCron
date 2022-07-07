using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Units.Abstraction
{
  public partial class Building : IIdentifiable, IAuditable
  {
    [Required] public Guid Id { get; set; }
    [Required][MaxLength(50)] public string Name { get; set; }
    [Required][MaxLength(150)] public string SiteName { get; set; }
    [Required][MaxLength(250)] public string AddressLine1 { get; set; }
    [MaxLength(250)] public string? AddressLine2 { get; set; }
    [Required][MaxLength(250)] public string CityOrMunicipality { get; set; }
    [Required][MaxLength(50)] public string StateOrProvidence { get; set; }
    [Required][MaxLength(50)] public string PostalCode { get; set; }
    [MaxLength(3)] public string? Country { get; set; }
    public byte[]? GpsData { get; set; }
    [MaxLength(250)] public string CreatedBy { get; set; }
    [MaxLength(250)] public string? UpdatedBy { get; set; }
    [MaxLength(250)] public string? DeletedBy { get; set; }
    [Required] public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
    public DateTimeOffset? DeletedOnUtc { get; set; }
  }
}
