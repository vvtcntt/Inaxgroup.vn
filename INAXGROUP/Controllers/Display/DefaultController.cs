using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAXGROUP.Models;
using System.Text;
namespace INAXGROUP.Controllers.Display
{
    public class DefaultController : Controller
    {
        // GET: Default
        private INAXGROUPContext db = new INAXGROUPContext(); 
        public ActionResult Index()
        {
            tblConfig config = db.tblConfigs.First();
            ViewBag.Title = "<title>" + config.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + config.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + config.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + config.Keywords + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Inaxgroup.vn\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + config.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + config.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Inaxgroup.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + config.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Inaxgroup.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Inaxgroup.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + config.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; 
            ViewBag.h1 = "<h1 class=\"h1\">"+config.Title+"</h1>";
            if (Session["registry"] != null)
            {
                ViewBag.Thongbao = Session["registry"].ToString();
            }
            Session["registry"] = null;
            var tblImage = db.tblImages.Where(p => p.idCate == 9 && p.Active==true).OrderBy(p => p.Ord).Take(1).ToList();
            if(tblImage.Count>0)
                ViewBag.chuoiimage = "<div class=\"Thongbao\"><a target=\"_blank\" href=\"" + tblImage[0].Url + "\" title=\"" + tblImage[0].Name + "\"><img src=\"" + tblImage[0].Images + "\" alt=\"" + tblImage[0].Name + "\" /></a></div>";
            return View();
        }
        public PartialViewResult AdwLeftRight()
        {
            StringBuilder chuoileft = new StringBuilder();
            StringBuilder chuoiright = new StringBuilder();
            var listLeft = db.tblImages.Where(p => p.Active == true & p.idCate == 4).ToList();
            for (int i = 0; i < listLeft.Count; i++)
            {
                chuoileft.Append("<a href=\"" + listLeft[i].Url + "\" title=\"" + listLeft[i].Name + "\"><img src=\"" + listLeft[i].Images + "\" alt=\"" + listLeft[i].Name + "\" width=\"128\" /></a>");
            }
            var listright = db.tblImages.Where(p => p.Active == true & p.idCate == 5).ToList();
            for (int i = 0; i < listright.Count; i++)
            {
                chuoiright.Append("<a href=\"" + listright[i].Url + "\" title=\"" + listright[i].Name + "\"><img src=\"" + listright[i].Images + "\" alt=\"" + listright[i].Name + "\" width=\"128\" /></a>");
            }
            ViewBag.chuoileft = chuoileft;
            ViewBag.chuoiright = chuoiright;
            return PartialView();
        }
        public PartialViewResult AdwLeftExt()
        {
            tblConfig config = db.tblConfigs.First();
            StringBuilder chuoi = new StringBuilder();
            if (config.PopupSupport == true)
            {
                var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 8).OrderByDescending(p => p.Ord).Take(1).ToList();
                if (listImage.Count > 0)
                {
                    chuoi.Append("<div class=\"float-ck\" style=\"left: 0px\">");
                    chuoi.Append("<div id=\"hide_float_left\">");
                    chuoi.Append("<a href=\"javascript:hide_float_left()\">Tắt Quảng Cáo [X]</a>");
                    chuoi.Append("</div>");
                    chuoi.Append("<div id=\"float_content_left\"> ");
                    if (listImage[0].Link == true)
                    { chuoi.Append("<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"); }
                    else
                    { chuoi.Append("<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\" rel=\"" + listImage[0].Link + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"); }

                    chuoi.Append("</div>");
                    chuoi.Append("</div>");
                }
            }
            ViewBag.popupSuport = chuoi;
            return PartialView();
        }
        public PartialViewResult Popup()
        {
            tblConfig config = db.tblConfigs.First();
            StringBuilder chuoi = new StringBuilder();
            if (config.Popup == true)
            {
                var listImage = db.tblImages.Where(p => p.Active == true && p.idCate==7).OrderByDescending(p => p.Ord).Take(1).ToList();
                if (listImage.Count > 0)
                {
                    chuoi.Append("<div id=\"myModal\" class=\"linhnguyen-modal\">");
                    chuoi.Append("<a class=\"close-linhnguyen-modal\" title=\"đóng\">X</a>");
                    if (listImage[0].Link == true)
                    { chuoi.Append("<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"); }
                    else
                    { chuoi.Append("<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\" rel=\"" + listImage[0].Link + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"); }

                    chuoi.Append("</div>");
                }
            }
            ViewBag.Popup = chuoi;
            return PartialView();
        }
       public ActionResult Action()
        {
            var listProduct = db.tblProducts.ToList();
            for (int i = 0; i < listProduct.Count;i++ )
            {
                clsSitemap.CreateSitemap("1/" + listProduct[i].Tag + "-" + listProduct[i].id + ".aspx", listProduct[i].id.ToString(), "Product");
            }
            var listGrouProduct = db.tblGroupProducts.ToList();
            for (int i = 0; i < listGrouProduct.Count; i++)
            {
                clsSitemap.CreateSitemap("0/" + listGrouProduct[i].Tag + "-" + listGrouProduct[i].id + ".aspx", listGrouProduct[i].id.ToString(), "GroupProduct");
            }
            var ListNews = db.tblNews.ToList();
            for (int i = 0; i < ListNews.Count; i++)
            {
                clsSitemap.CreateSitemap("3/" + ListNews[i].Tag + "-" + ListNews[i].id + ".aspx", ListNews[i].id.ToString(), "News");
            }
                return View();
        }
    }
}