namespace Auth.Services.Dto;

public record HanaAuthDTO(
    string CompanyDB, 
    string UserName, 
    string Password
);