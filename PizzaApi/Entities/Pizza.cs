namespace PizzaApi.Entities;

public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int Stars { get; set; } = 5;

    public decimal Price { get; set; } = 0;
    public int SpicyLevelId { get; set; } = 1;
    public SpicyLevel spicy { get; set; }
}
