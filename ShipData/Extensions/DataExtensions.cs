using ShipData.Entities;

namespace ShipData.Extensions;

internal static class DataExtensions
{
    public static IEnumerable<Ship> ToShip(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(',');
            yield return new Ship
            {
                Id = int.Parse(columns[0]),
                UniqueId = Guid.Parse(columns[1]),
                Name = columns[2],
                LaunchedDate = DateTime.Parse(columns[3]),
                Description = columns[4],
                CountryId = int.Parse(columns[5]),
                NumberGuns = int.Parse(columns[6]),
                Bore = int.Parse(columns[7]),
                NormalDisplacement = decimal.Parse(columns[8]),
                Active = bool.Parse(columns[9])
            };
        }
    }

    public static IEnumerable<Country> ToCountry(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(',');
            yield return new Country
            {
                Id = int.Parse(columns[0]),
                Name = columns[1]
            };
        }
    }
}