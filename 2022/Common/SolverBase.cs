using System.Reflection;
using Serilog.Core;

namespace AOCCommon;

public abstract class SolverBase : ISolver
{
    protected string[] data;
    protected Logger logger;

    protected SolverBase(string path, Logger logger) {
        this.logger = logger;

        var day = GetType().GetCustomAttribute<DayAttribute>()?.DayNumber;
        if (day == null) throw new Exception("Could not resolve day number");
        var inputFile = $"{path}\\day{day:D2}.txt";
        data = File.ReadAllLines(inputFile);
        // ReSharper disable once VirtualMemberCallInConstructor
        PostConstruct();
    }

    public abstract void Solve1();

    public abstract void Solve2();

    protected virtual void PostConstruct() { }
}