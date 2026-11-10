namespace PizzaApi.Entities;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HashedPassword { get; set; }

    public int MyRoleId { get; set; }
    public UserRole MyRole { get; set; }
}