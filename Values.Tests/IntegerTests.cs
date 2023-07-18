namespace Values.Tests;
using Results;

public class IntegerTests
{
    [Fact]
    public void PositiveInteger_Creation_With_Negative_Number_Returns_Failed_Result()
    {
        //Given
        Result<PositiveInteger> positive = PositiveInteger.Create(-10);
        //Then
        Assert.True(positive.Failed);
    }

    [Fact]
    public void NegativeInteger_Creation_With_Positive_Number_Returns_Failed_Result()
    {
        //Given
        Result<NegativeInteger> negative = NegativeInteger.Create(10);
        //Then
        Assert.True(negative.Failed);
    }

    [Fact]
    public void Zero_Is_Not_A_Positive_Integer()
    {
        //Given
        Result<PositiveInteger> zero = PositiveInteger.Create(0);
        //Then
        Assert.True(zero.Failed);
    }

    [Fact]
    public void Zero_Is_Not_A_Negative_Integer()
    {
        //Given
        Result<NegativeInteger> zero = NegativeInteger.Create(0);
        //Then
        Assert.True(zero.Failed);
    }

    [Fact]
    public void Zero_Is_NonNegativeInteger()
    {
        //Given
        Result<INonNegativeInteger> zero = Integer.CreateNonNegativeInteger(0);
        //Then
        Assert.True(zero.Succeeded);
        Assert.Equal(0, zero.Data.Value);
    }

    [Fact]
    public void Zero_Is_NonPositiveInteger()
    {
        //Given
        Result<INonPositiveInteger> zero = Integer.CreateNonPositiveInteger(0);
        //Then
        Assert.True(zero.Succeeded);
        Assert.Equal(0, zero.Data.Value);
    }
}
