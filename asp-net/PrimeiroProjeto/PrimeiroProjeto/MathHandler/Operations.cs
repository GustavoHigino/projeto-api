using PrimeiroProjeto.Extensions;
namespace PrimeiroProjeto.MathHandler
{
    public static class Operations
    {
        public static double Sum(string number1,string number2)
        {
            var sum =
            Numeric.ConvertToDouble(number1)
            +
            Numeric.ConvertToDouble(number2);
            return sum;

        }
        public static double Sub(string number1,string number2)
        {
            var sub =
            Numeric.ConvertToDouble(number1)
            -
            Numeric.ConvertToDouble(number2);
            return sub;

        }
        public static double Mult(string number1,string number2)
        {
            var mult =
            Numeric.ConvertToDouble(number1)
            *
            Numeric.ConvertToDouble(number2);
            return mult;

        }
        public static double Div(string number1,string number2)
        {
            var div =
            Numeric.ConvertToDouble(number1)
            /
            Numeric.ConvertToDouble(number2);
            return div;

        }
        public static double Med(string number1,string number2)
        {
            var med =(
            Numeric.ConvertToDouble(number1)
            +
            Numeric.ConvertToDouble(number2))/2;
            return med;

        }
    }
}
