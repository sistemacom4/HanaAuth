using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployeeByEmail(string email);
}