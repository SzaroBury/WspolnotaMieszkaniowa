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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<Post> posts = new List<Post>();

        // GET: Posts
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var a = await db.Announcements.ToListAsync();
            var b = await db.Brochures.ToListAsync();
            var s = await db.Surveys.ToListAsync();

            posts.AddRange(a);
            posts.AddRange(b);
            posts.AddRange(s);

            return View(posts);
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await FindPostAsync(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostId,Title,AuthorId,CreatedAt,CommunityId")] Announcement post)
        {
            if (ModelState.IsValid)
            {
                db.Announcements.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostId,Title,AuthorId,CreatedAt,CommunityId")] Brochure post)
        {
            if (ModelState.IsValid)
            {
                db.Brochures.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostId,Title,AuthorId,CreatedAt,CommunityId")] Survey post)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await FindPostAsync(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            return View(post);
        }

        //POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostId,Title,AuthorId,CreatedAt,CommunityId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await FindPostAsync(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Announcements.FindAsync(id);
            if (post == null)
            {
                post = await db.Surveys.FindAsync(id);
                if (post == null)
                {
                    post = await db.Brochures.FindAsync(id);
                    db.Brochures.Remove((Brochure)post);
                }
                else db.Surveys.Remove((Survey)post);
            }
            else db.Announcements.Remove((Announcement)post);

            //Post post = await FindPostAsync(id);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<Post> FindPostAsync(int id)
        {
            Post post = await db.Announcements.FindAsync(id);
            if (post == null)
            {
                post = await db.Surveys.FindAsync(id);
                if (post == null) post = await db.Brochures.FindAsync(id);
            }
            return post;
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
