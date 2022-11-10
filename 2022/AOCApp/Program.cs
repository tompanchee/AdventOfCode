using AOCApp;
using Spectre.Console.Cli;

var app = new CommandApp<App>();
app.Configure(c => c.PropagateExceptions());
return app.Run(Environment.GetCommandLineArgs()[1..]);