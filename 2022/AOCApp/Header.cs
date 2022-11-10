using Spectre.Console;

namespace AOCApp;

internal static class Header
{
    const string AOC = @"

   ###     #######   ######      #######    #####    #######   #######  
  ## ##   ##     ## ##    ##    ##     ##  ##   ##  ##     ## ##     ## 
 ##   ##  ##     ## ##                 ## ##     ##        ##        ## 
##     ## ##     ## ##           #######  ##     ##  #######   #######  
######### ##     ## ##          ##        ##     ## ##        ##        
##     ## ##     ## ##    ##    ##         ##   ##  ##        ##        
##     ##  #######   ######     #########   #####   ######### ######### 

";

    const string TOMPANCHEE = @"
  _                                         _               
 | |_ ___  _ __ ___  _ __   __ _ _ __   ___| |__   ___  ___ 
 | __/ _ \| '_ ` _ \| '_ \ / _` | '_ \ / __| '_ \ / _ \/ _ \
 | || (_) | | | | | | |_) | (_| | | | | (__| | | |  __/  __/
  \__\___/|_| |_| |_| .__/ \__,_|_| |_|\___|_| |_|\___|\___|
                    |_|                                     
";

    public static void Render() {
        var table = new Table()
            .AddColumn("c1")
            .AddColumn("c2")
            .Border(TableBorder.None)
            .HideHeaders();

        var image = new CanvasImage("tonttulakki.jpg") {
            MaxWidth = 15
        };

        table.AddRow(image, new Markup(AOC + TOMPANCHEE) {Alignment = Justify.Center});

        AnsiConsole.Write(table);
    }
}