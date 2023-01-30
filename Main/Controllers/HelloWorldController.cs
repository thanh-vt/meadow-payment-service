using Microsoft.AspNetCore.Mvc;

namespace MeadowPaymentService.Controllers
{
    
    [Route("hello-world")]
    [Controller]
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        // 
        // GET: /HelloWorld/Welcome/ 
        [HttpGet("welcome")]
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}