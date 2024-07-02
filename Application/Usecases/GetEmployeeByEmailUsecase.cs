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

    public async Task<EmployeeDTO> Run(string email)
    {
        var employees = await _repository.GetEmployeeByEmail(email);

        if (employees.Any() && employees.First().eMail == email)
        {
            return EmployeeMapper.ToDTO(employees?.First());
        }

        throw NotFoundError.Build(HttpStatusCode.NotFound, "Nenhum usuario encontrado!");
    }
}
