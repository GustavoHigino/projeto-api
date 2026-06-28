using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Extensions;
using PrimeiroProjeto.MathHandler;

namespace PrimeiroProjeto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {


        [HttpGet
        ("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(
            string firstNumber,
            string secondNumber)
        {
            if(Numeric.IsNumeric(firstNumber)&&
                    Numeric.IsNumeric(secondNumber))
            {
                var sum = Operations.Sum(firstNumber, secondNumber);
                    
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
            if(Numeric.IsNumeric(firstNumber)&&
                Numeric.IsNumeric(secondNumber))
            {
                var sub =
                    Operations.Sub(firstNumber, secondNumber);
                return Ok(sub);
            }
            return BadRequest("Invalid Param");
        }


        [HttpGet("Mult/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Mult(string firstNumber
            ,string secondNumber)
        {
            if (Numeric.IsNumeric(firstNumber) &&
                Numeric.IsNumeric(secondNumber))
            {
                var mult =
                    Operations.Mult(firstNumber, secondNumber);
                return Ok(mult); 
            }
            return BadRequest("Invalid Param");
        }


        [HttpGet("Div/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Div(string firstNumber
            ,string secondNumber)
        {
            if (Numeric.IsNumeric(firstNumber) &&
                Numeric.IsNumeric(secondNumber))
            {
                var div =
                    Operations.Div(firstNumber, secondNumber);
                return Ok(div);
            }
            return BadRequest("Invalid Param");
        }


        [HttpGet("Med/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Med(string firstNumber
            ,string secondNumber)
        {
            if (Numeric.IsNumeric(firstNumber) &&
                Numeric.IsNumeric(secondNumber))
            {
                var med =
                    Operations.Med(firstNumber, secondNumber);
                return Ok(med);
            }
            return BadRequest("Invalid Param");
        }


        [HttpGet("Raiz/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Raiz(string firstNumber
            ,string secondNumber)
        {
            if (Numeric.IsNumeric(firstNumber) &&
                Numeric.IsNumeric(secondNumber))
            {
                var firstResult =
                    Math.Sqrt(Numeric.ConvertToDouble
                    (firstNumber));
                var secondResult =
                    Math.Sqrt(Numeric.ConvertToDouble
                    (secondNumber));
                return Ok
                    (
                    $"a primeira raiz " +
                    $"é {firstResult}\n" +
                    $"a segunda raiz é {secondResult}");
                
                    
                    
                
            }
            return BadRequest("Invalid Param");
        }

        
    }
}
