using System.Buffers;

namespace Day22;

internal class Walker
{
    static readonly List<char> directions = new(new[] {'R', 'D', 'L', 'U'});

    static readonly IDictionary<char, (int dr, int dc)> offsets = new Dictionary<char, (int dr, int dc)> {
        {'R', (0, 1)},
        {'D', (1, 0)},
        {'L', (0, -1)},
        {'U', (-1, 0)}
    };

    readonly int height;

    readonly List<string> map;
    readonly int width;
    readonly bool isCube;
    readonly int cubeSize;

    (int row, int col) position;

    public Walker((int row, int col) position, char direction, List<string> map, bool isCube = false)
    {
        this.position = position;
        Direction = direction;
        this.map = map;
        width = map[0].Length;
        height = map.Count;
        this.isCube = isCube;
        cubeSize = map[0].Length / 3; // See map form ResolveSide method
    }

    public (int row, int col) Position { get => position; set => position = value; }
    public char Direction { get; set; }

    public void ExecuteInstruction(string instruction) {
        if (int.TryParse(instruction, out var distance)) Move(distance);
        else Turn(instruction);
    }

    public long CalculatePassword() {
        return 1000 * (position.row + 1) + 4 * (position.col + 1) + directions.IndexOf(Direction);
    }

    void Move(int distance) {
        if (isCube) MoveCube(distance);
        
        var offset = offsets[Direction];
        var next = position;

        for (var i = 0; i < distance; i++) {
            do {
                next = (next.row + offset.dr, next.col + offset.dc);
                if (next.row < 0) next.row = height - 1;
                if (next.row >= height) next.row = 0;
                if (next.col < 0) next.col = width - 1;
                if (next.col >= width) next.col = 0;
            } while (map[next.row][next.col] == ' ');

            if (map[next.row][next.col] == '#') break; // Stop
            if (map[next.row][next.col] == '.') position = next; // Keep on moving
        }
    }

    private void MoveCube(int distance)
    {
        var side = ResolveSide();

        var offset = offsets[Direction];
        var next = position;

        for(var i=0; i < distance; i++)
        {            
            next = (next.row + offset.dr, next.col + offset.dc);

            var row = next.row;
            var col = next.col;

            // Stupid if section for cube traversal
            if (next.row < 0) // Going up from 0 or 1
            {
                if (side == 0)
                {
                    Direction = 'R';
                    next.row = position.col;
                    next.col = 0;
                }
                else // side == 1
                {
                    next.row = height - 1;
                }
            }

            if (next.row >= height) // Going down from 5
            {
                next.row = 0;
                next.col = position.col;
            }

            if (next.col < 0) // Going left from 3 or 5
            {
                if (side == 3)
                {
                    Direction = 'R';
                    next.row = cubeSize - (row - 2 * cubeSize);
                    next.col = 0;

                } 
                else // side == 5
                {
                    Direction = 'D';
                    next.row = 0;
                    next.col = row + cubeSize - 3*cubeSize;
                }
            }

            if (next.col >= width)  // Going right from 1
            {
                Direction = 'L';
                next.row = cubeSize - (row - 2 * cubeSize);
                next.col = 2 * cubeSize - 1;
            }

            if (map[next.row][next.col] == ' ')
            {
                if (side == 0) // Going left from side 0
                {
                    Direction = 'R';
                    next.row = cubeSize - row + 2 * cubeSize;
                    next.col = 0;
                }

                if (side == 1) // Going down fom side 1
                {
                    Direction = 'L';
                    next.row = col - 2 * cubeSize + cubeSize;
                }

                if (side == 2) // Going left or right from side 2
                {
                    if (next.col < cubeSize) // left
                    {
                        Direction = 'D';
                        next.row = 0;
                        next.col = row - cubeSize + 2* cubeSize;
                    }
                    else // right
                    {
                        Direction = 'U';
                        next.row = cubeSize - 1;
                        next.col = row-cubeSize + 2 * cubeSize;
                    }
                }

                if (side == 3) // Going up from side 3
                {
                    Direction = 'R';
                    next.row = width - 1;
                    next.col = 0;
                }

                if (side == 4) // Going right or down from side 4
                {
                    if (next.col >= 2 * cubeSize) // Right
                    {
                        Direction = 'L';
                        next.row = cubeSize - (row - 2 * cubeSize);
                        next.col = width -1;
                    }
                    else // Down
                    {
                        Direction = 'L';
                        next.row = col - cubeSize + 3 * cubeSize;
                        next.col = cubeSize - 1;
                    }
                }

                if (side == 5) // Going right from side 5
                {
                    Direction = 'U';
                    next.row = 3 * cubeSize -1;
                    next.col = row - 3 * cubeSize + cubeSize;
                }
            }

            if (map[next.row][next.col] == '#') break; // Stop
            if (map[next.row][next.col] == '.') position = next; // Keep on moving
            side = ResolveSide();
        }
        
    }

    int ResolveSide()
    {
        /* Cube sides          
          |0|1|
          |2|
        |3|4|
        |5|          
        */

        // side 0,1
        if (position.row < cubeSize) {        
            if (position.col < cubeSize * 2) return 0;
            return 1;
        }
        
        // side 2
        if (position.row >= cubeSize && position.row < cubeSize*2)
        {
            return 2;
        }

        // side 3,4
        if (position.row >= cubeSize*2 && position.row < cubeSize*3)
        {
            if (position.col < cubeSize) return 3;
            return 4;
        }

        // side 5
        return 5;
    }


    void Turn(string instruction) {
        var idx = directions.IndexOf(Direction);
        if (instruction == "L") idx--;
        else idx++;

        if (idx < 0) idx = directions.Count - 1;
        if (idx >= directions.Count) idx = 0;

        Direction = directions[idx];
    }
}