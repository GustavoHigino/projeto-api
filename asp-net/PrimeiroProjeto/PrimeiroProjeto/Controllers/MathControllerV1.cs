using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Services;
using PrimeiroProjeto.Utils;

namespace PrimeiroProjeto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathV1Controller : ControllerBase
    {
        private readonly MathService _mathService;
        public MathV1Controller(MathService service)
        {
            _mathService = service;
        }
        [HttpGet
        ("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(
            string firstNumber,
            string secondNumber)
        {
            if(NumberHelper.IsNumeric(firstNumber)&&
                    NumberHelper.IsNumeric(secondNumber))
            {
                var sum = 
                    _mathService.Sum(
                    NumberHelper.ConvertToDecimal
                    (firstNumber) ,
                    NumberHelper.ConvertToDecimal
                    (secondNumber));
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
            if(NumberHelper.IsNumeric(firstNumber)&&
                NumberHelper.IsNumeric(secondNumber))
            {
                var sub = 
                    _mathService.Sub(
                    NumberHelper.ConvertToDecimal
                    (firstNumber)
                    ,
                    NumberHelper.ConvertToDecimal
                    (secondNumber));
                return Ok(sub);
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Mult/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Mult(string firstNumber
            ,string secondNumber)
        {
            if (NumberHelper.IsNumeric(firstNumber) &&
                NumberHelper.IsNumeric(secondNumber))
            {
                var mult =
                    _mathService.Mult(
                    NumberHelper.ConvertToDecimal
                    (firstNumber)
                    ,
                    NumberHelper.ConvertToDecimal
                    (secondNumber));
                return Ok(mult); 
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Div/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Div(string firstNumber
            ,string secondNumber)
        {
            if (NumberHelper.IsNumeric(firstNumber) &&
                NumberHelper.IsNumeric(secondNumber))
            {
                var div =
                    _mathService.Div(
                    NumberHelper.ConvertToDecimal
                    (firstNumber)
                    ,
                    NumberHelper.ConvertToDecimal
                    (secondNumber));
                return Ok(div);
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Med/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Med(string firstNumber
            ,string secondNumber)
        {
            if (NumberHelper.IsNumeric(firstNumber) &&
                NumberHelper.IsNumeric(secondNumber))
            {
                var med =
                    _mathService.Med(
                    NumberHelper.ConvertToDecimal
                    (firstNumber)
                    ,
                    NumberHelper.ConvertToDecimal
                    (secondNumber));
                return Ok(med);
            }
            return BadRequest("Invalid Param");
        }
        [HttpGet("Raiz/" +
            "{firstNumber}/{secondNumber}")]
        public IActionResult Raiz(string firstNumber
            ,string secondNumber)
        {
            if (NumberHelper.IsNumeric(firstNumber) &&
                NumberHelper.IsNumeric(secondNumber))
            {
                var firstResult =
                    _mathService.SquareRoot
                    (NumberHelper.ConvertToDecimal
                    (firstNumber));
                var secondResult =
                    _mathService.SquareRoot
                    (NumberHelper.ConvertToDecimal
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
