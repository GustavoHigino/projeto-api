using Microsoft.AspNetCore.Mvc;

namespace PrimeiroProjeto.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class MathControllerV2 : ControllerBase
    {
        [HttpGet
        ("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(
            string firstNumber,
            string secondNumber)
        {
            if(IsNumeric(firstNumber)&& 
                    IsNumeric(secondNumber))
            {
                var sum = 
                    ConvertToDecimal
                    (firstNumber) 
                    +
                    ConvertToDecimal
                    (secondNumber);
                return Ok
                    (sum);
            }
            return BadRequest("Invalid input");
        }
        [HttpGet("Sub/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Sub(string firstNumber
            ,string secondNumber)
        {
            if(IsNumeric(firstNumber)&&
                IsNumeric(secondNumber))
            {
                var sub =
                    ConvertToDecimal
                    (firstNumber)
                    -
                    ConvertToDecimal
                    (secondNumber);
                return Ok(sub);
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Mult/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Mult(string firstNumber
            ,string secondNumber)
        {
            if (IsNumeric(firstNumber) &&
                IsNumeric(secondNumber))
            {
                var mult =
                    ConvertToDecimal
                    (firstNumber)
                    *
                    ConvertToDecimal
                    (secondNumber);
                return Ok(mult); 
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Div/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Div(string firstNumber
            ,string secondNumber)
        {
            if (IsNumeric(firstNumber) &&
                IsNumeric(secondNumber))
            {
                var div =
                    ConvertToDecimal
                    (firstNumber)
                    /
                    ConvertToDecimal
                    (secondNumber);
                return Ok(div);
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Med/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Med(string firstNumber
            ,string secondNumber)
        {
            if (IsNumeric(firstNumber) &&
                IsNumeric(secondNumber))
            {
                var med =(
                    ConvertToDecimal
                    (firstNumber)
                    +
                    ConvertToDecimal
                    (secondNumber))/2;
                return Ok(med);
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Raiz/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Raiz(string firstNumber
            ,string secondNumber)
        {
            if (IsNumeric(firstNumber) &&
                IsNumeric(secondNumber))
            {
                var firstResult =
                    Math.Sqrt(ConvertToDouble
                    (firstNumber));
                var secondResult =
                    Math.Sqrt(ConvertToDouble
                    (secondNumber));
                return Ok
                    (
                    $"a primeira raiz " +
                    $"é {firstResult}\n" +
                    $"a segunda raiz é {secondResult}");
                
                    
                    
                
            }
            return BadRequest("Invalid Param");
        }

        private double ConvertToDouble(string stringNumber)
        {
            double doubleNumber;
            if(double.TryParse(stringNumber,
                out doubleNumber))
            {
                return doubleNumber;
            }
            return 0;
        }

        private decimal ConvertToDecimal
            (string stringNumber)
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

        private bool IsNumeric(string stringNumber)
        {
            decimal decimalValue;
            var isNumber= decimal.TryParse
                (stringNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization
                .NumberFormatInfo.InvariantInfo,
                out decimalValue);
            return isNumber;
            
        }
    }
}
