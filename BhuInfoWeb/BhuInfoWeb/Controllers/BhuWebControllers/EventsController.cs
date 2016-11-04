using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers.BhuWebControllers
{
    public class EventsController : Controller
    {
        private readonly EventDataContext _db = new EventDataContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(_db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(string id)
        {
            var eventId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var @event = _db.Events.Find(eventId);
            if (@event == null)
                return HttpNotFound();
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventName,Venue,StartDate,EndDate,Organizer,DateCreated")] Event @event,FormCollection collectedValues)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    @event.DateCreated = DateTime.Now;
                    @event.DateLastModified  = DateTime.Now;
                    @event.CreatedById = loggedinuser.AppUserId;
                    @event.LastModifiedById = loggedinuser.AppUserId;
                    @event.StartTime = collectedValues["StartTime"];
                    @event.EndTime = collectedValues["EndTime"];
                    if (@event.EndDate < @event.StartDate)
                    {
                        TempData["event"] = "The End date cannot be less than the start date!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return View(@event);
                    }
                        _db.Events.Add(@event);
                        _db.SaveChanges();
                        TempData["event"] = "New event Successfully Created!";
                        TempData["notificationtype"] = NotificationType.Success.ToString();
                }
                else
                {
                    TempData["event"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(string id)
        {
            var eventId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var @event = _db.Events.Find(eventId);
            if (@event == null)
                return HttpNotFound();
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventName,Venue,StartDate,EndDate,Organizer,DateCreated")] Event @event,FormCollection collectedValues)
        {
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    @event.DateCreated = Convert.ToDateTime(collectedValues["date"]);
                    @event.CreatedById = long.Parse(collectedValues["createdby"]);
                    @event.DateLastModified = DateTime.Now;
                    @event.StartTime = collectedValues["StartTime"];
                    @event.EndTime = collectedValues["EndTime"];
                    @event.LastModifiedById = loggedinuser.AppUserId;
                    _db.Entry(@event).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["event"] = "This Event has been modified successfully!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                }
                else
                {
                    TempData["event"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(string id)
        {
            var eventId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var @event = _db.Events.Find(eventId);
            if (@event == null)
                return HttpNotFound();
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var eventId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(id, true));
            var @event = _db.Events.Find(eventId);
            _db.Events.Remove(@event);
            _db.SaveChanges();
            TempData["event"] = "Event has been deleted successfully!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}