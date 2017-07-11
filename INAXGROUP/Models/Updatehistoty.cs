using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INAXGROUP.Models;
namespace INAXGROUP.Models
{
    public class Updatehistoty
    {
        public INAXGROUPContext db = new INAXGROUPContext();
        public static void UpdateHistory(string task,string FullName,string UserID)
        {

            INAXGROUPContext db = new INAXGROUPContext();
            tblHistoryLogin tblhistorylogin = new tblHistoryLogin();
            tblhistorylogin.FullName = FullName;
            tblhistorylogin.Task = task;
            tblhistorylogin.idUser = int.Parse(UserID);
            tblhistorylogin.DateCreate = DateTime.Now;
            tblhistorylogin.Active = true;
            
            db.tblHistoryLogins.Add(tblhistorylogin);
            db.SaveChanges();
           
        }
    }
}