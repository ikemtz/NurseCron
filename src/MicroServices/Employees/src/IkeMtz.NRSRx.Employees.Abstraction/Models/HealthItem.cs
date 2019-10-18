using System;

namespace IkeMtz.NRSRx.Employees.Models
{
    public partial class HealthItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
