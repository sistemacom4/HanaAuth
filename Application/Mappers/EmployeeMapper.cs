using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class EmployeeMapper
{
    public static EmployeeDTO ToDTO(Employee employee)
    {
        return new EmployeeDTO(
            employeeId: employee.EmployeeId,
            firstName: employee.FirstName,
            lastName: employee.LastName,
            email: employee.eMail
            );
    }
}