using GitHubRepositoryTool.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitHubRepositoryTool.Controllers
{
    public class GitHubSearchController : Controller
    {
        private IBookmarkService bookmarkService;
        private ILogService logService;
        public GitHubSearchController(IBookmarkService bookmarkService, ILogService logService)
        {
            this.bookmarkService = bookmarkService;
            this.logService = logService;
        }

        #region Views
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bookmark()
        {
            return View();
        }

        #endregion

        #region HTTP
        /// <summary>
        /// Save repository in session
        /// </summary>
        /// <param name="repository">repository data</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveBookMark(string repository)
        {
            logService.NewRequest();
            JsonResult res = new JsonResult();
            res.Data = bookmarkService.AddRepositoryToSession(Session,repository);
            return res;
        }

        /// <summary>
        /// Get all saved repositories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllBookMarks()
        {
            logService.NewRequest();
            JsonResult res = new JsonResult();
            res.Data = bookmarkService.GetAllBookMarks(Session);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}