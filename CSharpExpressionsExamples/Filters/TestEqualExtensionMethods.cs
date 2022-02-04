using ShipData;
using System.Diagnostics;

namespace CSharpExpressionsExamples.Filters
{
    public class TestEqualExtensionMethods
    {
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

        public void TestEqualConvertMethod()
        {
            var contextBuilder = new ContextInitializer();
            var exampleDbContext = contextBuilder.GetContext();

            var intEqual = exampleDbContext
                .Ships
                .EqualParseMethod("Id", "1").ToList();
            Debug.Assert(intEqual.Count() == 1);

            var boolEqual = exampleDbContext
                .Ships
                .EqualParseMethod("Active", "true")
                .ToList();
            Debug.Assert(boolEqual.Count() == 1);

            var datetimeEqual = exampleDbContext
                .Ships
                .EqualParseMethod("LaunchedDate", "1941/12/16 00:00:00")
                .ToList();
            Debug.Assert(datetimeEqual.Count() == 1);

            var guidEqual = exampleDbContext
                .Ships
                .EqualParseMethod("UniqueId", "8faccb4b-e634-4522-8392-ffc4486c4cfb")
                .ToList();
            Debug.Assert(guidEqual.Count() == 1);
        }
    }
}
