using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace Tests;

public class SimpleCalculatorTest
{
    [Test]
    public void Add()
    {
        Calculator.SimpleCalculator simpleCalculator = new Calculator.SimpleCalculator();
        Assert.That(simpleCalculator.Add(1, 2), Is.EqualTo(3));
    }
    
    [Test]
    public void Subtract()
    {
        Calculator.SimpleCalculator simpleCalculator = new Calculator.SimpleCalculator();
        Assert.That(simpleCalculator.Subtract(3, 2), Is.EqualTo(1));
    }
    
    [Test]
    public void Multiply()
    {
        Calculator.SimpleCalculator simpleCalculator = new Calculator.SimpleCalculator();
        Assert.That(simpleCalculator.Multiply(2, 3), Is.EqualTo(6));
    }
    
    [Test]
    public void Divide()
    {
        Calculator.SimpleCalculator simpleCalculator = new Calculator.SimpleCalculator();
        Assert.That(simpleCalculator.Divide(6, 2), Is.EqualTo(3));
        Assert.That(simpleCalculator.Divide(6, 2), Is.EqualTo(3)); // Second one to test cache, but in fact I can't be sure that it's cached so I can't assert anything about it.
    }
    
    [Test]
    public void Factorial()
    {
        Calculator.SimpleCalculator simpleCalculator = new Calculator.SimpleCalculator();
        Assert.That((int)simpleCalculator.Factorial(5), Is.EqualTo(120));
        Assert.Catch<ArgumentException>(() => simpleCalculator.Factorial(-1));
    }
    
    [Test]
    public void IsPrime()
    {
        Calculator.SimpleCalculator simpleCalculator = new Calculator.SimpleCalculator();
        Assert.Multiple(() =>
        {
            Assert.That(simpleCalculator.IsPrime(7), Is.True);
            Assert.That(simpleCalculator.IsPrime(1), Is.False);
            Assert.That(simpleCalculator.IsPrime(4), Is.False);
        });
    }
}