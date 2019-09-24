using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubRepositoryTool.Services
{
    public interface IBookmarkService
    {
        string AddRepositoryToSession(HttpSessionStateBase session,string repository);
        List<string> GetAllBookMarks(HttpSessionStateBase session);
    }
}