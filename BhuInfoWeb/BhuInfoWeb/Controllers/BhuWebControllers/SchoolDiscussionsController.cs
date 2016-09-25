﻿using System;
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
        private readonly SchoolDiscussionDataContext _db = new SchoolDiscussionDataContext();
        private readonly SchoolDiscussionCommentDataContext _dbc = new SchoolDiscussionCommentDataContext();

        // GET: SchoolDiscussions
        public ActionResult Index()
        {
            return View(_db.SchoolDiscussions.ToList());
        }

        // GET: SchoolDiscussions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var schoolDiscussion = _db.SchoolDiscussions.Find(id);
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
            var discussionUpdate = _db.SchoolDiscussions.Find(Id);
            discussionUpdate.DiscussionView = discussionUpdate.DiscussionView + 1;
            _db.Entry(discussionUpdate).State = EntityState.Modified;
            _db.SaveChanges();
            return View("Activity", discussion);
        }

        public ActionResult GetDiscussionComments(long Id)
        {
            var discussion = _db.SchoolDiscussions.Find(Id);
            return PartialView("_ActivitySubComments", discussion);
        }
        public ActionResult GetCompleteDiscussionComments(long Id)
        {
            var discussion = _db.SchoolDiscussions.Find(Id);
            return PartialView("_ActivityComments", discussion);
        }
        // GET: SchoolDiscussions/CloseActivity
        public ActionResult CloseActivity(long Id)
        {
            var discussionUpdate = _db.SchoolDiscussions.Find(Id);
            discussionUpdate.Status = DiscussionState.Closed.ToString();
            _db.Entry(discussionUpdate).State = EntityState.Modified;
            _db.SaveChanges();
            TempData["discussion"] = "This Discussion has been closed!";
            TempData["notificationtype"] = NotificationType.Success.ToString();

            return RedirectToAction("Index", "SchoolDiscussions", _db.SchoolDiscussions.ToList());
        }

        // POST: SchoolDiscussions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
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
                    _db.SchoolDiscussions.Add(schoolDiscussion);
                    _db.SaveChanges();
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
            var schoolDiscussion = _db.SchoolDiscussions.Find(id);
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
                _db.Entry(schoolDiscussion).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolDiscussion);
        }

        // GET: SchoolDiscussions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var schoolDiscussion = _db.SchoolDiscussions.Find(id);
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
            var schoolDiscussion = _db.SchoolDiscussions.Find(id);
            _db.SchoolDiscussions.Remove(schoolDiscussion);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: SchoolDiscussion/CreateDiscussionComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDiscussionComment(
            [Bind(Include = "Comment")] SchoolDiscussionComment discussionComment,
            FormCollection collectedValues)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            discussionComment.SchoolDiscussionId = long.Parse(collectedValues["DiscussionId"]);
            var discussion = new SchoolDiscussionDataFactory().GetDiscussionById(discussionComment.SchoolDiscussionId);
            if (ModelState.IsValid)
            {
                string[] words = { "fuck", "Fuck", "4kin", "idiot", "pussy", "dick", "blowjob", "bastard", "stupid" };
                var comment = collectedValues["Comment"].ToLower();
                discussionComment.CommentBy = collectedValues["CommentBy"];
                discussionComment.Email = collectedValues["Email"];
                foreach (var item in words)
                    if (comment.Contains(item))
                    {
                        TempData["activity"] =
                            "Please check your words again and make sure your arent using any vulgar word!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return View("Activity", discussion);
                    }
                if (loggedinuser != null)
                {
                    discussionComment.CommentBy = loggedinuser.DisplayName;
                    discussionComment.Email = loggedinuser.Email;
                }
                discussionComment.DateCreated = DateTime.Now;
                discussionComment.SchoolDiscussionId = long.Parse(collectedValues["DiscussionId"]);
                _dbc.SchoolDiscussionComments.Add(discussionComment);
                _dbc.SaveChanges();
                ModelState.Clear();
                return PartialView("_ActivityComments",discussion);
            }

            return View("Activity", discussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}