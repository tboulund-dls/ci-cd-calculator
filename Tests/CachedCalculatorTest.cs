namespace Tests;

public class CachedCalculatorTest
{
    [Test]
    public void Add()
    {
        Calculator.CachedCalculator cachedCalculator = new Calculator.CachedCalculator();
        Assert.That(cachedCalculator.Add(1, 2), Is.EqualTo(3));
        Assert.IsFalse(cachedCalculator.WasLatestOperationCached);
        Assert.That(cachedCalculator.Add(1, 2), Is.EqualTo(3));
        Assert.IsTrue(cachedCalculator.WasLatestOperationCached);
    }
    
    [Test]
    public void Subtract()
    {
        Calculator.CachedCalculator cachedCalculator = new Calculator.CachedCalculator();
        Assert.That(cachedCalculator.Subtract(3, 2), Is.EqualTo(1));
        Assert.IsFalse(cachedCalculator.WasLatestOperationCached);
        Assert.That(cachedCalculator.Subtract(3, 2), Is.EqualTo(1));
        Assert.IsTrue(cachedCalculator.WasLatestOperationCached);
    }
    
    [Test]
    public void Multiply()
    {
        Calculator.CachedCalculator cachedCalculator = new Calculator.CachedCalculator();
        Assert.That(cachedCalculator.Multiply(2, 3), Is.EqualTo(6));
        Assert.IsFalse(cachedCalculator.WasLatestOperationCached);
        Assert.That(cachedCalculator.Multiply(2, 3), Is.EqualTo(6));
        Assert.IsTrue(cachedCalculator.WasLatestOperationCached);
    }
    
    [Test]
    public void Divide()
    {
        Calculator.CachedCalculator cachedCalculator = new Calculator.CachedCalculator();
        Assert.That(cachedCalculator.Divide(6, 2), Is.EqualTo(3));
        Assert.That(cachedCalculator.Divide(6, 2), Is.EqualTo(3));
        Assert.IsTrue(cachedCalculator.WasLatestOperationCached);
    }
    
    [Test]
    public void Factorial()
    {
        Calculator.CachedCalculator cachedCalculator = new Calculator.CachedCalculator();
        Assert.That((int)cachedCalculator.Factorial(5), Is.EqualTo(120));
        Assert.Catch<ArgumentException>(() => cachedCalculator.Factorial(-1));
    }
    
    [Test]
    public void IsPrime()
    {
        Calculator.CachedCalculator cachedCalculator = new Calculator.CachedCalculator();
        Assert.Multiple(() =>
        {
            Assert.That(cachedCalculator.IsPrime(7), Is.True);
            Assert.That(cachedCalculator.IsPrime(1), Is.False);
            Assert.That(cachedCalculator.IsPrime(4), Is.False);
        });
    }
}