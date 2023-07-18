namespace Values;
using Results;

/// <summary>
/// Base abstraction and wrapper around integer values.
/// Provides factory methods to create non-negative and non-positive integers.
/// </summary>
public abstract record Integer
{
    /// <summary>
    /// Gets the integer value.
    /// </summary>
    public abstract int Value { get; }

    /// <summary>
    /// Creates a non-negative integer value.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>A Result containing the created non-negative integer or an error.</returns>
    public static Result<INonNegativeInteger> CreateNonNegativeInteger(int value)
    {
        if (value < 0)
            return Result<INonNegativeInteger>.Fail(new UnknownError());
        else if (value == 0)
            return Result<INonNegativeInteger>.Ok(new ZeroInteger());
        else
            return PositiveInteger.Create(value)
                .Map(integer => Result<INonNegativeInteger>.Ok(integer));
    }

    /// <summary>
    /// Creates a non-positive integer value.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>A Result containing the created non-positive integer or an error.</returns>
    public static Result<INonPositiveInteger> CreateNonPositiveInteger(int value)
    {
        if (value > 0)
            return Result<INonPositiveInteger>.Fail(new UnknownError());
        else if (value == 0)
            return Result<INonPositiveInteger>.Ok(new ZeroInteger());
        else
            return NegativeInteger.Create(value)
                .Map(integer => Result<INonPositiveInteger>.Ok(integer));
    }
}

/// <summary>
/// Represents a non-negative integer value.
/// </summary>
public interface INonNegativeInteger
{
    /// <summary>
    /// Gets the integer value.
    /// </summary>
    int Value { get; }
}

/// <summary>
/// Represents a non-positive integer value.
/// </summary>
public interface INonPositiveInteger
{
    /// <summary>
    /// Gets the integer value.
    /// </summary>
    int Value { get; }
}

/// <summary>
/// Represents a positive integer value (greater than zero).
/// </summary>
public record PositiveInteger : Integer, INonNegativeInteger
{
    /// <summary>
    /// Gets the integer value.
    /// </summary>
    public override int Value { get; }

    protected PositiveInteger(int value) => Value =
        value <= 0
            ? throw new ArgumentOutOfRangeException()
            : value;

    /// <summary>
    /// Creates a positive integer value.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>A Result containing the created positive integer or an error.</returns>
    public static Result<PositiveInteger> Create(int value) =>
        value <= 0
            ? Result<PositiveInteger>.Fail(new UnknownError())
            : Result<PositiveInteger>.Ok(new PositiveInteger(value));
}

/// <summary>
/// Represents the integer value zero.
/// </summary>
public record ZeroInteger : Integer, INonNegativeInteger, INonPositiveInteger
{
    /// <summary>
    /// Gets the integer value.
    /// </summary>
    public override int Value => 0;

    public ZeroInteger() { }
}

/// <summary>
/// Represents a negative integer value (less than zero).
/// </summary>
public record NegativeInteger : Integer, INonPositiveInteger
{
    /// <summary>
    /// Gets the integer value.
    /// </summary>
    public override int Value { get; }

    protected NegativeInteger(int value) => Value =
        value > 0
            ? throw new ArgumentOutOfRangeException()
            : value;

    /// <summary>
    /// Creates a negative integer value.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>A Result containing the created negative integer or an error.</returns>
    public static Result<NegativeInteger> Create(int value) =>
        value >= 0
            ? Result<NegativeInteger>.Fail(new UnknownError())
            : Result<NegativeInteger>.Ok(new NegativeInteger(value));
}

