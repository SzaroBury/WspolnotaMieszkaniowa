using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Wspolnota.Models;
using Microsoft.AspNet.Identity;
using System;

namespace Wspolnota.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<Post> posts = new List<Post>();

        // GET: Posts
        [Authorize]
        public async Task<ActionResult> Index(int? id)
        {
            if(!id.HasValue) return RedirectToAction("Index", "Communities");
            string userId = User.Identity.GetUserId();
            if (!db.Users.Where(u => u.Id == userId).Include("Communities").FirstOrDefault().Communities.Select(c => c.CommunityID).Contains(id.Value)) return RedirectToAction("Index", "Communities");

            //var a = await db.Announcements.Where(a2 => db.Users.Where(u => u.Id == userId).FirstOrDefault().Communities.Select(c => c.CommunityID).Contains(a2.CommunityId)).ToListAsync();
            //var b = await db.Brochures.Where(a2 => db.Users.Where(u => u.Id == userId).FirstOrDefault().Communities.Select(c => c.CommunityID).Contains(a2.CommunityId)).ToListAsync();
            //var s = await db.Surveys.Include("Answers").Where(a2 => db.Users.Where(u => u.Id == userId).FirstOrDefault().Communities.Select(c => c.CommunityID).Contains(a2.CommunityId)).ToListAsync();

            posts.AddRange(await db.Announcements.Where(a => a.CommunityId == id).ToListAsync());
            posts.AddRange(await db.Brochures.Where(b => b.CommunityId == id).ToListAsync());
            posts.AddRange(await db.Surveys.Where(s => s.CommunityId == id).Include("Answers").Include(s=>s.Votes).ToListAsync());

            ViewData["userId"] = userId;
            return View(posts.OrderByDescending(p => p.CreatedAt.Date).ThenByDescending(p => p.CreatedAt.TimeOfDay).ToList());
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await FindPostAsync(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewData["returnUrl"] = HttpContext.Request.Url.AbsolutePath;
            ViewData["userId"] = User.Identity.GetUserId();
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create(int id)
        {
            //ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name");
            ViewData["id"] = id;
            return View();
        }

        // GET: Posts/Create
        public ActionResult CreateAnnouncement(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAnnouncement([Bind(Include = "Title, Content, CommunityId")] Announcement post)
        {
            post.Author = db.Users.Find(User.Identity.GetUserId());
            post.AuthorId = post.Author.Id;
            post.Community = db.Communities.Find(post.CommunityId);
            post.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Announcements.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = post.CommunityId});
            }

            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            return View(post);
        }

        public ActionResult CreateBrochure(int id)
        {
            ViewData["id"] = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBrochure([Bind(Include = "Title, Image, Link, CommunityId")] Brochure post)
        {
            post.Author = db.Users.Find(User.Identity.GetUserId());
            post.AuthorId = post.Author.Id;
            post.Community = db.Communities.Find(post.CommunityId);
            post.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Brochures.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            return View(post);
        }

        public ActionResult CreateSurvey(int id)
        {
            ViewData["id"] = id;
            return View(new Survey());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSurvey([Bind(Include = "Title, CommunityID")] Survey post, List<string> Answers )
        {
            post.Author = db.Users.Find(User.Identity.GetUserId());
            post.AuthorId = post.Author.Id;
            post.Community = db.Communities.Find(post.CommunityId);
            post.CreatedAt = DateTime.Now;
            foreach(string a in Answers) post.Answers.Add(new Answer { Content = a, Survey = post, SurveyId = post.PostId });

            if (ModelState.IsValid)
            {
                db.Surveys.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateVote(int AnswerId)
        {
            Answer answer = await db.Answers.Select(a=>a).Include(a=>a.Survey).Where(a=>a.AnswerId == AnswerId).FirstAsync();
            Vote vote = new Vote
            {
                AuthorId = User.Identity.GetUserId(),
                Author = db.Users.Find(User.Identity.GetUserId()),
                AnswerId = answer.AnswerId,
                Answer = answer
            };
            if (ModelState.IsValid)
            {
                db.Surveys.Select(s => s).Where(s => s.PostId == answer.Survey.PostId).FirstOrDefaultAsync().Result.Votes.Add(vote);
                db.Votes.Add(vote);
                await db.SaveChangesAsync();
                
            }
            if(HttpContext.Request.Url.AbsolutePath.Substring(7, 5) == "Detai") return RedirectToAction("Details", new { id = answer.Survey.PostId });
            else return RedirectToAction("Index", new { id = answer.Survey.CommunityId});
            //ViewBag.CommunityId = new SelectList(db.Communities, "CommunityID", "Name", post.CommunityId);
            //return View(post);
        }

        //// GET: Posts/Edit/5
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

            if (post.GetType().Name.ToString() == "Announcement") return View("EditAnnouncement", post);
            else if (post.GetType().Name.ToString() == "Survey") return View("EditSurvey", post);
            else return View("EditBrochure", post); // (post.GetType().Name.ToString() == "Brochure")
        }

        //POST: Posts/EditAnnouncement/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAnnouncement([Bind(Include = "PostId, AuthorId, CommunityId, CreatedAt, Title, Content")] Announcement post)
        {
            if (ModelState.IsValid)
            {
                post.Author = db.Users.Find(post.AuthorId);
                post.Community = db.Communities.Find(post.CommunityId);
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = post.CommunityId });
            }
            return View(post);
        }
        //POST: Posts/EditBrochure/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBrochure([Bind(Include = "PostId, AuthorId, CommunityId, CreatedAt, Title, Link, Image")] Brochure post)
        {
            if (ModelState.IsValid)
            {
                post.Author = db.Users.Find(post.AuthorId);
                post.Community = db.Communities.Find(post.CommunityId);
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = post.CommunityId });
            }
            return View(post);
        }
        //POST: Posts/EditAnnouncement/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSurvey([Bind(Include = "PostId, AuthorId, CommunityId, CreatedAt, Title")] Survey post, List<string> Answers)
        {
            if (ModelState.IsValid)
            {
                foreach (string a in Answers) post.Answers.Add(new Answer { Content = a, Survey = post, SurveyId = post.PostId });
                post.Author = db.Users.Find(post.AuthorId);
                post.Community = db.Communities.Find(post.CommunityId);
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = post.CommunityId });
            }
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
            int communityId = db.Communities.Select(c => c).Include(c => c.Posts).Where(c => c.Posts.Any(p => p.PostId == id)).Select(c => c.CommunityID).FirstOrDefault();
            Post post = await db.Announcements.Select(a => a).Where(a => a.PostId == id).Include(s => s.Author).Include(s => s.Community).FirstOrDefaultAsync();
            if (post == null)
            {
                post = await db.Surveys.Select(s => s).Where(s => s.PostId == id).Include(s => s.Answers).Include(s => s.Votes).Include(s => s.Author).Include(s => s.Community).FirstOrDefaultAsync();
                if (post == null)
                {
                    post = await db.Brochures.Select(b => b).Where(b => b.PostId == id).Include(s => s.Author).Include(s => s.Community).FirstOrDefaultAsync();
                    db.Brochures.Remove((Brochure)post);
                }
                else db.Surveys.Remove((Survey)post);
            }
            else db.Announcements.Remove((Announcement)post);

            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = communityId });
        }

        private async Task<Post> FindPostAsync(int id)
        {
            Post post = await db.Announcements.Select(a => a).Where(a => a.PostId == id).Include(s => s.Author).Include(s => s.Community).FirstOrDefaultAsync();
            if (post == null)
            {
                post = await db.Surveys.Select(s => s).Where(s => s.PostId == id).Include(s => s.Answers).Include(s => s.Votes).Include(s => s.Author).Include(s => s.Community).FirstOrDefaultAsync();
                if (post == null) post = await db.Brochures.Select(b => b).Where(b => b.PostId == id).Include(s => s.Author).Include(s => s.Community).FirstOrDefaultAsync();
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
