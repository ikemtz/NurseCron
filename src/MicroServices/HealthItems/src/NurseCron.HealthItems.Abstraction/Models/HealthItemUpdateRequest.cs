using System;
using System.ComponentModel.DataAnnotations;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;

namespace NurseCron.HealthItems.Models
{
  public class HealthItemUpdateRequest : IIdentifiable
  {
    [RequiredNonDefault]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public void UpdateHealthItem(HealthItem item)
    {
      item.Name = Name;
    }
  }
}
