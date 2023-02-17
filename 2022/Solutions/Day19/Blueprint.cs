namespace Day19;

internal class Blueprint
{
    public Blueprint(int id, int oreRobotCost, int clayRobotCost, (int ore, int clay) obsidianRobotCost, (int ore, int obsidian) geodeRobotCost) {
        Id = id;
        OreRobotCost = oreRobotCost;
        ClayRobotCost = clayRobotCost;
        ObsidianRobotCost = obsidianRobotCost;
        GeodeRobotCost = geodeRobotCost;

        MaxOreRobotCount = new[] {clayRobotCost, obsidianRobotCost.ore, geodeRobotCost.ore}.Max();
        MaxClayRobotCount = obsidianRobotCost.clay;
        MaxObsidianRobotCount = geodeRobotCost.obsidian;
    }

    public int Id { get; init; }
    public int OreRobotCost { get; init; }
    public int ClayRobotCost { get; init; }
    public (int ore, int clay) ObsidianRobotCost { get; init; }
    public (int ore, int obsidian) GeodeRobotCost { get; init; }
    public int MaxOreRobotCount { get; init; }
    public int MaxClayRobotCount { get; init; }
    public int MaxObsidianRobotCount { get; init; }
}