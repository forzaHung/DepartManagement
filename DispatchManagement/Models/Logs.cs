using Framework.EF;
using Dispatch;
using DispatchManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Models
{
    public class Logs
    {
        public static void logs( string description, string action, string link, int userid)
        {
            ITransactionLogs _transactionlogs;
            _transactionlogs = SingletonIpl.GetInstance<IplTransactionLogs>();
            TransactionLogsEntity translog = new TransactionLogsEntity();
            translog.Action = action;
            translog.Description = description;
            translog.Link = link;
            translog.LogDate = DateTime.Now;
            translog.UserId = userid;
            _transactionlogs.Insert(translog);
        }
    }
}