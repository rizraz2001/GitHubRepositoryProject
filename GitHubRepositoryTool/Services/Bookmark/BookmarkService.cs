using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubRepositoryTool.Services
{
    public class BookmarkService : IBookmarkService
    {
        private ILogService logService;
        public BookmarkService(ILogService logService)
        {
            this.logService = logService;
        }

        public string AddRepositoryToSession(HttpSessionStateBase session, string repository)
        {
            string res = "ok";
            List<string> bookMarks = new List<string>();
            try
            {
                if (session["bookmarks"] != null)
                {
                    bookMarks = (List<string>)session["bookmarks"];
                    if (IsDuplicatedBookmark(bookMarks, repository))
                    {
                        return "bookmark already exist in the bookmarks";
                    }
                }
                bookMarks.Add(repository);
                session["bookmarks"] = bookMarks;
            }
            catch(Exception e)
            {
                res = e.Message;
            }
            return res;
        }

        public List<string> GetAllBookMarks(HttpSessionStateBase session)
        {
            List<string> bookMarks = new List<string>();
            try
            {
                if (session["bookmarks"] != null)
                {
                    bookMarks = (List<string>)session["bookmarks"];
                }
            }
            catch(Exception e)
            {
                logService.GetAllBookMarksException(e.Message);
            }
            return bookMarks;
        }

        private bool IsDuplicatedBookmark(List<string> bookmarks, string repository)
        {
            var repositoryId = JObject.Parse(repository)["id"].ToString();
            foreach (var rep in bookmarks)
            {
                string id = JObject.Parse(rep)["id"].ToString();
                if (id == repositoryId)
                {
                    logService.AddDuplicateRepository(id);
                    return true;
                }
                   
            }
            return false;
        }
    }
}