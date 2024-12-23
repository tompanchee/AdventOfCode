using App.Formatters;
using App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<SolverRepository>(sp => SolverRepository.Initialize());

builder.Services.AddControllers(options => options.InputFormatters.Add(new TextPlainInputFormatter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.EnableTryItOutByDefault();
            options.InjectStylesheet("/assets/css/aoc.css");
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
