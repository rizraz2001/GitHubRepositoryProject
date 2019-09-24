using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace GitHubRepositoryTool.Services
{
    public class LogService : ILogService
    {
        private Logger logger;
        private IRequestService requestService;

        public LogService(IRequestService requestService)
        {
            this.requestService = requestService;
            logger = LogManager.GetLogger("AL");
            logger.Info("Log service has initialize...");
        }

        public void AddDuplicateRepository(string id)
        {
            string message = $"tried to add duplicate bookmark of id : {id}";
            PrintLog(message);
        }

        private void PrintLog(string message)
        {
            logger.Info($"ip : {requestService.GetRequestUserIp()}, message : {message}");
        }

        public void AddRepositoryToSessionException(string message)
        {
            PrintLog($"error in  AddRepositoryToSession : {message}");
        }

        public void GetAllBookMarksException(string message)
        {
            PrintLog($"error in GetAllBookMarksException : {message}");
        }

        public void NewRequest()
        {
            string message = $"new request : path : {requestService.GetRequestPath()}";
            PrintLog(message);
        }
      
    }
}