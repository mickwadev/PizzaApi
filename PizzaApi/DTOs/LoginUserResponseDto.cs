namespace PizzaApi.DTOs;

public record LoginUserResponseDto(string JWT, string RefreshToken);