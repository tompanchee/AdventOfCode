using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AOCCommon;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AOCApp;

internal sealed class App : Command<App.Options>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] Options settings) {
        var loggerConfig = new LoggerConfiguration()
            .WriteTo.Console();
        if (settings.Debug) loggerConfig.MinimumLevel.Debug();
        else loggerConfig.MinimumLevel.Information();
        Log.Logger = loggerConfig.CreateLogger();

        var solvers = ResolveSolvers();

        AnsiConsole.Clear();
        Header.Render();

        var solverCtx = AnsiConsole.Prompt(
            new SelectionPrompt<SolverModel>()
                .Title("Select day to solve")
                .PageSize(25)
                .AddChoices(solvers)
        );

        var solver = Activator.CreateInstance(solverCtx.Solver, settings.InputPath, Log.Logger) as ISolver;

        Log.Information("Solving problem 1");
        var stopwatch = Stopwatch.StartNew();
        solver?.Solve1();
        stopwatch.Stop();
        Log.Information("Solved problem 1 in {0}ms", stopwatch.ElapsedMilliseconds);

        Log.Information("Solving problem 2");
        stopwatch.Restart();
        solver?.Solve2();
        stopwatch.Stop();
        Log.Information("Solved problem 2 in {0}ms", stopwatch.ElapsedMilliseconds);

        return 0;
    }

    static IEnumerable<SolverModel> ResolveSolvers() {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var assemblies = Directory.GetFiles(path!, "*.dll").Select(Assembly.LoadFile);

        var types = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && t.GetInterfaces().Contains(typeof(ISolver)));

        var result = new List<SolverModel>();
        foreach (var t in types.Where(t => t != null)) {
            var a = t.GetCustomAttributes<DayAttribute>().FirstOrDefault();
            if (a != null) {
                Log.Debug("Adding solver for day {0}", a.DayNumber);
                result.Add(new SolverModel(a.DayNumber, a.Description, t));
            }
        }

        return result.OrderByDescending(s => s.Day);
    }

    record SolverModel(int Day, string Description, Type Solver)
    {
        public override string ToString() {
            return $"Day {Day} - {Description}";
        }
    }

    public sealed class Options : CommandSettings
    {
        [CommandOption("-i|--input")]
        [DefaultValue(".")]
        public string InputPath { get; set; } = ".";

        [CommandOption("-d|--debug")]
        public bool Debug { get; set; } = false;
    }
}