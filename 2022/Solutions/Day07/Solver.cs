using AOCCommon;
using Serilog.Core;

namespace Day07;

[Day(7, "No Space Left On Device")]
internal class Solver : SolverBase
{
    Node root;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() 
    {
        var sum = 0L;
        CalculateChildDirectoriesSize(root);

        logger.Information("Total size of directories with a total size of at most 100000 is {sum}", sum);

        void CalculateChildDirectoriesSize(Node node) 
        {
            if (node != root && node.Size <= 100000) sum += node.Size;
            foreach (var dir in node.Directories) CalculateChildDirectoriesSize(dir);
        }
    }

    public override void Solve2() {
        const int totalSpace = 70000000;
        const int updateSpace = 30000000;

        var neededSpace = updateSpace - (totalSpace - root.Size);

        var freedSize = long.MaxValue;
        CalculateMinimumFreedSize(root);

        logger.Information("Smallest directory size to be deleted is {freedSize}", freedSize);

        void CalculateMinimumFreedSize(Node node) 
        {
            if (node != root && node.Size > neededSpace && node.Size < freedSize) freedSize = node.Size;
            foreach (var dir in node.Directories) CalculateMinimumFreedSize(dir);
        }
    }

    protected override void PostConstruct() {
        logger.Information("Analyzing file system...");

        root = new Directory("/");
        var currentNode = root;

        var i = 0;
        while (i < data.Length) 
        {
            var row = data[i];

            i += HandleCommand(row[2..]);
        }

        int HandleCommand(string cmd) 
        {
            var op = cmd[..2];
            logger.Debug("Handling command {cmd}", cmd);

            return op switch 
            {
                "cd" => SwitchDirectory(cmd[3..]),
                "ls" => ListDirectory(),
                _ => 0
            };
        }

        int SwitchDirectory(string dir) 
        {
            logger.Debug("Switching to directory {dir}", dir);
            switch (dir) 
            {
                case "/":
                    currentNode = root;
                    break;
                case "..":
                    if (currentNode.Parent != null) currentNode = currentNode.Parent;
                    break;
                default: 
                {
                    var newNode = currentNode.Children.SingleOrDefault(n => n.Name == dir);
                    if (newNode != null) currentNode = newNode;
                    break;
                }
            }

            logger.Debug("Current node is {dir}", currentNode.Name);

            return 1;
        }

        int ListDirectory() {
            logger.Debug("Listing current directory...");
            var offset = 1;

            while (i + offset < data.Length && data[i + offset][0] != '$') 
            {
                logger.Debug("Adding item {item}", data[i + offset]);
                var itemInfo = data[i + offset].Split(' ');
                if (itemInfo[0] == "dir") 
                {
                    currentNode.Children.Add(new Directory(itemInfo[1], currentNode));
                    logger.Debug("Added directory {dir}", itemInfo[1]);
                } else 
                {
                    var size = long.Parse(itemInfo[0]);
                    currentNode.Children.Add(new File(itemInfo[1], size, currentNode));
                    logger.Debug("Added file {file} of size {size}", itemInfo[1], size);
                }

                offset++;
            }

            return offset;
        }
    }
}