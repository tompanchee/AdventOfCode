namespace Day19;

internal class Mine
{
    readonly Blueprint blueprint;
    int geodes;

    public Mine(Blueprint blueprint) {
        this.blueprint = blueprint;
    }

    public int StartMining(int time) {
        var state = new State {TimeLeft = time, OreRobots = 1};

        Step(state);

        return geodes;
    }

    void Step(State state) {
        if (state.Geode > geodes) geodes = state.Geode;
        foreach (var nextState in state.GetNextPossibleStates(blueprint, geodes)) Step(nextState);
    }
}