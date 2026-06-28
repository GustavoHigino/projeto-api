namespace PrimeiroProjeto.Extensions
{
    public static class Numeric
    {
        public static double ConvertToDouble(this string stringNumber)
        {
            double doubleNumber;
            if (double.TryParse(stringNumber,
                out doubleNumber))
            {
                return doubleNumber;
            }
            return 0;
        }

        public static decimal ConvertToDecimal
            (this string stringNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(stringNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization
                .NumberFormatInfo.InvariantInfo,
                 out decimalValue))
            {
                return decimalValue;
            }
            return 0;

        }

        public static bool IsNumeric(this string stringNumber)
        {
            decimal decimalValue;
            var isNumber = decimal.TryParse
                (stringNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization
                .NumberFormatInfo.InvariantInfo,
                out decimalValue);
            return isNumber;

        }
    }
}
