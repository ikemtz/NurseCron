using IkeMtz.NRSRx.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.Employees.OData.Data
{
    public interface IEmployeesContext
    {
        DbSet<Employee> Employees { get; set; }
    }
}