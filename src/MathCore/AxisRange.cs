namespace MathCore
{
    public class AxisRange
    {
        public double First { get; private set; }
        public double Second { get; private set; }

        public AxisRange() { }
        public AxisRange(double first, double second) { First = first; Second = second; }
    }
}
