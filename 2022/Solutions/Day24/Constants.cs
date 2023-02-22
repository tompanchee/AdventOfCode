namespace Day24;

internal static class Constants
{
    public const int EDGE_X_MIN = 0;
    public const int EDGE_X_MAX = 151;
    public const int EDGE_Y_MIN = 0;
    public const int EDGE_Y_MAX = 21;

    public const int LEAST_COMMON_MULTIPLE = 600; // Least common multiple for basin size 150x20 (300 should do but my algorithm gives an incorrect result with 300?)

    // Test data
    //public const int EDGE_X_MIN = 0;
    //public const int EDGE_X_MAX = 7;
    //public const int EDGE_Y_MIN = 0;
    //public const int EDGE_Y_MAX = 5;

    //public const int LEAST_COMMON_MULTIPLE = 24; // Least common multiple for basin size 6x4

    // Use directions towards target first
    public static readonly (int dx, int dy)[] Offsets = {
        (1, 0),  //R
        (0, 1),  //D
        (0, 0),  //Wait
        (0, -1), //U
        (-1, 0)  //L
    };

    public static readonly (int dx, int dy)[] OffsetsReverse = {
        (-1, 0), //L
        (0, -1), //U
        (0, 0),  //Wait
        (0, 1),  //D
        (1, 0)   //R
    };
}