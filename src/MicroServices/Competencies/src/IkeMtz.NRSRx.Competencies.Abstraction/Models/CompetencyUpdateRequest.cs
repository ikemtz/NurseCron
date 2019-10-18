using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Models.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace IkeMtz.NRSRx.Competencies.Abstraction.Models
{
    public class CompetencyUpdateRequest : IIdentifiable
    {
        public CompetencyUpdateRequest()
        { }
        public CompetencyUpdateRequest(Competency obj)
        {
            Id = obj.Id;
            Name = obj.Name;
        }
        [RequiredNonDefault]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public void UpdateCompetency(Competency item)
        {
            item.Name = Name;
        }
    }
}
