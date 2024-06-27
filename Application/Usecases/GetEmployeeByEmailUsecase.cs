using System.Net;
using Application.DTOs;
using Application.Errors;
using Application.Mappers;
using Application.Usecases.Interfaces;
using Domain.Interfaces;

namespace Application.Usecases;

public class GetEmployeeByEmailUsecase : IGetEmployeeByEmailUsecase
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByEmailUsecase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDTO> Run(LoginDTO data)
    {
        var employees = await _repository.GetEmployeeByEmail(data.Email);

        if (employees.Any())
        {
            var employee = employees.First();
            if (employee.Pager == data.Password)
            {
                return EmployeeMapper.ToDTO(employees.First());

            }
            throw UnauthorizedError.Build(HttpStatusCode.Unauthorized, "Credenciais incorretas!");

        }

        throw NotFoundError.Build(HttpStatusCode.NotFound, "Nenhum usuario encontrado!");
    }
}
