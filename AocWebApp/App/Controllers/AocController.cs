using System.Diagnostics;
using App.Models;
using Common;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class AocController : ControllerBase
{
    private readonly SolverRepository solverRepository;

    public AocController(SolverRepository solverRepository)
    {
        this.solverRepository = solverRepository;
    }

    [HttpGet("")]
    [Produces("application/json")]
    public async Task<AocSolver[]> GetSolvers()
    {
        return await Task.FromResult(solverRepository.Solvers.Select(s => s.Solver).ToArray());
    }

    [HttpPost("{year}/{day}")]
    [Consumes("text/plain")]
    [Produces("text/plain")]
    public async Task<IActionResult> ExecuteSolver(int year, int day, [FromBody] string input, [FromHeader] bool debug = false)
    {
        await using var writer = new StringWriter();

        await using var logger = new LoggerConfiguration()
            .MinimumLevel.Is(debug ? LogEventLevel.Debug : LogEventLevel.Information)
            .WriteTo.TextWriter(writer)
            .WriteTo.Console()
            .CreateLogger();

        var solverModel = solverRepository.Solvers.SingleOrDefault(s => s.Solver.Year == year && s.Solver.Day == day);
        if (solverModel == null) return new BadRequestObjectResult($"No solver found for {year}-{day}");
        logger.Information("Advent of Code {year}-{day} - {description}", year, day, solverModel.Solver.Description);

        var totalTimeSw = Stopwatch.StartNew();
        var solver = Activator.CreateInstance(solverModel.Type, input, logger) as ISolver;

        logger.Information("---");

        logger.Information("Solving problem 1...");
        var sw = Stopwatch.StartNew();
        await solver!.Solve1();
        sw.Stop();
        logger.Information($"Problem 1 solved in {sw.ElapsedMilliseconds}ms");

        logger.Information("---");
        logger.Information("Solving problem 2...");
        sw.Restart();
        await solver!.Solve2();
        sw.Stop();
        logger.Information($"Problem 2 solved in {sw.ElapsedMilliseconds}ms");

        totalTimeSw.Stop();
        logger.Information("Solved both problems in totally {total} ms", totalTimeSw.ElapsedMilliseconds);

        return new OkObjectResult(writer.ToString());
    }
}