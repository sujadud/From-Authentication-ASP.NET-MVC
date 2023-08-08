using System.Web.Mvc;
using Task1.Models;

namespace Task1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly RoleDbEntities _db = new RoleDbEntities();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
    }
}