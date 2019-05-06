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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CommunityID,Name,Image")] Community community)
        {
            if (ModelState.IsValid)
            {
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
