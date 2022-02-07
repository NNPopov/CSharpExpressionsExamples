using ShipData;
using ShipData.Entities;
using System.Diagnostics;

namespace CSharpExpressionsExamples.Filters
{
    public class TestEqualExtensionMethods
    {
        /// <summary>
        /// Testing a method in which the type of the searched value matches the data type of the field being searched.
        /// </summary>
        public void TestGenericMethod()
        {
            var contextBuilder = new ContextInitializer();
            var exampleDbContext = contextBuilder.GetContext();

            var stringEqual = exampleDbContext
                .Ships
                .EqualGenericMethod("Name", "Hiei")
                .ToList();
            Debug.Assert(stringEqual.Count() == 1);


            var stringEqual2 = exampleDbContext
                .Ships
                .EqualGenericMethod("Description", "Class Renown.  Type Cruiser.")
                .ToList();
            Debug.Assert(stringEqual2.Count() == 3);

            var intEqual = exampleDbContext
                .Ships
                .EqualGenericMethod("NumberGuns", 9).ToList();
            Debug.Assert(intEqual.Count() == 6);

            var datetimeEqual = exampleDbContext
                .Ships
                .EqualGenericMethod("LaunchedDate", DateTime.Parse("1941/12/16 00:00:00"))
                .ToList();
            Debug.Assert(datetimeEqual.Count() == 1);

            var guidEqual = exampleDbContext
                .Ships
                .EqualGenericMethod("UniqueId", Guid.Parse("ad92a9b1-3bac-41b2-a9cb-bb0fd42f4b37"))
                .ToList();
            Debug.Assert(guidEqual.Count() == 1);
        }

        /// <summary>
        /// Testing a method where the data type of the lookup is string and does not match the datatype of the lookup field.
        /// </summary>
        public void TestEqualParseMethod()
        {
            var contextBuilder = new ContextInitializer();
            var exampleDbContext = contextBuilder.GetContext();


            // 1. Filters are applied sequentially to the IQueryable.
            IQueryable<Ship> ships = exampleDbContext.Ships;

            _filterParameters
              .ForEach(t => ships = ships.EqualParseMethod(t.PropertyName, t.PropertyValue));

            var searchResult = ships.ToList();

            Debug.Assert(searchResult.Count() == 2);



            // 2. The filter is formed completely for all conditions and then applied to the IQueryable
            IQueryable<Ship> ships2 = exampleDbContext.Ships;

            ships2 = ships2.EqualParseMethod(_filterParameters);

            var searchResult2 = ships2.ToList();

            Debug.Assert(searchResult2.Count() == 2);
        }

        private readonly List<(string PropertyName, string PropertyValue)> _filterParameters = new()
        {
            ("Active", "true"),
            ("CountryId", "2"),
            ("NormalDisplacement", "65000"),
            ("NumberGuns", "0")
        };
    }
}