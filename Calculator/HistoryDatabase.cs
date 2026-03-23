using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using MySqlConnector;

namespace Calculator;

[ExcludeFromCodeCoverage(Justification = "A wrapper with no logic except data-communication")]
public class HistoryDatabase(ICalculator inner) : ICalculator
{
    public int Add(int a, int b)
    {
        var result = inner.Add(a, b);
        StoreInDatabase(result, a, b);
        return result;
    }

    public int Subtract(int a, int b)
    {
        var result = inner.Subtract(a, b);
        StoreInDatabase(result, a, b);
        return result;
    }

    public int Multiply(int a, int b)
    {
        var result = inner.Multiply(a, b);
        StoreInDatabase(result, a, b);
        return result;
    }

    public int Divide(int a, int b)
    {
        var result = inner.Divide(a, b);
        StoreInDatabase(result, a, b);
        return result;
    }

    public BigInteger Factorial(int n)
    {
        var result = inner.Factorial(n);
        StoreInDatabase(result, n);
        return result;
    }

    public bool IsPrime(int candidate)
    {
        var result = inner.IsPrime(candidate);
        StoreInDatabase(result, candidate);
        return result;
    }
    
    private static void StoreInDatabase(object result, int number1, int? number2 = null, [CallerMemberName] string? caller = null)
    {
        using var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONN_STRING"));
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO MathHistory (number1, number2, operator, result) VALUES (@Number1, @Number2, @Operator, @Result)";
        command.Parameters.AddWithValue("@Number1", number1);
        command.Parameters.AddWithValue("@Number2", number2);
        command.Parameters.AddWithValue("@Operator", caller);
        command.Parameters.AddWithValue("@Result", result);
        command.ExecuteNonQuery();
    }
}