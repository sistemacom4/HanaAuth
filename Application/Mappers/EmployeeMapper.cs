using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class EmployeeMapper
{
    public static EmployeeDTO ToDTO(Employee employee)
    {
        return new EmployeeDTO
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.eMail
        };
    }
}