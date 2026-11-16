namespace PizzaApi.Entities;

public class SpicyLevel
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public int Lvl { get; set; } = 0;

    public ICollection<Pizza> pizzas { get; set; }
}
