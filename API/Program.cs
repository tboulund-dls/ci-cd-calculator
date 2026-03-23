using System.Diagnostics.CodeAnalysis;
using Calculator;

[assembly: ExcludeFromCodeCoverage(Justification = "Only used for routing to Business Logic")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(c =>
{
    c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});
// app.UseHttpsRedirection();

app.MapGet("/calculator", (string calculator, string op, int number1, int? number2) =>
    {
        ICalculator calc;
        if (calculator == "Simple")
        {
            calc = new SimpleCalculator();
        }
        else
        {
            calc = new CachedCalculator();
        }

        var operatorMethod = calc.GetType().GetMethod(op);
        if (operatorMethod?.GetParameters().Length == 1)
        {
            return operatorMethod?.Invoke(calc, [number1])?.ToString();
        }
        return operatorMethod?.Invoke(calc, [number1, number2])?.ToString();
    })
    .WithName("Calculator")
    .WithOpenApi();

app.MapGet("/history", () =>
{
    
});

await app.RunAsync();