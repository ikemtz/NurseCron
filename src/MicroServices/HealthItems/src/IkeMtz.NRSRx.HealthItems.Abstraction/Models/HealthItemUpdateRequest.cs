using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace IkeMtz.NRSRx.HealthItems.Models
{
    public class HealthItemUpdateRequest : IIdentifiable
    {
        public HealthItemUpdateRequest()
        { }
        public HealthItemUpdateRequest(HealthItem item)
        {
            Id = item.Id;
            Name = item.Name;
        }

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
