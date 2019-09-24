using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubRepositoryTool.Services
{
    public interface ILogService
    {
        void NewRequest();
        void AddRepositoryToSessionException(string message);
        void AddDuplicateRepository(string id);
        void GetAllBookMarksException(string message);
    }
}