using CSharpExpressionsExamples.Filters;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using ShipData;

Log.Logger = new LoggerConfiguration()
        .MinimumLevel
        .Information()
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Information)
        .WriteTo
        .Console(
            outputTemplate: "[{Timestamp:HH:mm:ss}] {NewLine}{NewLine}  {Message:lj}  {NewLine}{NewLine}",
            theme: AnsiConsoleTheme.Code)
        .CreateLogger();

var testEqualExtensionMethods = new TestEqualExtensionMethods();
testEqualExtensionMethods.TestGenericMethod();
testEqualExtensionMethods.TestEqualParseMethod();

Console.WriteLine("Press any key to close");