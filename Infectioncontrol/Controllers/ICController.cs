using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Infectioncontrol.Models;
using Infectioncontrol.Utility;

namespace Infectioncontrol.Controllers
{
    public class ICController : ApiController
    {
        //// GET: api/IC
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/IC/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/IC
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/IC/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/IC/5
        //public void Delete(int id)
        //{
        //}

        // GET: api/IC
        public List<IC> Get()
        {
            CDRU cdru = new CDRU();
            List<IC> allic = new List<IC>();
            allic = cdru.Get();
            return allic;
        }

        
        // GET: api/IC/5
        public IC Get(string Datetimenow, string Workerid)
        {
            string datetime = Datetimenow.ToString();
            string id = Workerid.ToString();
            CDRU cdru = new CDRU();
            IC oneic = new IC();
            oneic = cdru.Get(datetime, id);
            return oneic;

        }

        // POST: api/IC
        public int Post(string Datetimenow, string Workerid , string Workername , string Workerdepartment , string Workertemperature, string Cough, string Diarrhea)
        {
            CDRU cdru = new CDRU();
            int result = cdru.Post(Datetimenow, Workerid, Workername, Workerdepartment, Workertemperature, Cough, Diarrhea);
            return result;
        }

        // PUT: api/IC/5
        public int Put(string Datetimenow, string Workerid, string Workertemperature, string Cough, string Diarrhea)
        {
            CDRU cdru = new CDRU();
            int result = cdru.Put(Datetimenow, Workerid, Workertemperature, Cough, Diarrhea);
            return result;
        }

        // DELETE: api/IC/5
        public int Delete(string Datetimenow, string Workerid)
        {
            CDRU cdru = new CDRU();
            int result = cdru.Delete(Datetimenow, Workerid);
            return result;
        }
    }
}
