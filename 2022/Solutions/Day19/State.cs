namespace Day19;

internal class State
{
    public int TimeLeft { get; set; }
    public int OreRobots { get; set; }
    public int ClayRobots { get; set; }
    public int ObsidianRobots { get; set; }
    public int GeodeRobots { get; set; }
    public int Ore { get; set; }
    public int Clay { get; set; }
    public int Obsidian { get; set; }
    public int Geode { get; set; }

    public State[] GetNextPossibleStates(Blueprint blueprint, int maxGeodes = 0) {
        if (TimeLeft == 0) return Array.Empty<State>();
        if (!ShouldContinue(blueprint, maxGeodes)) return Array.Empty<State>();

        var states = new List<State>();

        if (CanBuildGeodeRobot(blueprint)) {
            states.Add(BuildGeodeRobot(blueprint));
        } else {
            if (CanBuildObsidianRobot(blueprint)) states.Add(BuildObsidianRobot(blueprint));
            if (CanBuildClayRobot(blueprint)) states.Add(BuildClayRobot(blueprint));
            if (CanBuildOreRobot(blueprint)) states.Add(BuildOreRobot(blueprint));
        }

        states.Add(Mine());

        return states.ToArray();
    }

    bool ShouldContinue(Blueprint blueprint, int maxGeodes) {
        // NOTE: This should be optimized to do more aggressive pruning of obsolete branches
        var currentGeodes = Geode + GeodeRobots * TimeLeft;
        var potentialGeodes = TimeLeft / 2 * TimeLeft;
        if (currentGeodes + potentialGeodes < maxGeodes) return false;
        return true;
    }

    State Mine() {
        return new State {
            TimeLeft = TimeLeft - 1,
            Ore = Ore + OreRobots,
            Clay = Clay + ClayRobots,
            Obsidian = Obsidian + ObsidianRobots,
            Geode = Geode + GeodeRobots,
            OreRobots = OreRobots,
            ClayRobots = ClayRobots,
            ObsidianRobots = ObsidianRobots,
            GeodeRobots = GeodeRobots
        };
    }

    State BuildOreRobot(Blueprint blueprint) {
        var state = Mine();
        state.Ore -= blueprint.OreRobotCost;
        state.OreRobots++;

        return state;
    }

    State BuildClayRobot(Blueprint blueprint) {
        var state = Mine();
        state.Ore -= blueprint.ClayRobotCost;
        state.ClayRobots++;

        return state;
    }

    State BuildObsidianRobot(Blueprint blueprint) {
        var state = Mine();
        state.Ore -= blueprint.ObsidianRobotCost.ore;
        state.Clay -= blueprint.ObsidianRobotCost.clay;
        state.ObsidianRobots++;

        return state;
    }

    State BuildGeodeRobot(Blueprint blueprint) {
        var state = Mine();
        state.Ore -= blueprint.GeodeRobotCost.ore;
        state.Obsidian -= blueprint.GeodeRobotCost.obsidian;
        state.GeodeRobots++;

        return state;
    }

    bool CanBuildOreRobot(Blueprint blueprint) {
        return Ore >= blueprint.OreRobotCost && OreRobots < blueprint.MaxOreRobotCount;
    }

    bool CanBuildClayRobot(Blueprint blueprint) {
        return Ore >= blueprint.ClayRobotCost && ClayRobots < blueprint.MaxClayRobotCount;
    }

    bool CanBuildObsidianRobot(Blueprint blueprint) {
        return Clay >= blueprint.ObsidianRobotCost.clay && Ore >= blueprint.ObsidianRobotCost.ore && ObsidianRobots < blueprint.MaxObsidianRobotCount;
    }

    bool CanBuildGeodeRobot(Blueprint blueprint) {
        return Obsidian >= blueprint.GeodeRobotCost.obsidian && Ore >= blueprint.GeodeRobotCost.ore;
    }
}