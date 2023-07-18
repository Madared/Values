namespace Values.Tests;
using Results;

public class PercentageTests
{
    [Fact]
    public void IntegerBetween0And100NonInclusice_Fails_With_100()
    {
        //Given
        Result<IntegerBetween0And100NonInclusive> limitedInt = IntegerBetween0And100NonInclusive.Create(100);
        //Then
        Assert.True(limitedInt.Failed);
    }

    [Fact]
    public void DiscountPercentage_Maps_From_IntegerBetween0And100NonInclusive()
    {
        //Given
        Result<IntegerBetween0And100NonInclusive> limitedInt = IntegerBetween0And100NonInclusive.Create(30);
        //When
        Result<DiscountPercentage> percentage = limitedInt
            .Map(value => new DiscountPercentage(value));
        //Then
        Assert.True(percentage.Succeeded);
        Assert.Equal(limitedInt.Data.Value, percentage.Data.Value);
    }
}
