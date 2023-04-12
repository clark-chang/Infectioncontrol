using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infectioncontrol.Models
{
    public class IC
    {
        public string Workerid { get; set; }
        public string Workername { get; set; }
        public string Workerdepartment { get; set; }
        public string Workertemperature { get; set; }
        public string Cough { get; set; }
        public string Diarrhea { get; set; }
        public string Datetimenow { get; set; }
    }
}