using Day20;

var input = File.ReadAllLines("input.txt");

var particles = Parse();
var origin = new Vector(0, 0, 0);

Console.WriteLine("Solving part 1...");

var particlesToCheck = particles.OrderBy(p => p.Acceleration.ManhattanDistanceTo(origin)).Take(10).ToList();
Simulate(particlesToCheck, 1000);

var closestParticle = particlesToCheck.OrderBy(p => p.Position.ManhattanDistanceTo(origin)).First();

Console.WriteLine($"Closest particle to <0,0,0> is {closestParticle.Id}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
// reset particles
particles = Parse();
Simulate(particles, 1000, true);

Console.WriteLine($"After all collisions there are {particles.Count} particles left");

void Simulate(IList<Particle> parts, int rounds, bool removeCollidingParticles = false) {
    for (var i = 0; i < rounds; i++) {
        foreach (var particle in parts) {
            particle.Velocity += particle.Acceleration;
            particle.Position += particle.Velocity;
        }

        if (removeCollidingParticles) {
            var collisions = parts
                .GroupBy(p => p.Position)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (!collisions.Any()) continue;

            var ids = parts.Where(p => collisions.Contains(p.Position)).Select(p => p.Id);

            var toRemove = parts.Where(p => ids.Contains(p.Id)).ToList();
            foreach (var p in toRemove) parts.Remove(p);
        }
    }
}

List<Particle> Parse() {
    var result = new List<Particle>();

    var id = 0;
    foreach (var line in input) {
        if (string.IsNullOrWhiteSpace(line)) continue;
        var split = line.Split(", ");

        var vectors = split
            .Select(component => component[3..^1]
                .Split(',')).Select(s => new Vector(long.Parse(s[0]), long.Parse(s[1]), long.Parse(s[2])))
            .ToList();

        result.Add(new Particle(id++, vectors[0], vectors[1], vectors[2]));
    }

    return result;
}