using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAXGROUP.Models;
using System.Text;
namespace INAXGROUP.Controllers.Display.Session.ProductSyn
{
    public class ProductSynDisplayController : Controller
    {
        INAXGROUPContext db = new INAXGROUPContext();
        //
        // GET: /ProductSynDisplay/
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ListProductSyn()
        {
            StringBuilder chuoi = new StringBuilder();
            var listProductSyn = db.tblProductSyns.Where(p => p.Active == true && p.ViewHomes == true).OrderBy(p => p.Ord).ToList();
             
                chuoi.Append("<div id=\"ProductSYN\">");
                if (listProductSyn.Count > 0)
                {
                     chuoi.Append("<div class=\"n_var\">");
            	 chuoi.Append("<div class=\"Left_n_Var\"></div>");
                 chuoi.Append("<div class=\"Center_n_Var\"><h3>Sản phẩm INAXGROUP đồng bộ</h3></div>");
                 chuoi.Append("<div class=\"Right_n_Var\"></div>"); 
             chuoi.Append("</div>");
                    
                }
                chuoi.Append("<div class=\"Adw_Syn\">");
                var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 2).OrderBy(p => p.Ord).ToList();
                for (int i = 0; i < listImage.Count; i++)
                {
                    chuoi.Append("<a href=\"" + listImage[i].Url + "\" title=\"" + listImage[i].Name + "\"><img src=\"" + listImage[i].Images + "\" alt=\"" + listImage[i].Name + "\"/></a>");

                }
                chuoi.Append(" </div>");
                if (listProductSyn.Count > 0)
                {
                    chuoi.Append("<div id=\"Content_ProductSYN\">");

                    for (int i = 0; i < listProductSyn.Count; i++)
                    {
                        chuoi.Append(" <div class=\"spdb\">");
                        chuoi.Append(" <div class=\"sptb\"></div>");

                        chuoi.Append(" <div class=\"img_spdb\">");
                        chuoi.Append(" <a href=\"/Syn/" + listProductSyn[i].Tag + "\" title=\"" + listProductSyn[i].Name + "\"><img src=\"" + listProductSyn[i].ImageLinkThumb + "\" alt=\"" + listProductSyn[i].Name + "\" /></a>");
                        chuoi.Append(" </div>");
                        chuoi.Append(" <a href=\"/Syn/" + listProductSyn[i].Tag + "\" class=\"Name\" title=\"" + listProductSyn[i].Name + "\">" + listProductSyn[i].Name + "</a>");
                        chuoi.Append("<div class=\"Bottom_Tear_Sale\">");
                        chuoi.Append("<div class=\"Price\">");
                        if (listProductSyn[i].PriceSale>2)
                        { chuoi.Append("<p class=\"PriceSale\">" + string.Format("{0:#,#}", listProductSyn[i].PriceSale) + " <span>đ</span></p>"); }
                        else
                        { chuoi.Append("<p class=\"PriceSale\"> Liên hệ</p>");}
                        if (listProductSyn[i].Price > 2)
                        { chuoi.Append("<p class=\"Price_s\">" + string.Format("{0:#,#}", listProductSyn[i].Price) + "đ</p>"); }
                        else
                        { chuoi.Append("<p class=\"Price_s\">Liên hệ</p>"); }
                        chuoi.Append("</div>");
                        chuoi.Append("<div class=\"sevices\">");
                        if (listProductSyn[i].Status == true)
                        {
                            chuoi.Append("<span class=\"Status\"></span>");
                        }
                        else
                        {
                            chuoi.Append("<span class=\"StatusNo\"></span>");
                        }

                        chuoi.Append("<span class=\"Transport\"><span class=\"icon\">");
                        if (listProductSyn[i].Transport == true)
                        {
                            chuoi.Append("</span> Toàn quốc</span>");
                        }
                        else
                        {
                            chuoi.Append("</span> Liên hệ</span>");
                        }
                        chuoi.Append("<span class=\"View\"></span>");
                        chuoi.Append("</div>");
                        chuoi.Append("</div>");

                        chuoi.Append("  </div>");
                    }
                    chuoi.Append("</div>");
                }
                chuoi.Append("</div>");
           
            ViewBag.chuoisyc = chuoi;
            return PartialView();
        }
     
        public ActionResult ProductSyn_Detail(string tag)
        {
            var tblproductSyn = db.tblProductSyns.First(p => p.Tag == tag);
            int id = int.Parse(tblproductSyn.id.ToString());
            string chuoi = "Khách hàng vui lòng kích vào chi tiết từng sản phẩm ở trên để xem thông thông số kỹ thuật !";
            ViewBag.Title = "<title>" + tblproductSyn.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblproductSyn.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblproductSyn.Keyword + "\" /> ";
            //Load Images Liên Quan
            var listImage = db.tblImageProducts.Where(p => p.idProduct == id && p.Type==1).ToList();
            string chuoiimages = "";
            for (int i = 0; i < listImage.Count; i++)
            {
                chuoiimages += " <li class=\"Tear_pl\"><a href=\"" + listImage[i].Images + "\" rel=\"prettyPhoto[gallery1]\" title=\"" + listImage[i].Name + "\"><img src=\"" + listImage[i].Images + "\"   alt=\"" + listImage[i].Name + "\" /></a></li>";
            }
            ViewBag.chuoiimage = chuoiimages;
            int idsyn = int.Parse(tblproductSyn.id.ToString());
            int visit = int.Parse(tblproductSyn.Visit.ToString());
            if (visit > 0)
            {
                tblproductSyn.Visit = tblproductSyn.Visit + 1;
                db.SaveChanges();
            }
            else
            {
                tblproductSyn.Visit = tblproductSyn.Visit + 1;
                db.SaveChanges();
            }
            var Product = db.ProductConnects.Where(p => p.idSyn == idsyn).ToList();
            StringBuilder chuoipr = new StringBuilder();
            StringBuilder chuoisosanh = new StringBuilder();
            float tonggia = 0;
            if (Product.Count > 0)
            {
                chuoipr.Append("<div id=\"Content_spdb\">");
               chuoipr.Append("<span class=\"tinhnang\">&diams; Danh sách sản phẩm có trong " + tblproductSyn.Name + "</span>");
                chuoisosanh.Append("<div id=\"equa\">");
               chuoisosanh.Append("<div class=\"nVar_Equa\"><span>Bảng so sánh giá mua lẻ và mua theo bộ</span></div>");
               chuoisosanh.Append("<div class=\"Clear\"></div>");
               chuoisosanh.Append("<table width=\"200\" border=\"1\">");
               chuoisosanh.Append("<tr style=\"color:#333; text-transform:uppercase; line-height:25px; text-align:center\">");
               chuoisosanh.Append("<td style=\"width:5%;text-align:center\">STT</td>");
               chuoisosanh.Append("<td style=\"width:40%\">Tên Sản phẩm</td>");
               chuoisosanh.Append("<td style=\"width:10%;text-align:center\">Số lượng</td>");
               chuoisosanh.Append("<td style=\"width:20%;text-align:center\">Đơn Giá</td>");
               chuoisosanh.Append("<td style=\"text-align:center; width:20%\">Thành Tiền</td>");
               chuoisosanh.Append("</tr>");
               chuoisosanh.Append("</div>");
                for (int i = 0; i < Product.Count; i++)
                {
                    string codepd = Product[i].idpd;

                    var Productdetail = db.tblProducts.Where(p => p.Code == codepd && p.Active == true).Take(1).ToList();
                    if (Productdetail.Count > 0)
                    {
                        int idCate = int.Parse(Productdetail[0].idCate.ToString());
                        var ListGroup = db.tblGroupProducts.Find(idCate);
                        chuoipr.Append("<div class=\"Tear_syn\">");
                        chuoipr.Append("<div class=\"img_syn\">");
                        chuoipr.Append("<div class=\"nvar_Syn\">");
                        chuoipr.Append("<h2><a href=\"/" + ListGroup.Tag + "" + Productdetail[0].Tag + "_" + Productdetail[0].id + ".html\" title=\"" + Productdetail[0].Name + "\">" + Productdetail[0].Name + "</a></h2>");
                        chuoipr.Append("</div>");
                        chuoipr.Append("<div class=\"img_syn\">");
                        chuoipr.Append("<a href=\"/" + ListGroup.Tag + "" + Productdetail[0].Tag + "_" + Productdetail[0].id + ".html\" title=\"" + Productdetail[0].Name + "\"><img src=\"" + Productdetail[0].ImageLinkThumb + "\" alt=\"" + Productdetail[0].Name + "\" /></a>");
                        chuoipr.Append("</div>");
                        chuoipr.Append("</div>");
                        chuoipr.Append("</div>");
                       chuoisosanh.Append("<tr style=\"line-height:20px\">");
                       chuoisosanh.Append("<td style=\"width:5%;text-align:center\">" + (i + 1) + "</td>");
                       chuoisosanh.Append("<td style=\"width:40%; text-indent:7px\">" + Productdetail[0].Name + "</td>");
                       chuoisosanh.Append("<td style=\"width:10%;text-align:center\"> 1 </td>");
                       chuoisosanh.Append("<td style=\"width:20%;text-align:center\">" + string.Format("{0:#,#}", Productdetail[0].PriceSale) + "</td>");
                       chuoisosanh.Append("<td style=\"text-align:center; width:20%\">" + string.Format("{0:#,#}", Productdetail[0].PriceSale) + "</td>");
                       chuoisosanh.Append(" </tr>");
                        tonggia = tonggia + float.Parse(Productdetail[0].PriceSale.ToString());
                    }

                }
                chuoipr.Append("</div>");
               chuoisosanh.Append("<tr style=\"line-height:25px \">");
               chuoisosanh.Append("<td colspan=\"4\"><span style=\"text-align:center; margin-right:5px; font-weight:bold; display:block\">TỔNG GIÁ MUA LẺ</span></td>");
               chuoisosanh.Append("<td style=\"font-weight:bold; font-size:16px; text-align:center\">" + string.Format("{0:#,#}", Convert.ToInt32(tonggia)) + " đ</td>");
               chuoisosanh.Append("</tr>");
               chuoisosanh.Append("<tr>");
                int sodu = Convert.ToInt32(tonggia) - int.Parse(tblproductSyn.PriceSale.ToString());

               chuoisosanh.Append("<td colspan=\"4\"><span style=\"text-align:center; margin-right:5px; font-weight:bold; display:block; color:#ff5400\">GIÁ MUA THEO BỘ</span></td>");
               chuoisosanh.Append("<td style=\"font-weight:bold; color:#ff5400; font-size:18px; font-family:UTMSwiss; text-align:center\">" + string.Format("{0:#,#}", tblproductSyn.PriceSale) + "đ<span style=\"font-style:italic; color:#333; font-size:12px; font-family:Arial, Helvetica, sans-serif; margin:5px; display:block; font-weight:normal\">Bạn đã tiết kiệm : " + string.Format("{0:#,#}", sodu) + "đ khi mua theo bộ</span></td>");
               chuoisosanh.Append("</tr>");
               chuoisosanh.Append("</table>");
            }

            ViewBag.chuoi = chuoi;
            ViewBag.chuoisosanh = chuoisosanh;
            ViewBag.chuoipr = chuoipr;
            return View(tblproductSyn);
        }
        public PartialViewResult RightProductSyn(string tag)
        {
            var tblproductSyn = db.tblProductSyns.First(p => p.Tag == tag);
            StringBuilder chuoisupport = new StringBuilder();
            var listSupport = db.tblSupports.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listSupport.Count; i++)
            {
                chuoisupport.Append("<div class=\"Line_Buttom\"></div>");
                chuoisupport.Append("<div class=\"Tear_Supports\">");
                chuoisupport.Append("<div class=\"Left_Tear_Support\">");
                chuoisupport.Append("<span class=\"htv1\">" + listSupport[i].Mission + ":</span>");
                chuoisupport.Append("<span class=\"htv2\">" + listSupport[i].Name + " :</span>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("<div class=\"Right_Tear_Support\">");
                chuoisupport.Append("<a href=\"ymsgr:sendim?" + listSupport[i].Yahoo + "\">");
                chuoisupport.Append("<img src=\"http://opi.yahoo.com/online?u=" + listSupport[i].Yahoo + "&m=g&t=1\" alt=\"Yahoo\" class=\"imgYahoo\" />");
                chuoisupport.Append(" </a>");
                chuoisupport.Append("<a href=\"Skype:" + listSupport[i].Skyper + "?chat\">");
                chuoisupport.Append("<img class=\"imgSkype\" src=\"/Content/Display/iCon/skype-icon.png\" title=\"Kangaroo\" alt=\"" + listSupport[i].Name + "\">");
                chuoisupport.Append("</a>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("</div>");
            }
            ViewBag.chuoisupport = chuoisupport;


            //Load sản phẩm liên quan
            StringBuilder chuoiproduct = new StringBuilder();
            var listProductSyn = db.tblProductSyns.Where(p => p.Active == true).OrderBy(p => p.Ord).Take(7).ToList();
            for (int i = 0; i < listProductSyn.Count; i++)
            {

                

             
               chuoiproduct.Append(" <div class=\"spdb\">");
               chuoiproduct.Append(" <div class=\"sptb\"></div>");

               chuoiproduct.Append(" <div class=\"img_spdb\">");
               chuoiproduct.Append(" <a href=\"/Syn/" + listProductSyn[i].Tag + "\" title=\"" + listProductSyn[i].Name + "\"><img src=\"" + listProductSyn[i].ImageLinkThumb + "\" alt=\"" + listProductSyn[i].Name + "\" /></a>");
               chuoiproduct.Append(" </div>");
               chuoiproduct.Append(" <a href=\"/Syn/" + listProductSyn[i].Tag + "\" class=\"Name\" title=\"" + listProductSyn[i].Name + "\">" + listProductSyn[i].Name + "</a>");
               chuoiproduct.Append("<div class=\"Bottom_Tear_Sale\">");
               chuoiproduct.Append("<div class=\"Price\">");
               chuoiproduct.Append("<p class=\"PriceSale\">" + string.Format("{0:#,#}", listProductSyn[i].PriceSale) + " <span>đ</span></p>");
               chuoiproduct.Append("<p class=\"Price_s\">" + string.Format("{0:#,#}", listProductSyn[i].Price) + "</p>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<div class=\"sevices\">");
                if (listProductSyn[i].Status == true)
                {
                   chuoiproduct.Append("<span class=\"Status\"></span>");
                }
                else
                {
                   chuoiproduct.Append("<span class=\"StatusNo\"></span>");
                }

               chuoiproduct.Append("<span class=\"Transport\"><span class=\"icon\">");
                if (listProductSyn[i].Transport == true)
                {
                   chuoiproduct.Append("</span> Toàn quốc</span>");
                }
                else
                {
                   chuoiproduct.Append("</span> Liên hệ</span>");
                }
               chuoiproduct.Append("<span class=\"View\"></span>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");

               chuoiproduct.Append("  </div>");
            }
            ViewBag.chuoiproduct = chuoiproduct;
            tblConfig tblconfig = db.tblConfigs.First();
            return PartialView(tblconfig);

        }
        public ActionResult Hienthidongbo()
        {
            ViewBag.Title = "<title> Danh sách sản phẩm INAXGROUP đồng bộ - Khuyến mại lớn !</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"Danh sách những sản phẩm INAXGROUP đồng bộ áp dụng khuyến mại dành cho khách hàng khi mua thiết bị vệ sinh INAXGROUP\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"Sản phẩm INAXGROUP đồng bộ, thiết bị vệ sinh INAXGROUP đồng bộ\" /> ";
            var listsanphamdongbo = db.tblProductSyns.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 2).ToList();
            StringBuilder chuoi = new StringBuilder();
            for (int i = 0; i < listImage.Count; i++)
            {
                chuoi.Append("<a href=\"" + listImage[i].Url + "\" title=\"" + listImage[i].Name + "\" rel=\"" + listImage[i].Link + "\"><img src=\"" + listImage[i].Images + "\" alt=\"" + listImage[i].Name + "\" /></a>");
            }
            ViewBag.hienthianh = chuoi;
            return View(listsanphamdongbo);
        }
	}
}