namespace Common.Solver;

[AttributeUsage(AttributeTargets.Class)]
public class DayAttribute : Attribute
{
    readonly int year;
    readonly int dayNumber;
    readonly string description;

    public DayAttribute(int year, int dayNumber, string? description = null)
    {
        this.year = year;
        this.dayNumber = dayNumber;
        this.description = description ?? $"Day {dayNumber:D2}";        
    }

    public int Year => year;
    public int DayNumber => dayNumber;
    public string Description => description;
}