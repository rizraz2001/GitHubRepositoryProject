using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubRepositoryTool.Services
{
    public class RequestService : IRequestService
    {
        public string GetRequestPath()
        {
            var request = HttpContext.Current.Request;
            var requestPath = request.FilePath;
            return requestPath;
        }

        public string GetRequestUserIp()
        {
            var request = HttpContext.Current.Request;
            string userIp;
            if (request.IsLocal)
                userIp = "localhost";
            else userIp = request.UserHostAddress;

            return userIp;
        }
    }
}