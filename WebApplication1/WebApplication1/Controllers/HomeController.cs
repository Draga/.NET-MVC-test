using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private NHSTestEntities db = new NHSTestEntities();

        // GET: People
        public async Task<ActionResult> Index()
        {
            return View(await db.People.ToListAsync());
        }

        // GET: People/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = await db.People.FindAsync(id);

            if (person == null)
            {
                return HttpNotFound();
            }

            var editPersonModel = new EditPersonModel()
            {
                PersonId = person.PersonId,
                Name = person.FirstName,
                IsAuthorised =  person.IsAuthorised,
                IsEnabled = person.IsEnabled,
                FavouriteColours = person.FavouriteColours
            };

            return View(editPersonModel);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditPersonModel editPersonModel)
        {
            if (ModelState.IsValid)
            {
                var person = db.People.FirstOrDefault(p => p.PersonId == editPersonModel.PersonId);

                if (person == null)
                {
                    return HttpNotFound();
                }

                person.IsAuthorised = editPersonModel.IsAuthorised;
                person.IsEnabled = editPersonModel.IsEnabled;
                person.FavouriteColours = editPersonModel.FavouriteColours;

                db.Entry(person).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(editPersonModel);
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
