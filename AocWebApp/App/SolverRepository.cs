using System.Reflection;
using App.Models;
using Common;
using Common.Solver;

namespace App;

public class SolverRepository
{

    List<SolverModel> solvers = new();

    public static SolverRepository Initialize()
    {
        var repository = new SolverRepository();

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var assemblies = Directory.GetFiles(path!, "*.dll").Select(Assembly.LoadFile);

        var types = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && t.GetInterfaces().Contains(typeof(ISolver)));

        repository.solvers = new List<SolverModel>();
        foreach (var t in types.Where(t => t != null))
        {
            var a = t.GetCustomAttributes<DayAttribute>().FirstOrDefault();
            if (a != null)
            {
                repository.solvers.Add(new SolverModel(new AocSolver(a.Year, a.DayNumber, a.Description), t));
            }
        }

        return repository;
    }

    public IEnumerable<SolverModel> Solvers => solvers;
}

public record SolverModel(AocSolver Solver, Type Type);
