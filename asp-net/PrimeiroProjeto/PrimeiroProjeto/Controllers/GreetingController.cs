using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Controllers;

[ApiController]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    private static long _counter = 0;
    private static readonly string _template 
        ="Hello, {0}";
    
    [HttpGet]
    public Greeting Get([FromQuery] string name ="world")
    {
        var id = Interlocked
            .Increment(ref _counter);
        var content = string
            .Format(_template, name);
        return new Greeting(id, content);   
    }
}
