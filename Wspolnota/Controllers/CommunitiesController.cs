using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Wspolnota.Models;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace Wspolnota.Controllers
{
    public class CommunitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Communities
        public async Task<ActionResult> Index()
        {
            ViewData["user"] = db.Users.Find(User.Identity.GetUserId());
            return View(await db.Communities.Include(c => c.Users).ToListAsync());
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
                community.Posts = new List<Post>
                {
                    new Announcement    { Title = "Pierwsze ogłoszenie", Author = db.Users.Find(User.Identity.GetUserId()), CreatedAt = DateTime.Now, Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec non nunc hendrerit, auctor diam eleifend, malesuada elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Phasellus lobortis congue sem. Maecenas viverra massa vitae ante egestas gravida. In ante turpis, pretium at nisi sit amet, sagittis venenatis sapien. Maecenas ultrices, eros ac posuere eleifend, dui ipsum luctus nunc, et euismod mauris orci tempor tortor. Praesent mollis ante quis orci eleifend scelerisque eget pellentesque eros. Nulla eget urna erat. Duis nec arcu hendrerit tortor sollicitudin tempor in eget justo." },
                    new Survey          { Title = "Pierwsza ankieta - Co było pierwsze, jajko czy kura?", Author = db.Users.Find(User.Identity.GetUserId()), CreatedAt = DateTime.Now, Answers = new List<Answer>{ new Answer { Content = "Jajko"} , new Answer { Content = "Kura" } }  },
                    new Brochure        { Title = "Pierwsza reklama", Author = db.Users.Find(User.Identity.GetUserId()), CreatedAt = DateTime.Now, Image = @"https://pbs.twimg.com/media/DtxH42HXQAAb5Ks.jpg", Link = @"https://www.google.com" }
                };
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
            List<Survey> surveys = await db.Surveys.Where(a => a.CommunityId == id).ToListAsync();

            db.Votes.RemoveRange(await db.Votes.Include("Answer.Survey").Where(v => v.Answer.Survey.CommunityId == id).ToListAsync());
            db.Answers.RemoveRange(await db.Answers.Include("Survey").Where(ans => ans.Survey.CommunityId == id).ToListAsync());
            db.Surveys.RemoveRange(await db.Surveys.Where(s => s.CommunityId == id).ToListAsync());
            db.Announcements.RemoveRange(await db.Announcements.Where(a => a.CommunityId == id).ToListAsync());
            db.Brochures.RemoveRange(await db.Brochures.Where(a => a.CommunityId == id).ToListAsync());
            db.Communities.Remove(await db.Communities.FindAsync(id));
            

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
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Include(u => u.Communities).Where(u => u.Id == userId).First();
                Community community = db.Communities.Find(communityID);
                if (user.Communities.Contains(community)) return RedirectToAction("Index", "Communities");

                user.Communities.Add(community);
                community.Users.Add(user);
                
                db.Entry(user).State = EntityState.Modified;
                db.Entry(community).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Posts");
            }
            return View();
        }

        // GET: Communities/Enter/5
        [Authorize]
        public async Task<ActionResult> Enter(int? id)
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
            return RedirectToAction("Index", "Posts");
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
