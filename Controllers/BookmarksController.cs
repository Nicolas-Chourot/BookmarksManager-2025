using BookmarksManager.Models;
using JSON_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookmarksManager.Controllers
{
    public class BookmarksController : Controller
    {
        public ActionResult About()
        {
            Session["Action"] = "About";
            return View();
        }

        public ActionResult GetBookmarks(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Bookmarks.HasChanged)
            {
                return PartialView(DB.Bookmarks.ToList().OrderBy(c => c.Title).ToList());
            }
            return null;
        }

        public ActionResult Index()
        {
            Session["Action"] = "Index";
            return View();
        }

        public ActionResult Details(int id)
        {
            Session["Action"] = "Details";
            Session["CurrentId"] = id;
            Bookmark contact = DB.Bookmarks.Get(id);
            if (contact != null)
                return View(contact);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult TitleAvailable(string title)
        {
            int Id = (int)Session["CurrentId"];
            bool available = true;
            Bookmark bookmark = DB.Bookmarks.ToList().Where(c => c.Title.ToLower() == title.ToLower()).FirstOrDefault();
            if (bookmark == null) available = true;
            else
                if (bookmark.Id != Id) available = bookmark.Title.ToLower() != title.ToLower();
            return Json(available);
        }

        public ActionResult Create()
        {
            Session["CurrentId"] = 0;
            Session["Action"] = "Create";
            return View(new Bookmark());
        }

        [HttpPost]
        public ActionResult Create(Bookmark bookmark)
        {
            try
            {
                DB.Bookmarks.Add(bookmark);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit()
        {
            Session["Action"] = "Edit";
            int id = (int)Session["CurrentId"];
            Bookmark bookmark = DB.Bookmarks.Get(id);
            if (bookmark != null)
                return View(bookmark);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Bookmark bookmark)
        {
            try
            {
                bookmark.Id = (int)Session["CurrentId"];
                DB.Bookmarks.Update(bookmark);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete()
        {
            Bookmark bookmark = DB.Bookmarks.Get((int)Session["CurrentId"]);
            if (bookmark != null)
                DB.Bookmarks.Delete(bookmark.Id);
            return RedirectToAction("Index");
        }
    }
}