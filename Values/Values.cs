namespace Values;
using Results;
public record PositiveFloat
{
    public float Value { get; }
    protected PositiveFloat(float value) => Value =
        value <= 0
            ? throw new ArgumentOutOfRangeException()
            : value;

    public static Result<PositiveFloat> Create(float value) =>
        value <= 0
            ? Result<PositiveFloat>.Fail(new UnknownError())
            : Result<PositiveFloat>.Ok(new PositiveFloat(value));

    public static implicit operator float(PositiveFloat value) => value.Value;
}

public record PurchaseAmount : PositiveInteger
{
    private PurchaseAmount(int value) : base(value) { }

    new public static Result<PurchaseAmount> Create(int value) =>
        value <= 0
            ? Result<PurchaseAmount>.Fail(new UnknownError())
            : Result<PurchaseAmount>.Ok(new PurchaseAmount(value));

    public static implicit operator int(PurchaseAmount amount) => amount.Value;
}

