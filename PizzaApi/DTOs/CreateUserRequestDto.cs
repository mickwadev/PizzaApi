namespace PizzaApi.DTOs;

public record CreateUserRequestDto(string Name, int RoleId, string Password);