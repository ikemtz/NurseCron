using System.ComponentModel.DataAnnotations;

namespace IkeMtz.NRSRx.HealthItems.Models
{
    public class HealthItemInsertRequest
    {
        public HealthItemInsertRequest() { }
        public HealthItemInsertRequest(HealthItem value)
        {
            this.Name = value.Name;
        }

        [Required]
        public string Name { get; set; }

        public HealthItem ToHealthItem()
        {
            return new HealthItem()
            {
                Name = this.Name
            };
        }
    }
}
