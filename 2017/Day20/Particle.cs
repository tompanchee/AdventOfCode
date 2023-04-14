namespace Day20;

internal class Particle
{
    public Particle(int id, Vector position, Vector velocity, Vector acceleration) {
        Id = id;
        Position = position;
        Velocity = velocity;
        Acceleration = acceleration;
    }

    public int Id { get; }
    public Vector Position { get; set; }
    public Vector Velocity { get; set; }
    public Vector Acceleration { get; set; }
}