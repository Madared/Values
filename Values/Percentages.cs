namespace Values;
using Results;

/// <summary>
/// Base abstraction for representing a percentage value.
/// </summary>
public abstract record Percentage
{
    /// <summary>
    /// Gets the percentage value.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Percentage"/> class.
    /// </summary>
    /// <param name="value">The percentage value.</param>
    protected Percentage(int value) => Value = value;
}

/// <summary>
/// Represents a discount percentage value.
/// </summary>
public record DiscountPercentage : Percentage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DiscountPercentage"/> class.
    /// </summary>
    /// <param name="value">The discount percentage value.</param>
    public DiscountPercentage(IntegerBetween0And100NonInclusive value) 
        : base(value.Value) {}
}

/// <summary>
/// Represents an integer value between 0 and 100 (non-inclusive).
/// </summary>
public record IntegerBetween0And100NonInclusive : Integer, INonNegativeInteger
{
    /// <summary>
    /// Gets the integer value.
    /// </summary>
    public override int Value { get; }

    private IntegerBetween0And100NonInclusive(int value) => Value = 
        value >= 100
            ? throw new ArgumentOutOfRangeException()
            : value;

    /// <summary>
    /// Creates an instance of <see cref="IntegerBetween0And100NonInclusive"/>.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>A Result containing the created instance or an error.</returns>
    public static Result<IntegerBetween0And100NonInclusive> Create(int value) => 
        value >= 100 || value < 0
            ? Result<IntegerBetween0And100NonInclusive>.Fail(new UnknownError())
            : Result<IntegerBetween0And100NonInclusive>.Ok(new IntegerBetween0And100NonInclusive(value));
}

