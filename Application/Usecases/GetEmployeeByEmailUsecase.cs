using Application.DTOs;
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
        return EmployeeMapper.ToDTO(employee.First());
    }
}
