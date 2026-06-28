namespace PrimeiroProjeto.Services
{
    public class MathService
    {
        public decimal Sum(decimal firstNumber,
            decimal secondNumber) =>
            firstNumber + secondNumber;
        public decimal Sub(decimal firstNumber,
            decimal secondNumber) =>
            firstNumber - secondNumber;
        public decimal Mult(decimal firstNumber,
            decimal secondNumber) =>
            firstNumber * secondNumber;
        public decimal Div(decimal firstNumber,
            decimal secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new DivideByZeroException(
                    "Division by zero " +
                    "is not allowed");
            }
            return firstNumber / secondNumber;

        }
        public decimal Med(decimal firstNumber,
            decimal secondNumber) =>(
            firstNumber + secondNumber)/2;
        public double SquareRoot
            (decimal firstNumber)
        {
            if((firstNumber < 0))
            {
                throw new
                    ArgumentOutOfRangeException
                ("cannot calculate " +
                "the square root of a negative" +
                " number.");
            }
           return Math.Sqrt((double)firstNumber);
        }
            
    }
}
