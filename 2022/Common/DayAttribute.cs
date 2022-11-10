namespace AOCCommon;

[AttributeUsage(AttributeTargets.Class)]
public class DayAttribute : Attribute
{
    readonly int dayNumber;
    readonly string description;

    public DayAttribute(int dayNumber, string? description = null) {
        this.dayNumber = dayNumber;
        this.description = description ?? $"Day {dayNumber:D2}";
    }

    public int DayNumber => dayNumber;
    public string Description => description;
}