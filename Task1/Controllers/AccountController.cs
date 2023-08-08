using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Task1.Models;
using Task1.VIewModel;

namespace Task1.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleDbEntities _db = new RoleDbEntities();

        [HttpGet]
        public ActionResult Login()
        {
            var data = new UserModel();
            return View(data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = _db.Users
               .Any(u => u.Email.ToLower() == user
               .Email.ToLower() && user
               .Password == user.Password);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var data = new User();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(registerUser);
                _db.SaveChanges();
                return RedirectToAction("Login");

            }
            return View(registerUser);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}