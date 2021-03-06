﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAXGROUP.Models;
using System.Text;
namespace INAXGROUP.Controllers.Display.Footter
{
    public class FootterController : Controller
    {
        private INAXGROUPContext db = new INAXGROUPContext();
        // GET: Footter
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ControlFootter()
        {
            tblConfig tblconfig = db.tblConfigs.First();
            var Url = db.tblUrls.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder chuoi = new StringBuilder();
            for (int i = 0; i < Url.Count;i++ )
            {
                if(Url[i].Rel==true)
                { chuoi.Append("<a href=\"" + Url[i].Url + "\" title=\"" + Url[i].Name + "\" rel=\"nofollow\">" + Url[i].Name + "</a>"); }
                else
                chuoi.Append("<a href=\"" + Url[i].Url + "\" title=\"" + Url[i].Name + "\">" + Url[i].Name + "</a>");
            }
            ViewBag.chuoi = chuoi;
            var maps = db.tblMaps.First();
            ViewBag.maps = maps.Content;
            var Imagesadw = db.tblImages.Where(p => p.Active == true && p.idCate == 10).OrderByDescending(p => p.Ord).Take(1).ToList();
            if (Imagesadw.Count > 0)
                ViewBag.Chuoiimg = "<a href=\"" + Imagesadw[0].Url + "\" title=\"" + Imagesadw[0].Name + "\"><img src=\"" + Imagesadw[0].Images + "\" alt=\"" + Imagesadw[0].Name + "\" style=\"max-width:100%;\" /> </a>";
            return PartialView(tblconfig);
        }
        public ActionResult Command(FormCollection collection, tblRegister registry)
        {
             string Name = collection["txtName"];
                string Hotline = collection["txtHotline"];
                string selectcate = collection["selectcate"];
                registry.Name = Name;
                registry.Mobile = Hotline;
                registry.idCate = int.Parse(selectcate);
                     db.tblRegisters.Add(registry);
                    db.SaveChanges();
                    Session["registry"] = "<script>$(document).ready(function(){ alert('Bạn đã đăng ký thành công') });</script>";
                 
            
            return Redirect("/Default/Index");
        }
        public PartialViewResult MenuMobine()
        {
            var listGroup = db.tblGroupProducts.Where(p => p.Active == true && p.Priority == true).OrderBy(p => p.Ord).ToList();
            string chuoimenu = "";
            for (int i = 0; i < listGroup.Count; i++)
            {

                string tag = listGroup[i].Tag;

                chuoimenu += "<a href=\"/0/" + tag + "\" title=\"" + listGroup[i].Name + "\">" + listGroup[i].Name + "</a>";

            }
            ViewBag.chuimenu = chuoimenu;
            StringBuilder chuoi = new StringBuilder();
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID==null).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                chuoi.Append("<li><a href=\"/" + ListMenu[i].Tag + "\" title=\"" + ListMenu[i].Name + "\">" + ListMenu[i].Name + "</a>");
                int idCate = ListMenu[i].id;
                var listmenuchild = db.tblGroupProducts.Where(p => p.ParentID==idCate & p.Active == true).OrderBy(p => p.Ord).ToList();
                if (listmenuchild.Count > 0)
                {
                    chuoi.Append("<ul>");
                    for (int j = 0; j < listmenuchild.Count; j++)
                    {
                        chuoi.Append("<li><a href=\"/0/" + listmenuchild[j].Tag + "\" title=\"" + listmenuchild[j].Name + "\">" + listmenuchild[j].Name + "</a></li>");
                    }
                    chuoi.Append("</ul>");
                }
                chuoi.Append("</li>");
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
    }
}