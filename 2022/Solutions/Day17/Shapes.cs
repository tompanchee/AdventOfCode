namespace Day17;

internal static class Shapes
{
    public const int SHAPE_COUNT = 5;

    // Flip shapes horizontally to help with indexes, bottom index 0

    // ####
    static readonly byte[] shape1 = {
        0b00011110
    };

    // .#.
    // ###
    // .#.
    static readonly byte[] shape2 = {
        0b00001000,
        0b00011100,
        0b00001000
    };

    // ..#
    // ..#
    // ###
    static readonly byte[] shape3 = {
        0b00011100,
        0b00000100,
        0b00000100
    };

    // #
    // #
    // #
    // #
    static readonly byte[] shape4 = {
        0b00010000,
        0b00010000,
        0b00010000,
        0b00010000
    };

    // ##
    // ##
    static readonly byte[] shape5 = {
        0b00011000,
        0b00011000
    };

    static readonly IList<byte[]> shapeList = new List<byte[]> {
        shape1,
        shape2,
        shape3,
        shape4,
        shape5
    };

    public static byte[] GetShape(int i) {
        return shapeList[i % shapeList.Count].ToArray();
    }
}