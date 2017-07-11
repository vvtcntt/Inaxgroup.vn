using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAXGROUP.Models;
using System.Text;

namespace INAXGROUP.Controllers.Display.Header
{
    public class HeaderController : Controller
    {
        private INAXGROUPContext db = new INAXGROUPContext();
        // GET: Header
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ControlHeader()
        {
            var tblconfig = db.tblConfigs.First();
            return PartialView(tblconfig);
        }
        public PartialViewResult PartialSearch()
        {
            return PartialView();
        }
        public PartialViewResult PartialnVar()
        {
            return PartialView();
        }
      public PartialViewResult PartialMenu()
        {
            var listParent = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID==null).OrderBy(p => p.Ord).ToList();
            StringBuilder chuoi = new StringBuilder();
            chuoi.Append("<div class=\"Menu\">");
            chuoi.Append("<ul class=\"ul1\">");
            for (int i = 0; i < listParent.Count;i++ )
            {
             
                chuoi.Append("<li class=\"li1\">");
                chuoi.Append("<a href=\"/0/" + listParent[i].Tag + "-"+listParent[i].id+".aspx\" title=\"" + listParent[i].Name + "\"><span style=\"background:url(" + listParent[i].iCon + ") no-repeat\"></span>" + listParent[i].Name + "</a>");
                int idcate = listParent[i].id;
                var listchild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID==idcate).OrderBy(p => p.Ord).ToList();
                if (listchild.Count > 0)
                {
                    chuoi.Append("<ul class=\"ul2\">");
                    for (int j = 0; j < listchild.Count; j++)
                    {
                        chuoi.Append("<li class=\"li2\">");
                        chuoi.Append("<a href=\"/0/" + listchild[j].Tag + "-" + listchild[j].id + ".aspx\" class=\"Name\" title=\"" + listchild[j].Name + "\">" + listchild[j].Name + "</a>");
                        chuoi.Append("<div class=\"Line\"></div>");
                        int idcate1 = listchild[j].id;
                        var listchildchild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idcate1).OrderBy(p => p.Ord).ToList();
                        if (listchildchild.Count > 0)
                        {
                            chuoi.Append("<ul class=\"ul3\">");
                            for (int k = 0; k < listchildchild.Count; k++)
                            {
                                chuoi.Append("<li class=\"li3\">");
                                chuoi.Append("<a href=\"/0/" + listchildchild[k].Tag + "-" + listchildchild[k].id + ".aspx\" title=\"" + listchildchild[k].Name + "\">" + listchildchild[k].Name + "</a>");
                                chuoi.Append("</li>");
                            }
                            chuoi.Append("</ul>");
                        }
                        chuoi.Append("</li>");
                    }
                    chuoi.Append(" </ul>");
                }
                chuoi.Append("</li>");
              

            }
            chuoi.Append("</ul>");
            chuoi.Append("</div>");
            ViewBag.chuoi = chuoi;
                return PartialView();
        }
        public PartialViewResult PatialHeader()
      {
          var listimageslide = db.tblImages.Where(p => p.Active == true && p.idCate == 1).OrderByDescending(p => p.Ord).Take(4).ToList();
          string chuoislide = "";
          for (int i = 0; i < listimageslide.Count; i++)
          {
              if (i == 0)
              {
                  chuoislide += "url(" + listimageslide[i].Images + ") " + (845 * i) + "px 0 no-repeat";
              }
              else
              {

                  chuoislide += ", url(" + listimageslide[i].Images + ") " + (845 * i) + "px 0 no-repeat";
              }
          }
          ViewBag.chuoislide = chuoislide;
          var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 6).OrderBy(p=>p.Ord).ToList();
            string chuoi="";
          for (int i = 0; i < listImage.Count;i++ )
          {
              chuoi+= " <a href=\"" + listImage[i].Url + "\" title=\"" + listImage[i].Name + "\"><img src=\"" + listImage[i].Images + "\" alt=\"" + listImage[i].Name + "\" title=\"" + listImage[i].Name + "\" /></a>";
          }
          ViewBag.chuoi = chuoi;
          return PartialView(listimageslide);
      }
    }
}