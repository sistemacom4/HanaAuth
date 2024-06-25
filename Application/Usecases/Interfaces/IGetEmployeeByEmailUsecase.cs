using Application.DTOs;
using Domain.Entities;

namespace Application.Usecases.Interfaces;

public interface IGetEmployeeByEmailUsecase
{
    public Task<EmployeeDTO> Run(LoginDTO data);
}