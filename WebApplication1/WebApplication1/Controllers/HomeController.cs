using System.Collections.Generic;
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

            Person person = await db.People
                .Include(p => p.FavouriteColours)
                .FirstOrDefaultAsync(p => p.PersonId == id);

            if (person == null)
            {
                return HttpNotFound();
            }

            var colours = db.Colours.ToList();
            var editPersonModel = new EditPersonModel()
            {
                PersonId = person.PersonId,
                Name = person.FullName,
                IsAuthorised = person.IsAuthorised,
                IsEnabled = person.IsEnabled,
                ColourPreferences = colours
                    .Select(c => new ColourPreference
                    {
                        ColourId = c.ColourId,
                        ColourName = c.Name,
                        Favourite = person.FavouriteColours.Any(fc => fc.ColourId == c.ColourId)
                    }).ToList()
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
                var person = db.People.Find(editPersonModel.PersonId);

                if (person == null)
                {
                    return HttpNotFound();
                }

                person.IsAuthorised = editPersonModel.IsAuthorised;
                person.IsEnabled = editPersonModel.IsEnabled;

                if (editPersonModel.ColourPreferences != null)
                {
                    foreach (var colourPreference in editPersonModel.ColourPreferences)
                    {
                        if (colourPreference.Favourite)
                        {
                            if (!person.FavouriteColours.Any(c => c.ColourId == colourPreference.ColourId))
                            {
                                person.FavouriteColours.Add(db.Colours.Find(colourPreference.ColourId));
                            }
                        }
                        else
                        {
                            var favouriteColour = person.FavouriteColours.FirstOrDefault(fc => fc.ColourId == colourPreference.ColourId);

                            if (favouriteColour != null)
                            {
                                person.FavouriteColours.Remove(favouriteColour);
                            }
                            
                        }
                    }
                }
                else
                {
                    person.FavouriteColours = new List<Colour>();
                }

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
