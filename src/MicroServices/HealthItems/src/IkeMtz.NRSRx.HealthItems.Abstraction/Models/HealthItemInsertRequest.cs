using System.ComponentModel.DataAnnotations;

namespace IkeMtz.NRSRx.HealthItems.Models
{
  public class HealthItemInsertRequest
  {
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
