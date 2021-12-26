using Day21;

var input = File.ReadAllLines("input.txt");

var (p1, p2) = Parse(input);

Console.WriteLine("Solving puzzle 1...");
var die = new DeterministicDie();
var game = new PracticeGame(p1, p2, 1000, die);
var winner = game.Play();
var checksum = (winner == Player.P1 ? game.P2Score : game.P1Score) * die.NumberOfRolls;
Console.WriteLine($"Player {winner} wins checksum is {checksum}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
var diracGame = new DiracGame(21);
diracGame.PlayAll(p1, p2);
Console.WriteLine($"{diracGame.MostWins} wins in {diracGame.MostWinsCount} universes");

static (int, int) Parse(string[] input) {
    var p1 = int.Parse(input[0]["Player x starting position: ".Length..]);
    var p2 = int.Parse(input[1]["Player x starting position: ".Length..]);
    return (p1, p2);
}
