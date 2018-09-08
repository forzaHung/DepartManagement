using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement
{
    public class FileImport
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public int ProductCount { get; set; }
        public int SuccessCount { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
    }
}