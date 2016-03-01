using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class HomeController : Controller
    {
        private NHSTestEntities db = new NHSTestEntities();

        // GET: People
        public async Task<ActionResult> Index()
        {
            return View(await db.People.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
