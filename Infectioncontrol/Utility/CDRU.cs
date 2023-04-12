using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using Infectioncontrol.Models;

namespace Infectioncontrol.Utility
{
    public class CDRU
    {
        public List<IC> Get()
        {
            Oledb oledb = new Oledb();
            OleDbConnection odcnn = oledb.GetOleDbconnection("ORACLE_DB_HO", "ic");
            OleDbCommand odcmm = new OleDbCommand();
            OleDbDataAdapter oddap = null;
            DataTable alldata = new DataTable();

            string sql = @"  SELECT Trim(datetimenow)         AS ""datetimenow"",         " +
                         @"         Trim(workerid)            AS ""workerid"",            " +
                         @"         Trim(workername)          AS ""workername"",          " +
                         @"         Trim(workerdepartment)    AS ""workerdepartment"",    " +
                         @"         Trim(workertemperature)   AS ""workertemperature"",   " +
                         @"         Trim(cough)               AS ""cough"",               " +
                         @"         Trim(diarrhea)            AS ""diarrhea""             " +
                         @"  FROM   infectioncontrol                                      " +
                         @"  GROUP  BY workerid,                                          " +
                         @"            workername,                                        " +
                         @"            workerdepartment,                                  " +
                         @"            workertemperature,                                 " +
                         @"            cough,                                             " +
                         @"            diarrhea,                                          " +
                         @"            datetimenow                                        " ;


            odcmm.Connection = odcnn;
            odcmm.CommandType = CommandType.Text;
            odcmm.CommandText = sql;

            try
            {
                odcnn.Open();
                oddap = new OleDbDataAdapter(odcmm);
                oddap.Fill(alldata);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (odcnn != null)
                {
                    odcnn.Dispose();
                }
                if (odcmm != null)
                {
                    odcmm.Dispose();
                }
            }
            List<IC> allic = new List<IC>();
            //For Loop
            for (int i = 0; i < alldata.Rows.Count; i++)
            {
                IC ic = new IC();
                ic.Datetimenow = alldata.Rows[i]["datetimenow"].ToString();
                ic.Workerid = alldata.Rows[i]["workerid"].ToString();
                ic.Workername = alldata.Rows[i]["workername"].ToString();
                ic.Workerdepartment = alldata.Rows[i]["workerdepartment"].ToString();
                ic.Workertemperature = alldata.Rows[i]["workertemperature"].ToString();
                ic.Cough = alldata.Rows[i]["cough"].ToString();
                ic.Diarrhea = alldata.Rows[i]["diarrhea"].ToString();
                allic.Add(ic);
             }
            allic = allic.OrderByDescending(x => x.Datetimenow).ToList();

            //LINQ
            //allic = (from DataRow dr in alldata.Rows
            //         select new IC()
            //         {
            //             Datetimenow = dr["datetimenow"].ToString(),
            //             Workerid = dr["workerid"].ToString(),
            //             Workername = dr["workername"].ToString(),
            //             Workerdepartment = dr["workerdepartment"].ToString(),
            //             Workertemperature = dr["workertemperature"].ToString(),
            //             Cough = dr["cough"].ToString(),
            //             Diarrhea = dr["diarrhea"].ToString()
            //         }).OrderByDescending(x => x.Datetimenow).ToList();

            return allic;
        }

        public IC Get(string datetime, string id)
        {
            Oledb oledb = new Oledb();
            OleDbConnection odcnn = oledb.GetOleDbconnection("ORACLE_DB_HO", "ic");
            OleDbCommand odcmm = new OleDbCommand();
            OleDbDataAdapter oddap = null;
            DataTable alldata = new DataTable();
            OleDbDataReader oddrd = null;
            IC oneic = new IC();
            string sql = 
                            @"      SELECT Trim(datetimenow)         AS ""datetimenow"",             " +
                            @"             Trim(workerid)            AS ""workerid"",                " +
                            @"             Trim(workername)          AS ""workername"",              " +
                            @"             Trim(workerdepartment)    AS ""workerdepartment"",        " +
                            @"             Trim(workertemperature)   AS ""workertemperature"",       " +
                            @"             Trim(cough)               AS ""cough"",                   " +
                            @"             Trim(diarrhea)            AS ""diarrhea""                 " +
                            @"      FROM infectioncontrol                                            " +
                            @"      WHERE 1 = 1                                                      " +
                            @"          AND datetimenow = '{0}'                                      " +
                            @"          AND workerid = '{1}'                                         " +
                            @"          GROUP BY workerid,                                           " +
                            @"                   workername,                                         " +
                            @"                   workerdepartment,                                   " +
                            @"                   workertemperature,                                  " +
                            @"                   cough,                                              " +
                            @"                   diarrhea,                                           " +
                            @"                   datetimenow                                         " ;

            sql = string.Format(sql, datetime, id);

            try
            {
                //odcnn.Open();
                //oddap = new OleDbDataAdapter(odcmm);
                //oddap.Fill(alldata);
                odcnn.Open();
                odcmm.Connection = odcnn;
                odcmm.CommandType = CommandType.Text;
                odcmm.CommandText = sql;
                oddrd = odcmm.ExecuteReader();
                
                while (oddrd.Read())
                {
                    oneic = new IC()
                    {
                        Datetimenow = oddrd.GetValue(0).ToString(),
                        Workerid = oddrd.GetValue(1).ToString(),
                        Workername = oddrd.GetValue(2).ToString(),
                        Workerdepartment = oddrd.GetValue(3).ToString(),
                        Workertemperature = oddrd.GetValue(4).ToString(),
                        Cough = oddrd.GetValue(5).ToString(),
                        Diarrhea = oddrd.GetValue(6).ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oddrd != null)
                {
                    oddrd.Dispose();
                }
                if (odcmm != null)
                {
                    odcmm.Dispose();
                }
                if (odcnn != null)
                {
                    odcnn.Dispose();
                }
            }
            return oneic;
        }

        //    public List<IC> Get(string department, string date1, string date2)
        //    {
        //        Oledb oledb = new Oledb();
        //        OleDbConnection odcnn = oledb.GetOleDbconnection("ORACLE_DB_HO", "ic");
        //        OleDbCommand odcmm = new OleDbCommand();
        //        OleDbDataAdapter oddap = null;
        //        DataTable alldata = new DataTable();

        //        string sql = @"  SELECT Substr(datetimenow, 1, 8) AS ""datetimenow"",           " +
        //                     @"         Trim(workerid)            AS ""workerid"",              " +
        //                     @"         Trim(workername)          AS ""workername"",            " +
        //                     @"         Trim(workerdepartment)    AS ""workerdepartment"",      " +
        //                     @"         Trim(workertemperature)   AS ""workertemperature"",     " +
        //                     @"         Trim(cough)               AS ""cough"",                 " +
        //                     @"         Trim(diarrhea)            AS ""diarrhea""               " +
        //                     @"  FROM   infectioncontrol                                        " +
        //                     @"  WHERE  1 = 1                                                  " +
        //                     @"          AND workerdepartment = '{0}'                           " +
        //                     @"          AND Substr(datetimenow, 1, 8) BETWEEN '{1}' AND '{2}'  " +
        //                     @"  GROUP  BY workerid,                                            " +
        //                     @"            workername,                                          " +
        //                     @"            workerdepartment,                                    " +
        //                     @"            workertemperature,                                   " +
        //                     @"            cough,                                               " +
        //                     @"            diarrhea,                                            " +
        //                     @"            datetimenow                                          " ;
        //        sql = string.Format(sql, department, date1, date2);
        //        odcmm.Connection = odcnn;
        //        odcmm.CommandType = CommandType.Text;
        //        odcmm.CommandText = sql;

        //        try
        //        {
        //            odcnn.Open();
        //            oddap = new OleDbDataAdapter(odcmm);
        //            oddap.Fill(alldata);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            if (odcnn != null)
        //            {
        //                odcnn.Dispose();
        //            }
        //            if (odcmm != null)
        //            {
        //                odcmm.Dispose();
        //            }
        //        }
        //        List<IC> allic = new List<IC>();
        //        //For Loop
        //        for (int i = 0; i < alldata.Rows.Count; i++)
        //        {
        //            IC ic = new IC();
        //            ic.Datetimenow = alldata.Rows[i]["datetimenow"].ToString();
        //            ic.Workerid = alldata.Rows[i]["workerid"].ToString();
        //            ic.Workername = alldata.Rows[i]["workername"].ToString();
        //            ic.Workerdepartment = alldata.Rows[i]["workerdepartment"].ToString();
        //            ic.Workertemperature = alldata.Rows[i]["workertemperature"].ToString();
        //            ic.Cough = alldata.Rows[i]["cough"].ToString();
        //            ic.Diarrhea = alldata.Rows[i]["diarrhea"].ToString();
        //            allic.Add(ic);
        //        }
        //        allic = allic.OrderByDescending(x => x.Datetimenow).ToList();

        //        //LINQ
        //        //allic = (from DataRow dr in alldata.Rows
        //        //         select new IC()
        //        //         {
        //        //             Datetimenow = dr["datetimenow"].ToString(),
        //        //             Workerid = dr["workerid"].ToString(),
        //        //             Workername = dr["workername"].ToString(),
        //        //             Workerdepartment = dr["workerdepartment"].ToString(),
        //        //             Workertemperature = dr["workertemperature"].ToString(),
        //        //             Cough = dr["cough"].ToString(),
        //        //             Diarrhea = dr["diarrhea"].ToString()
        //        //         }).OrderByDescending(x => x.Datetimenow).ToList();

        //        return allic;
        //    }
        //

        public int Post(string datetime, string id, string name, string department, string temperature, string cough, string diarrhea)
        {
            Oledb oledb = new Oledb();
            OleDbConnection odcnn = oledb.GetOleDbconnection("ORACLE_DB_HO", "ic");
            OleDbCommand odcmm = new OleDbCommand();
            OleDbDataAdapter oddap = null;
            DataTable alldata = new DataTable();
            OleDbDataReader oddrd = null;
            IC oneic = new IC();
            int result;
            string sql =
                           @"  INSERT INTO INFECTIONCONTROL  " +
                           @"  (DATETIMENOW,                 " +
                           @"   WORKERID,                    " +
                           @"   WORKERNAME,                  " +
                           @"   WORKERDEPARTMENT,            " +
                           @"   WORKERTEMPERATURE,           " +
                           @"   COUGH,                       " +
                           @"   DIARRHEA)                    " +
                           @"  VALUES                        " +
                           @"  ('{0}',                       " +
                           @"   '{1}',                       " +
                           @"   '{2}',                       " +
                           @"   '{3}',                       " +
                           @"   '{4}',                       " +
                           @"   '{5}',                       " +
                           @"   '{6}')                       " ;
           
            
            sql = string.Format(sql, datetime, id, name, department, temperature, cough, diarrhea);

            try
            {
                //odcnn.Open();
                //oddap = new OleDbDataAdapter(odcmm);
                //oddap.Fill(alldata);
                odcnn.Open();
                odcmm.Connection = odcnn;
                odcmm.CommandType = CommandType.Text;
                odcmm.CommandText = sql;
                result = odcmm.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oddrd != null)
                {
                    oddrd.Dispose();
                }
                if (odcmm != null)
                {
                    odcmm.Dispose();
                }
                if (odcnn != null)
                {
                    odcnn.Dispose();
                }
            }
            return result;
        }

        public int Put(string datetime, string id, string temperature, string cough, string diarrhea)
        {
            Oledb oledb = new Oledb();
            OleDbConnection odcnn = oledb.GetOleDbconnection("ORACLE_DB_HO", "ic");
            OleDbCommand odcmm = new OleDbCommand();
            OleDbDataAdapter oddap = null;
            DataTable alldata = new DataTable();
            OleDbDataReader oddrd = null;
            IC oneic = new IC();
            int result;
            string sql =
                          @"    UPDATE INFECTIONCONTROL                          " +
                          @"    SET    WORKERTEMPERATURE = '{2}',                " +
                          @"           COUGH = '{3}',                            " +
                          @"           DIARRHEA = '{4}'                          " +
                          @"    WHERE  1 = 1                                     " +
                          @"           AND DATETIMENOW = '{0}'                   " +
                          @"           AND WORKERID = '{1}'                      " ;

            sql = string.Format(sql, datetime, id, temperature, cough, diarrhea);

            try
            {
                //odcnn.Open();
                //oddap = new OleDbDataAdapter(odcmm);
                //oddap.Fill(alldata);
                odcnn.Open();
                odcmm.Connection = odcnn;
                odcmm.CommandType = CommandType.Text;
                odcmm.CommandText = sql;
                result = odcmm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oddrd != null)
                {
                    oddrd.Dispose();
                }
                if (odcmm != null)
                {
                    odcmm.Dispose();
                }
                if (odcnn != null)
                {
                    odcnn.Dispose();
                }
            }
            return result;
        }

        public int Delete(string datetime, string id)
        {
            Oledb oledb = new Oledb();
            OleDbConnection odcnn = oledb.GetOleDbconnection("ORACLE_DB_HO", "ic");
            OleDbCommand odcmm = new OleDbCommand();
            OleDbDataAdapter oddap = null;
            DataTable alldata = new DataTable();
            OleDbDataReader oddrd = null;
            IC oneic = new IC();
            int result;
            string sql =
                           @"  DELETE FROM INFECTIONCONTROL           " +
                           @"  WHERE 1 = 1                            " +
                           @"  AND DATETIMENOW = '{0}'                " +
                           @"  AND WORKERID = '{1}'                   " ;

            sql = string.Format(sql, datetime, id);

            try
            {
                //odcnn.Open();
                //oddap = new OleDbDataAdapter(odcmm);
                //oddap.Fill(alldata);
                odcnn.Open();
                odcmm.Connection = odcnn;
                odcmm.CommandType = CommandType.Text;
                odcmm.CommandText = sql;
                result = odcmm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oddrd != null)
                {
                    oddrd.Dispose();
                }
                if (odcmm != null)
                {
                    odcmm.Dispose();
                }
                if (odcnn != null)
                {
                    odcnn.Dispose();
                }
            }
            return result;
        }
    } 
}
