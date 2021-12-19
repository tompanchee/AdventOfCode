using Day15;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


var data = File.ReadAllLines("input.txt");

Console.WriteLine("Solving puzzle 1...");
var sw = Stopwatch.StartNew();
var graph = BuildGraph(data);
var (risk, _) = graph.FindShortestPath((0, 0), (data.Length - 1, data[0].Length - 1), NoHeuristics);
Console.WriteLine($"Minimum total risk through cave is {risk} in {sw.Elapsed}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
sw.Restart();
graph = BuildGraph5x(data);
Console.WriteLine($"Graph built, calculating path, time elapsed {sw.Elapsed}");
(risk, _) = graph.FindShortestPath((0, 0), (data.Length * 5 - 1, data[0].Length * 5 - 1), NoHeuristics);
Console.WriteLine($"Minimum total risk through cave is {risk} in {sw.Elapsed}");
        
// Heuristics for A* (NoHeuristics equals to Dijkstra algorithm)
static int NoHeuristics((int row, int col) n1, (int row, int col) n2) => 0;
static int Manhattan((int row, int col) n1, (int row, int col) n2) => Math.Abs(n1.row - n2.row) + Math.Abs(n1.col - n2.col);
static int Euclidean((int row, int col) n1, (int row, int col) n2) => (int)Math.Sqrt(Math.Pow(n1.row - n2.row, 2) + Math.Pow(n1.col - n2.col, 2));

static Graph<(int row, int col)> BuildGraph(string[] data) {
    (int dr, int dc)[] offsets = {
        (-1, 0),
        (0, 1),
        (1, 0),
        (0, -1)
    };

    var vertices = new List<Vertex<(int row, int col)>>();

    for (var row = 0; row < data.Length; row++) {
        for (var col = 0; col < data[row].Length; col++) {
            vertices.Add(new Vertex<(int row, int col)>((row, col)));
        }
    }

    foreach (var vertex in vertices) {
        foreach (var (dr, dc) in offsets) {
            var nr = vertex.Id.row + dr;
            var nc = vertex.Id.col + dc;

            if (nr < 0 || nr > data.Length - 1 || nc < 0 || nc > data[0].Length - 1) continue;

            vertex.Neighbours.Add((int.Parse(data[nr][nc].ToString()), (nr, nc)));
        }
    }

    return new Graph<(int row, int col)>(vertices, 100);
}

static Graph<(int row, int col)> BuildGraph5x(string[] data) {
    (int dr, int dc)[] offsets = {
        (-1, 0),
        (0, 1),
        (1, 0),
        (0, -1)
    };

    var vertices = new List<Vertex<(int row, int col)>>();

    for (var row = 0; row < data.Length * 5; row++) {
        for (var col = 0; col < data[row % 100].Length * 5; col++) {
            vertices.Add(new Vertex<(int row, int col)>((row, col)));
        }
    }

    foreach (var vertex in vertices) {
        foreach (var (dr, dc) in offsets) {
            var nr = vertex.Id.row + dr;
            var nc = vertex.Id.col + dc;

            if (nr < 0 || nr > data.Length * 5 - 1 || nc < 0 || nc > data[0].Length * 5 - 1) continue;

            var nrCost = int.Parse(data[nr % 100][nc % 100].ToString());
            nrCost += nr / 100 + nc / 100;
            if (nrCost > 9) nrCost -= 9;

            vertex.Neighbours.Add((nrCost, (nr, nc)));
        }
    }

    return new Graph<(int row, int col)>(vertices, 10000);
}