using System.ComponentModel.DataAnnotations;

namespace NurseCron.HealthItems.Models
{
  public class HealthItemInsertDto
  {
    [Required]
    public string Name { get; set; }

    public HealthItem ToHealthItem()
    {
      return new HealthItem()
      {
        Name = this.Name,
        IsEnabled = true,
      };
    }
  }
}
