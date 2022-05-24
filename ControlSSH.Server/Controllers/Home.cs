using Microsoft.AspNetCore.Mvc;

namespace ControlSSH.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class HomeController : Controller
{
    // GET
    [HttpGet("Index")]
    public string Index()
    {
        return "я бы с радостью если бы не учеба и работа !";
    }
}