using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Calculator;

public class CachedCalculator : ICalculator
{
    private readonly SimpleCalculator _calculator = new();
    private readonly Dictionary<string, Calculation> _cache = new();
    private bool _wasLatestOperationCached = false;
    
    public bool WasLatestOperationCached => _wasLatestOperationCached;
    
    public int Add(int a, int b)
    {
        var cachedResult = GetCachedResult<int>(a, b);
        if (cachedResult == null)
        {
            _wasLatestOperationCached = false;
            return StoreInCache(_calculator.Add(a, b), a, b).Result;
        }

        _wasLatestOperationCached = true;
        return cachedResult.Result;
    }

    public int Subtract(int a, int b)
    {        
        var cachedResult = GetCachedResult<int>(a, b);
        if (cachedResult == null)
        {
            _wasLatestOperationCached = false;
            return StoreInCache(_calculator.Subtract(a, b), a, b).Result;
        }

        _wasLatestOperationCached = true;
        return cachedResult.Result;
    }

    public int Multiply(int a, int b)
    {        
        var cachedResult = GetCachedResult<int>(a, b);
        if (cachedResult == null)
        {
            _wasLatestOperationCached = false;
            return StoreInCache(_calculator.Multiply(a, b), a, b).Result;
        }

        _wasLatestOperationCached = true;
        return cachedResult.Result;
    }

    public int Divide(int a, int b)
    {        
        var cachedResult = GetCachedResult<int>(a, b);
        if (cachedResult == null)
        {
            _wasLatestOperationCached = false;
            return StoreInCache(_calculator.Divide(a, b), a, b).Result;
        }

        _wasLatestOperationCached = true;
        return cachedResult.Result;
    }

    public BigInteger Factorial(int n)
    {
        var cachedResult = GetCachedResult<BigInteger>(n);
        if (cachedResult == null)
        {
            _wasLatestOperationCached = false;
            return StoreInCache(_calculator.Factorial(n), n).Result;
        }
        
        _wasLatestOperationCached = true;
        return cachedResult.Result;
    }

    public bool IsPrime(int candidate)
    {
        var cachedResult = GetCachedResult<bool>(candidate);
        if (cachedResult == null)
        {
            _wasLatestOperationCached = false;
            return StoreInCache(_calculator.IsPrime(candidate), candidate).Result;
        }
        
        _wasLatestOperationCached = true;
        return cachedResult.Result;
    }
    
    private Calculation<T>? GetCachedResult<T>(int a, int? b = null, [CallerMemberName]string operation = "")
    {
        var calc = new Calculation<T>(default, operation, a, b);
        if (_cache.ContainsKey(calc.GetKey()))
        {
            return (Calculation<T>?)_cache[calc.GetKey()];
        }
        return null;
    }
    
    private Calculation<T> StoreInCache<T>(T result, int a, int? b = null, [CallerMemberName]string operation = "")
    {
        var calc = new Calculation<T>(result, operation, a, b);
        calc.Result = result;
        _cache.Add(calc.GetKey(), calc);
        return calc;
    }
    
    private class Calculation
    {
        private int A { get; }
        private int? B { get; }
        private string Operation { get; }

        protected Calculation(string operation, int a, int? b = null)
        {
            A = a;
            B = b;
            Operation = operation;
        }
    
        public string GetKey()
        {
            return string.Concat(A, Operation, B);
        }
    }

    private sealed class Calculation<T>(T? result, string operation, int a, int? b = null)
        : Calculation(operation, a, b)
    {
        public T? Result { get; set; } = result;
    }
}