using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Wspolnota.Models;

namespace Wspolnota.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            ViewData["user"] = db.Users.Find(User.Identity.GetUserId());
            return View(await db.Communities.Include(c => c.Users).Include(c => c.Posts).ToListAsync());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Aplikacja została przygotowana na przedmiot Pracownia problemowa.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Fikcyjny przykładowy kontakt.";

            return View();
        }
    }
}