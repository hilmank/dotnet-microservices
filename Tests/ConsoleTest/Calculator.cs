using System;

namespace ConsoleTest;

public class Calculator
{
    public int CalculateAdd(int a, int b)
    {
        return a + b;
    }
    public double CalculateDivide(int a, int b)
    {
        if(b==0) return double.PositiveInfinity;
        var adec = (double)a;
        var bdec = (double)b;
        return adec/bdec;
    }
}
