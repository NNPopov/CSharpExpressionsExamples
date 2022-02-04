namespace ShipData.Entities;

public class Country
{
    public Country()
    {
        Ships = new HashSet<Ship>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Ship> Ships { get; set; }
}