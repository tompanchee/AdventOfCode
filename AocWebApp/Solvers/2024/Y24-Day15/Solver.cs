using System.Text;
using Common;
using Common.Solver;
using Common.Utils.Grid2D;
using Serilog;
using Serilog.Events;

namespace Y24_Day15;

[Day(2024, 15, "Warehouse Woes")]
public class Solver : SolverBase
{
    private string movements = string.Empty;
    private SparseGrid warehouse;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        ReadInputData();
    }

    private void ReadInputData()
    {
        List<string> wareHouseData = new();
        bool warehouseDone = false;

        foreach (string line in inputLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                warehouseDone = true;
                continue;
            }

            if (warehouseDone)
            {
                movements += line;
            }
            else
            {
                wareHouseData.Add(line);
            }
        }

        warehouse = new SparseGrid(wareHouseData.ToArray(), '.');
    }

    public override Task Solve1()
    {
        logger.Information("Robot running in the warehouse...");

        DebugPrint();
        Robot robot = new(warehouse);
        foreach (char move in movements)
        {
            robot.Move(move);
            DebugPrint();
        }
        
        logger.Information("Box GPS Coordinate sum is {sum}", CalculateBoxGpsCoordinateSum());

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        return Task.CompletedTask;
    }

    private int CalculateBoxGpsCoordinateSum()
    {
        return warehouse.Where(p => p.value == 'O').Sum(p => (p.point.Y * 100) + p.point.X);
    }

    private void DebugPrint()
    {
        if (!logger.IsEnabled(LogEventLevel.Debug))
        {
            return;
        }

        StringBuilder sb = new();

        for (int y = 0; y < warehouse.YLength; y++)
        {
            sb.AppendLine();
            for (int x = 0; x < warehouse.XLength; x++)
            {
                char[] value = warehouse[x, y];
                sb.Append(value.Length == 0 ? '.' : value.First());
            }
        }

        logger.Debug(sb.ToString());
    }
}

internal class Robot
{
    private const char RobotValue = '@';
    private const char Wall = '#';
    private const char Box = 'O';

    private static readonly Dictionary<char, Orientation> Moves = new()
    {
        { '^', Orientation.Up }, { '>', Orientation.Right }, { 'v', Orientation.Down }, { '<', Orientation.Left }
    };

    private readonly SparseGrid warehouse;
    private Point position;

    public Robot(SparseGrid warehouse)
    {
        this.warehouse = warehouse;
        position = warehouse.Single(v => v.value == RobotValue).point;
    }

    public void Move(char direction)
    {
        Point newPos = position.Move(Moves[direction]);

        if (warehouse[newPos].Contains(Wall))
        {
            return;
        }

        if (warehouse[newPos].Contains(Box))
        {
            if (!Push(newPos, Moves[direction]))
            {
                return;
            }
        }

        // When pushed or when empty move robot
        warehouse[position] = [];
        warehouse[newPos] = ['@'];
        position = newPos;
    }

    private bool Push(Point pos, Orientation direction)
    {
        Point newPos = pos;

        while (true)
        {
            newPos = newPos.Move(direction);

            if (warehouse[newPos].Contains(Wall))
            {
                return false;
            }

            if (warehouse.IsEmpty(newPos))
            {
                warehouse[newPos] = [Box];
                break;
            }
        }

        return true;
    }
}