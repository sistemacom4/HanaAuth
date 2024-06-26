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
        var employee = await _repository.GetEmployeeByEmail(data.Email);

        if (employee.Any())
        {
            return EmployeeMapper.ToDTO(employee.First());
        }

        throw NotFoundError.Build(HttpStatusCode.NotFound, "Nenhum registro encontrado");

    }
}
