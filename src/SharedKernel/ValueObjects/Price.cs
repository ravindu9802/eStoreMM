using SharedKernel.Primitives;

namespace SharedKernel.ValueObjects;

public class Price : ValueObject
{
    public Price()
    {
    }

    private Price(double amount, string unit)
    {
        Amount = amount;
        Unit = unit;
    }

    public double Amount { get; }
    public string Unit { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Unit;
    }

    public static Price Create(double amount, string unit)
    {
        if (amount < 0) throw new ArgumentException("Amount should be a positive value.", nameof(amount));

        if (string.IsNullOrEmpty(unit)) throw new ArgumentException("Currency unit cannot be empty.", nameof(unit));

        if (unit.Length != 3) throw new ArgumentException("Invalid currency unit.", nameof(unit));

        return new Price(amount, unit.ToLower());
    }
}