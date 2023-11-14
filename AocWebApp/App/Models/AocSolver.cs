namespace App.Models
{
    public class AocSolver
    {
        public AocSolver(int year, int day, string description)
        {
            Year = year;
            Day = day;
            Description = description;
        }

        public int Year { get; set; }
        public int Day { get; set; }
        public string? Description { get; set; }
    }
}