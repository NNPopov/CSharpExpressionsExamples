using Microsoft.EntityFrameworkCore;
using ShipData.Extensions;

namespace ShipData;

public class ContextInitializer
{
    public ExampleDbContext GetContext()
    {
        var exampleDbContext = new ExampleDbContext();

        exampleDbContext.Database.Migrate();

        var dataExists = exampleDbContext.Ships.Any(t => t.Id == 1);

        if (dataExists == false)
        {
            AddCountry(exampleDbContext, "countries.csv");
            AddShips(exampleDbContext, "ships.csv");

            exampleDbContext.SaveChanges();
        }
        Console.Clear();
        return exampleDbContext;
    }

    private static void AddShips(ExampleDbContext exampleDbContext, string dataFilePath)
    {
        var ships =
            File.ReadAllLines(dataFilePath)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToShip();
        exampleDbContext.Ships.AddRange(ships);
    }

    private static void AddCountry(ExampleDbContext exampleDbContext, string dataFilePath)
    {
        var countries = File.ReadAllLines(dataFilePath)
            .Skip(1)
            .Where(l => l.Length > 1)
            .ToCountry();

        exampleDbContext.Countries.AddRange(countries);
    }
}