using System;

namespace IkeMtz.NRSRx.Employees.Models
{
    public partial class Competency
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
