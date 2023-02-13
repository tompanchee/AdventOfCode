using AOCCommon;
using Serilog.Core;

namespace Day16;

[Day(16, "Proboscidea Volcanium")]
internal class Solver : SolverBase
{
    readonly Dictionary<string, int> flowRates = new();
    readonly List<string> interestingValves = new();
    readonly Dictionary<string, Valve> valves = new();
    int[,]? pathLengths;
    List<string>? valveIndices;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Turning valves on alone...");

        VisitValve("AA", new List<string> {"AA"}, 30, 0);
        var maxRate = flowRates.Values.Max();

        logger.Information("Maximum flow rate achieved is {maxRate}", maxRate);
    }

    public override void Solve2() {
        logger.Information("Turning valves on with an elephant...");

        flowRates.Clear();
        VisitValve("AA", new List<string> {"AA"}, 26, 0);

        var maxRate = 0;
        foreach (var fr1 in flowRates)
        foreach (var fr2 in flowRates) {
            var visited1 = fr1.Key.Split(',').ToList();
            visited1.Remove("AA");
            var visited2 = fr2.Key.Split(',').ToList();
            if (visited1.Intersect(visited2).Any()) continue;

            if (fr1.Value + fr2.Value > maxRate) maxRate = fr1.Value + fr2.Value;
        }

        logger.Information("Maximum flow rate achieved is {maxRate}", maxRate);
    }

    void VisitValve(string current, List<string> visited, int timeLeft, int flow) {
        var k = VisitedKey();
        foreach (var valve in interestingValves) {
            if (visited.Contains(valve)) continue;

            var currentIdx = valveIndices!.IndexOf(current);
            var valveIdx = valveIndices.IndexOf(valve);

            var durationOfWalkAndTurnOnValve = pathLengths![currentIdx, valveIdx] + 1;
            if (durationOfWalkAndTurnOnValve > timeLeft) continue;
            var time = timeLeft - durationOfWalkAndTurnOnValve;

            visited.Add(valve);
            var key = VisitedKey();
            var totalFlow = flow + time * valves[valve].FlowRate;
            if (flowRates.ContainsKey(key)) {
                if (flowRates[key] < totalFlow) flowRates[key] = totalFlow;
            } else {
                flowRates.Add(key, totalFlow);
            }

            VisitValve(valve, new List<string>(visited), timeLeft - durationOfWalkAndTurnOnValve, totalFlow);
            visited.Remove(valve);
        }

        string VisitedKey() {
            return string.Join(',', visited.OrderBy(x => x));
        }
    }

    protected override void PostConstruct() {
        logger.Information("Scanning cave...");

        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;
            var valve = Valve.Parse(row);
            valves.Add(valve.Id, valve);
        }

        valveIndices = valves.Keys.ToList();

        // Floyd-Warshall algorithm to solve path lengths between valves
        var count = valveIndices.Count;
        pathLengths = new int[count, count];
        // Initialize
        for (var i = 0; i < count; i++)
        for (var j = i; j < count; j++) {
            if (i == j) continue;
            var valvei = valveIndices[i];
            var valvej = valveIndices[j];

            if (valves[valvei].Neighbours.Contains(valvej)) {
                pathLengths[i, j]++;
                pathLengths[j, i]++;
            } else {
                pathLengths[i, j] = 9999;
                pathLengths[j, i] = 9999;
            }
        }

        // Calculate lengths
        for (var k = 0; k < count; k++)
        for (var i = 0; i < count; i++)
        for (var j = 0; j < count; j++)
            if (pathLengths[i, j] > pathLengths[i, k] + pathLengths[k, j])
                pathLengths[i, j] = pathLengths[i, k] + pathLengths[k, j];

        // Get Interesting valves (AA and the ones with a non zero flow)
        interestingValves.Add("AA");
        interestingValves.AddRange(valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Id));
    }
}