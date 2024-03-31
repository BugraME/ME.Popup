using Microsoft.AspNetCore.Mvc;
namespace SandBox.Controllers;
public class HomeController : Controller {
    public IActionResult Index() {
        return View();
    }
};