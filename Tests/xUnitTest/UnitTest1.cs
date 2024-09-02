using ConsoleTest;

namespace xUnitTest;

public class UnitTest1
{
    [Fact]
    public void CalculateAdd()
    {
        Calculator calculator = new Calculator();
        int number1 = 2;
        int number2 = 1;
        var result = calculator.CalculateAdd(number1, number2);
        Assert.Equal(3, result);
    }
    [Fact]
    public void CalculateDivide()
    {
        Calculator calculator = new();
        int number1 = 1;
        int number2 = 0;
        var result = calculator.CalculateDivide(number1, number2);
        Assert.Equal(double.PositiveInfinity, result, 2);
    }
    [Fact]
    public void GetList()
    {
        ArrayListData arrayListData = new();
        var result = arrayListData.GetList();
        Assert.Null(result);
    }
    [Fact]
    public void GetEnums()
    {
        ArrayListData arrayListData = new();
        var result = arrayListData.GetEnums();
        Assert.Null(result);
    }
}