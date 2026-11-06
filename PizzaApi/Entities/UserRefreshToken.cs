namespace PizzaApi.Entities;

public class UserRefreshToken
{
    public int Id { get; set; }
    public string HashedToken { get; set; }
    public bool Revoked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string DeviceInfo { get; set; }
}
