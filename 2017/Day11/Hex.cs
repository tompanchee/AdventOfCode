namespace Day11;

class Hex
{
    // Axial coordinate system, see  https://www.redblobgames.com/grids/hexagons/#coordinates-axial
    public Hex(int q, int r)
    {
        Q = q;
        R = r;
    }

    public int Q { get; private set; }
    public int R { get; private set; }

    public void Move(string direction)
    {
       switch (direction)
        {
            case "n":
                R--;
                break;            
            case "ne":
                R--;
                Q++;
                break;
            case "se":
                Q++;                
                break;
            case "s":
                R++;
                break;
            case "sw":
                Q--;
                R++;
                break;
            case "nw":
                Q--;
                break;
                
        }
    }

    public int DistanceTo(Hex other)
    {
        return (Math.Abs(Q - other.Q)
          + Math.Abs(Q + R - other.Q - other.R)
          + Math.Abs(R - other.R)) / 2;
    }
}
