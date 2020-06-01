using System;
using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Employees.Models
{
  public partial class Employee : ICalculateable
  {
    public void CalculateValues()
    {
      this.CompetencyCount = EmployeeCompetencies.Count;
      this.CertificationCount = EmployeeCertifications.Count;
      this.HealthItemCount = EmployeeHealthItems.Count;
    }
    public static Employee FromInsertDto(EmployeeInsertDto dto)
    {
      var employee = SimpleMapper<EmployeeInsertDto, Employee>.Instance.Convert(dto);
      employee.Id = dto.Id.GetValueOrDefault() != default ? dto.Id.Value : Guid.NewGuid();
      employee.IsEnabled = true;
      return employee;
    }

    public void UpdateFromDto(EmployeeUpdateDto dto)
    {
      SimpleMapper<EmployeeUpdateDto, Employee>.Instance.ApplyChanges(dto, this);
    }
  }
}
