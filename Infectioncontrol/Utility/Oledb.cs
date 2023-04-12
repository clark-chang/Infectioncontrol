using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace Infectioncontrol.Utility
{
    public class Oledb
    {
        private string connstring = /**/
        
        public OleDbConnection GetOleDbConnection(string ds)
        {
            /**/
            return new OleDbConnection(connstring);
        }
    }
}