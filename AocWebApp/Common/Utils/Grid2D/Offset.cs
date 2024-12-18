namespace Common.Utils.Grid2D;

// This record could be actually called Vector as Could the Point record
// wanted to separate them
public record Offset(int Dx, int Dy)
{
    public static Offset operator -(Offset a) => new(-a.Dx, -a.Dy);

    public static Offset operator *(int m, Offset a) => new(m * a.Dx, m * a.Dy);
}