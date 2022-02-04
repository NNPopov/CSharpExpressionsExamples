namespace ShipData.Entities;

public class Ship
{
    public int Id { get; set; }

    public Guid UniqueId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime LaunchedDate { get; set; }

    public int NumberGuns { get; set; }

    public int Bore { get; set; }

    public decimal NormalDisplacement { get; set; }

    public int CountryId { get; set; }
    public Country Country { get; set; }
}