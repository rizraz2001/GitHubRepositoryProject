using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubRepositoryTool.Services
{
    public interface IRequestService
    {
        string GetRequestUserIp();
        string GetRequestPath();
    }
}