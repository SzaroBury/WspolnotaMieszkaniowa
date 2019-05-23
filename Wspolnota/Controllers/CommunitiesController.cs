using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wspolnota.Models;
using Microsoft.AspNet.Identity;

namespace Wspolnota.Controllers
{
    public class CommunitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Communities
        public async Task<ActionResult> Index()
        {
            return View(await db.Communities.ToListAsync());
        }

        // GET: Communities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = await db.Communities.FindAsync(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // GET: Communities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Communities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "CommunityID,Name,Image")] Community community)
        {
            if (ModelState.IsValid)
            {
                community.Announcements = new List<Announcement>();
                community.Announcements.Add(new Announcement { Title = "Pierwsze ogłoszenie", Content = "Tekst ogłoszenia", Author = db.Users.Find(User.Identity.GetUserId()), CreatedAt = DateTime.Now });     
                db.Communities.Add(community);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(community);
        }

        // GET: Communities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = await db.Communities.FindAsync(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: Communities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CommunityID,Name,Image")] Community community)
        {
            if (ModelState.IsValid)
            {
                db.Entry(community).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(community);
        }

        // GET: Communities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = await db.Communities.FindAsync(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: Communities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Community community = await db.Communities.FindAsync(id);
            db.Communities.Remove(community);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Communities/Join/5
        [Authorize]
        public async Task<ActionResult> Join(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = await db.Communities.FindAsync(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: Communities/Join
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Join")]
        [ValidateAntiForgeryToken]
        public ActionResult Join([Bind(Include = "CommunityID,Name,Image")] int communityID)
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            if(user.Community != null) return RedirectToAction("Index", "Communities");
            if (ModelState.IsValid )
            {
                user.Community = db.Communities.Find(communityID);

                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
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
