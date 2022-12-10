using Serilog.Core;

namespace Day10;

internal class Device
{
    readonly Dictionary<int, Action<int, int>> cycleEvents = new();
    readonly Action<int, int>? everyCycleEvent;
    readonly Logger logger;

    int cycle;
    int x = 1;

    public Device(Logger logger, Action<int, int>? everyCycleEvent = null) {
        this.logger = logger;
        this.everyCycleEvent = everyCycleEvent;
    }

    public void Run(string[] program) {
        foreach (var operation in program) {
            logger.Debug("Operation: {operation}", operation);
            var split = operation.Split(' ');

            switch (split[0]) {
                case "noop":
                    NoOperation();
                    break;
                case "addx":
                    AddX(split[1]);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid operation : {operation}");
            }
        }
    }

    void NoOperation() {
        IncrementCycle();
    }

    void AddX(string value) {
        IncrementCycle();
        IncrementCycle();
        x += int.Parse(value);
    }

    void IncrementCycle() {
        cycle++;
        if (cycleEvents.ContainsKey(cycle)) cycleEvents[cycle](cycle, x);
        everyCycleEvent?.Invoke(cycle, x);

        logger.Debug("Cycle: {cycle}, x: {x}", cycle, x);
    }

    public void Subscribe(int c, Action<int, int> callback) {
        cycleEvents.Add(c, callback);
    }
}