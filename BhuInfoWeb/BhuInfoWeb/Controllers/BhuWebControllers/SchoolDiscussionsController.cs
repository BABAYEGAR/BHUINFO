using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class SchoolDiscussionsController : Controller
    {
        private readonly SchoolDiscussionDataContext db = new SchoolDiscussionDataContext();
        private readonly SchoolDiscussionCommentDataContext dbc = new SchoolDiscussionCommentDataContext();

        // GET: SchoolDiscussions
        public ActionResult Index()
        {
            return View(db.SchoolDiscussions.ToList());
        }

        // GET: SchoolDiscussions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var schoolDiscussion = db.SchoolDiscussions.Find(id);
            if (schoolDiscussion == null)
                return HttpNotFound();
            return View(schoolDiscussion);
        }

        // GET: SchoolDiscussions/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: SchoolDiscussions/Create
        public ActionResult Activity(long Id)
        {
            var discussion = new SchoolDiscussionDataFactory().GetDiscussionById(Id);
            var discussionUpdate = db.SchoolDiscussions.Find(Id);
            discussionUpdate.DiscussionView = discussionUpdate.DiscussionView + 1;
            db.Entry(discussionUpdate).State = EntityState.Modified;
            db.SaveChanges();
            return View("Activity", discussion);
        }

        // GET: SchoolDiscussions/CloseActivity
        public ActionResult CloseActivity(long Id)
        {
            var discussionUpdate = db.SchoolDiscussions.Find(Id);
            discussionUpdate.Status = DiscussionState.Closed.ToString();
            db.Entry(discussionUpdate).State = EntityState.Modified;
            db.SaveChanges();
            TempData["discussion"] = "This Discussion has been closed!";
            TempData["notificationtype"] = NotificationType.Success.ToString();

            return RedirectToAction("Index", "SchoolDiscussions", db.SchoolDiscussions.ToList());
        }

        // POST: SchoolDiscussions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "SchoolDiscussionId,Topic,Content")] SchoolDiscussion schoolDiscussion)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    schoolDiscussion.DateCreated = DateTime.Now;
                    schoolDiscussion.DateLastModified = DateTime.Now;
                    schoolDiscussion.LastModifiedById = loggedinuser.AppUserId;
                    schoolDiscussion.Status = DiscussionState.Open.ToString();
                    schoolDiscussion.DiscussionView = 0;
                    db.SchoolDiscussions.Add(schoolDiscussion);
                    db.SaveChanges();
                    TempData["discussion"] = "A new discussion has been created!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                }
                else
                {
                    TempData["discussion"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View(schoolDiscussion);
        }

        // GET: SchoolDiscussions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var schoolDiscussion = db.SchoolDiscussions.Find(id);
            if (schoolDiscussion == null)
                return HttpNotFound();
            return View(schoolDiscussion);
        }

        // POST: SchoolDiscussions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                 Include =
                     "SchoolDiscussionId,Topic,Content,Status,CreatedById,LastModifiedById,DateCreated,DateLastModified,DiscussionView"
             )] SchoolDiscussion schoolDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolDiscussion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolDiscussion);
        }

        // GET: SchoolDiscussions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var schoolDiscussion = db.SchoolDiscussions.Find(id);
            if (schoolDiscussion == null)
                return HttpNotFound();
            return View(schoolDiscussion);
        }

        // POST: SchoolDiscussions/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var schoolDiscussion = db.SchoolDiscussions.Find(id);
            db.SchoolDiscussions.Remove(schoolDiscussion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: SchoolDiscussion/CreateDiscussionComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDiscussionComment(
            [Bind(Include = "CommentBy,Comment,Email")] SchoolDiscussionComment discussionComment,
            FormCollection collectedValues)
        {
            discussionComment.SchoolDiscussionId = long.Parse(collectedValues["DiscussionId"]);
            var discussion = new SchoolDiscussionDataFactory().GetDiscussionById(discussionComment.SchoolDiscussionId);
            if (ModelState.IsValid)
            {
                string[] words = {"fuck", "Fuck", "4kin"};
                var comment = collectedValues["Comment"];
                foreach (var item in words)
                    if (comment.Contains(item))
                    {
                        TempData["activity"] =
                            "Please check your words again and make sure your arent using any vulgar word!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return View("Activity", discussion);
                    }
                discussionComment.DateCreated = DateTime.Now;
                discussionComment.SchoolDiscussionId = long.Parse(collectedValues["DiscussionId"]);
                dbc.SchoolDiscussionComments.Add(discussionComment);
                dbc.SaveChanges();


                return RedirectToAction("Activity", "SchoolDiscussions", new {Id = discussionComment.SchoolDiscussionId});
            }

            return View("Activity", discussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}