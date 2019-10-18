using System.ComponentModel.DataAnnotations;

namespace IkeMtz.NRSRx.Competencies.Abstraction.Models
{
    public class CompetencyInsertRequest
    {
        public CompetencyInsertRequest()
        {
        }

        public CompetencyInsertRequest(Competency value)
        {
            this.Name = value.Name;
        }
        [Required]
        public string Name { get; set; }

        public Competency ToCompetency()
        {
            return new Competency()
            {
                Name = this.Name
            };
        }
    }
}
