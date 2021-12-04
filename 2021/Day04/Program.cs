using Day04;

var input = File.ReadAllLines("input.txt");
var (numbers, boards) = ParseInput(input);

Console.WriteLine("Solving puzzle 1...");
var (number, winner) = Play(boards, numbers);
Console.WriteLine($"Winning board score is {winner.GetWinningScore(number)}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
var (lastnumber, lastBoard) = FindLastBoard(boards, numbers);
Console.WriteLine($"Last board to win has score {lastBoard.GetWinningScore(lastnumber)}");

(int[], Board[]) ParseInput(string[] input) {
    var numbers = input[0].Split(',').Select(int.Parse).ToArray();
    var boards = new List<Board>();

    var row = 2;
    int id = 0;
    while(true) {
        var table = input.Skip(row).Take(5).ToArray();

        if (table.Length == 0) break;

        boards.Add(Board.From(table, id++));
        row += 6;
    }

    return (numbers, boards.ToArray());
}

(int lastNumber, Board? winningBoard) Play(Board[] boards, int[] numbers) {
    Board? winner = null;
    int lastNumber = 0;
    foreach (var number in numbers) {
        foreach (var board in boards) { 
            board.DrawNumber(number);
            if (board.IsBingo()) {
                winner = board; 
                lastNumber = number;
                break;
            }            
        }
        if (winner != null) break;
    }

    return (lastNumber, winner);
}

(int lastNumber, Board? winningBoard) FindLastBoard(Board[] boards, int[] numbers) {
    var boardsLeft = new List<Board>(boards).ToArray();

    while(true) {
        var (lastNumber, board) = Play(boardsLeft, numbers);
        boardsLeft = new List<Board>(boardsLeft.Where(b => b.Id != board.Id)).ToArray();
        if (boardsLeft.Length == 0) return (lastNumber, board);
    }
}