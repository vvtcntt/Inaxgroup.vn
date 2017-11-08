using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAXGROUP.Models;
using System.Text;
using System.Text.RegularExpressions;
namespace INAXGROUP.Controllers.Display.Session.Product
{
    public class ProductController : Controller
    {
        // GET: Product
        private INAXGROUPContext db = new INAXGROUPContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PartialProductSaleHomes()
        {
            var listProduct = db.tblProducts.Where(p => p.Active == true && p.ProductSale == true).OrderBy(p => p.Ord).ToList();
            StringBuilder chuoi = new StringBuilder();
            if(listProduct.Count>0)
            { 
            for (int i = 0; i < listProduct.Count; i++)
            {
                int idcates = int.Parse(listProduct[i].idCate.ToString());

                string Url = db.tblGroupProducts.First(p => p.id == idcates).Tag;
                chuoi.Append("<div class=\"Tear_Sale\">");
                //if(listProduct[i].Access.Length>3)
                //{
                //         chuoi.Append("<div class=\"Tietkiemnuoc\">");
                //    	 chuoi.Append("<span class=\"tk1\">Mức xả</span>");
                //         chuoi.Append("<span class=\"tk2\">"+listProduct[i].Access+"</span>");
                //         chuoi.Append("</div>");
                //}

                if(listProduct[i].New==true)
                {
                 chuoi.Append(" <div class=\"Box_ProductNews\"></div>");}
                chuoi.Append("<div class=\"Box_Tietkiem\">");
                chuoi.Append("<span class=\"kt0\">Bạn hãy</span>");
                chuoi.Append("<span class=\"kt1\">Gọi điện</span>");
                chuoi.Append("<span class=\"kt3\">nhận giá rẻ nhất</span>");
                float nPrice = float.Parse(listProduct[i].Price.ToString());
                float nPriceSale = float.Parse(listProduct[i].PriceSale.ToString());
                float nSum = 100-((nPriceSale * 100) / nPrice);
                chuoi.Append(" </div>");
                chuoi.Append("<div class=\"img\">");
                chuoi.Append(" <a href=\"/1/" + listProduct[i].Tag + "-"+listProduct[i].id+".aspx\" title=\"" + listProduct[i].Name + "\"><img src=\"" + listProduct[i].ImageLinkThumb + "\" alt=\"" + listProduct[i].Name + "\" title=\"" + listProduct[i].Name + "\" /></a>");
                chuoi.Append("</div>");
                chuoi.Append("<h2><a href=\"/1/" + listProduct[i].Tag + "-" + listProduct[i].id + ".aspx\" title=\"" + listProduct[i].Name + "\" class=\"Name\">" + listProduct[i].Name + "</a></h2>");
                chuoi.Append("<div class=\"Info\">");
                chuoi.Append("<div class=\"Left_Info\">");
                chuoi.Append("<div class=\"Top_Left_Info\">");
                if (listProduct[i].Status == true)
                { chuoi.Append("<span class=\"Status\"></span>"); }
                else
                { chuoi.Append("<span class=\"Status1\"></span>"); }
                 chuoi.Append("</div>");
                chuoi.Append("<div class=\"Buttom_Left_Info\">");
                //load tính năng
                int id = int.Parse(listProduct[i].id.ToString());
                var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == id).ToList();
                if (checkfun.Count > 0)
                {
                    for (int j = 0; j < listfuc.Count; j++)
                    {
                        int idfun = int.Parse(listfuc[j].id.ToString());
                        var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == id).ToList();
                        if (connectfun.Count > 0)
                        {
                            chuoi.Append("<a href=\"" + listfuc[j].Url + "\" rel=\"nofollow\" title=\"" + listfuc[j].Name + "\"><img src=\"" + listfuc[j].Images + "\" alt=\"" + listfuc[j].Name + "\" /></a>");
                        }
                    }

                }
                chuoi.Append("</div>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"Right_Info\">");
                string note = ".";
                if(listProduct[i].PriceSaleActive>10)
                {
                    chuoi.Append("<span class=\"Pricesale\">" + string.Format("{0:#,#}", listProduct[i].PriceSaleActive) + "đ</span>");
                    if(listProduct[i].PriceSaleActive>listProduct[i].PriceSale)
                    {
                        note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[i].PriceSaleActive - listProduct[i].PriceSale) + "đ hoặc cao hơn nữa cho bạn !";

                    }
                }
                else
                {
                    chuoi.Append("<span class=\"Pricesale\">Liên hệ</span>");
                }
           
                chuoi.Append("<span class=\"Price\">" + string.Format("{0:#,#}", listProduct[i].Price) + "đ</span>");
                chuoi.Append("</div>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"Clear\"></div>");
                chuoi.Append("<div class=\"Qua\">");
                chuoi.Append("<span class=\"ng ng_" + i + "\">Hãy gọi điện hoặc đến trực tiếp để nhận giá khuyến mại. Chúng tôi cam kết giá rẻ nhất Việt Nam</span>");
                chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[i].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[i].id + "','" + listProduct[i].Name + "','" + String.Format("{0:#,#}", listProduct[i].PriceSale) + "','" + note + "');\" title=\"Để lấy giá hỗ trợ tốt nhất Hà Nội\">Lấy giá tốt nhất</span></div>");

                //chuoi.Append(" <div class=\"Content_Qua\">");
                //chuoi.Append("<span class=\"dt\" onclick=\"javascript:return Goidien(" + i + ")\">Gọi điện</span><span class=\"gd\"  onclick=\"javascript:return Dentructiep(" + i + ")\">Đến trực tiếp</span><span class=\"ch\"  onclick=\"javascript:return Chat(" + i + ")\">Chát</span>");
                //chuoi.Append(" </div>");
                chuoi.Append("</div>");
                chuoi.Append("</div>");

            }
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        List<string> Mangphantu = new List<string>();
        public List<string> Arrayid(int idParent)
        {

            var ListMenu = db.tblGroupProducts.Where(p => p.ParentID == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].id.ToString());
                int id = int.Parse(ListMenu[i].id.ToString());
                Arrayid(id);

            }

            return Mangphantu;
        }
        string nUrl = "";
        public string UrlProduct(int idCate)
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <a href=\"/0/" + ListMenu[i].Tag + "-"+ListMenu[i].id+".aspx\" title=\"" + ListMenu[i].Name + "\"> " + " " + ListMenu[i].Name + "</a> <i></i>" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }
        public PartialViewResult PartialProductHomes()
        {
            StringBuilder chuoi = new StringBuilder();
            var MenuParent = db.tblGroupProducts.Where(p => p.Active == true && p.Priority == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < MenuParent.Count; i++)
            {
                chuoi.Append("<div class=\"ProductHomes\">");
                chuoi.Append("<div class=\"nVar\">");
                chuoi.Append("<div class=\"Left_nVar\">");
                chuoi.Append("<div class=\"Name\">");
                chuoi.Append("<span class=\"iCon\" style=\"background:url(" + MenuParent[i].iCon + ") no-repeat center center scroll #FFF\"></span>");
                chuoi.Append("<h2><a href=\"/0/" + MenuParent[i].Tag + "-" + MenuParent[i].id + ".aspx\" title=\"" + MenuParent[i].Name + "\">" + MenuParent[i].Name + "</a></h2>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"iCon_nVar\">");
                chuoi.Append("<span>T" + (i + 1) + "</span>");
                chuoi.Append("</div>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"Right_nVar\">");
                int idcate1 = MenuParent[i].id;
                var Menuchild = db.tblGroupProducts.Where(p => p.ParentID == idcate1 && p.Active == true).OrderBy(p => p.Ord).Take(4).ToList();
                if (Menuchild.Count > 0)
                {
                    chuoi.Append("<ul class=\"ul_1\">");
                    for (int j = 0; j < Menuchild.Count; j++)
                    {
                        string ntag = Menuchild[j].Tag;
                        chuoi.Append("<li class=\"li_1\">");
                        chuoi.Append(" <h2><a href=\"/0/" + ntag + "-" + Menuchild[j].id + ".aspx\" title=\"" + Menuchild[j].Name + "\"> " + Menuchild[j].Name + "<span></span></a></h2>");
                        int idcate2 = Menuchild[j].id;
                        var Menuchild1 = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idcate2).OrderBy(p => p.Ord).ToList();
                        if (Menuchild1.Count > 0)
                        {
                            chuoi.Append("<ul class=\"ul_2\">");
                            for (int k = 0; k < Menuchild1.Count; k++)
                            {
                                chuoi.Append("<li class=\"li_2\">");
                                chuoi.Append("<a href=\"/0/" + Menuchild1[k].Tag + "-" + Menuchild1[k].id + ".aspx\" title=\"" + Menuchild1[k].Name + "\">› " + Menuchild1[k].Name + "</a>");
                                chuoi.Append("</li>");
                            }
                            chuoi.Append("</ul>");
                        }

                        chuoi.Append(" </li>");
                    }
                    chuoi.Append("  </ul>");
                }
                string tag1 = MenuParent[i].Tag;
                chuoi.Append("<a href=\"/0/" + tag1 + "-" + MenuParent[i].id + ".aspx\" title=\"Xem chi tiết\" class=\"Xemchitiet\">Xem thêm &raquo;</a>");
                chuoi.Append("</div>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"bg_ProductHomes\">");
                var listImageadwcenter = (from a in db.tblConnectImages join b in db.tblImages on a.idImg equals b.id where a.idCate == idcate1 && b.idCate == 11 select b).ToList();
                if (listImageadwcenter.Count > 0)
                {
                    chuoi.Append("<div class=\"Img_adwcenter\">");
                    for (int j = 0; j < listImageadwcenter.Count; j++)
                    {
                        chuoi.Append("<a href=\"" + listImageadwcenter[j].Url + "\" title=\"" + listImageadwcenter[j].Name + "\"><img src=\"" + listImageadwcenter[j].Images + "\" alt=\"" + listImageadwcenter[j].Name + "\" /></a>");

                    }

                    chuoi.Append("</div>");
                }
                chuoi.Append("<div class=\"Content_ProductHomes\">");
                chuoi.Append("<div class=\"Left_Content_ProductHomes\">");
                int idmenu = int.Parse(MenuParent[i].id.ToString());
                List<int> MangImage = new List<int>();
                var ListConnectImage = db.tblConnectImages.Where(p => p.idCate == idmenu).ToList();
                foreach(var item in ListConnectImage)
                {
                    int idm = int.Parse(item.idImg.ToString());
                    MangImage.Add(idm);
                }
                var listImages = db.tblImages.Where(p => MangImage.Contains(p.id) && p.Active == true ).OrderBy(p => p.Ord).ToList();
                for (int x = 0; x < listImages.Count; x++)
                {
                    chuoi.Append(" <a href=\"" + listImages[x].Url + "\" title=\"" + listImages[x].Name + "\" rel=\"nofollow\">");
                    chuoi.Append("<img src=\"" + listImages[x].Images + "\" alt=\"" + listImages[x].Name + "\" />");
                    chuoi.Append("</a>");
                }
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"Right_Content_ProductHomes\">");
               
                     List<string> Mang = new List<string>();
                     Mang = Arrayid(idcate1);
                     Mang.Add(idcate1.ToString());
                     var listProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString()) && p.ViewHomes == true).OrderBy(p => p.Ord).ToList();
                    for (int y = 0; y < listProduct.Count; y++)
                    {
                        chuoi.Append("<div class=\"Tear_1\">");
                        if (listProduct[y].New == true)
                        {
                            chuoi.Append(" <div class=\"Box_ProductNews\"></div>");
                        }
                        chuoi.Append("<div class=\"Box_Tietkiem\">");
                        chuoi.Append("<span class=\"kt0\">Bạn hãy</span>");
                        chuoi.Append("<span class=\"kt1\">gọi điện</span>");
                         chuoi.Append("<span class=\"kt3\">giá rẻ nhất</span>");
                        float nPrice = float.Parse(listProduct[y].Price.ToString());
                        float nPriceSale = float.Parse(listProduct[y].PriceSale.ToString());
                        float nSum = 100 - ((nPriceSale * 100) / nPrice);
                        //chuoi += "<span class=\"kt2\">" + Convert.ToInt32(nSum) + "<span>%</span></span>");
                        chuoi.Append(" </div>");
                        chuoi.Append("<div class=\"img\">");
                        chuoi.Append("<a href=\"/1/" + listProduct[y].Tag + "-" + listProduct[y].id + ".aspx\" title=\"" + listProduct[y].Name + "\">");
                        chuoi.Append("<img src=\"" + listProduct[y].ImageLinkThumb + "\" alt=\"" + listProduct[y].Name + "\" title=\"" + listProduct[y].Name + "\"  />");
                        chuoi.Append("</a>");
                        chuoi.Append("</div>");
                        chuoi.Append("<h3><a href=\"/1/" + listProduct[y].Tag + "-" + listProduct[y].id + ".aspx\" title=\"" + listProduct[y].Name + "\" class=\"Name\">" + listProduct[y].Name + "</a></h3>");
                        chuoi.Append("<div class=\"Info\">");

                        chuoi.Append(" <div class=\"LeftInfo\">");
                        string note = ".";
                        if (listProduct[y].PriceSaleActive> 10)
                        { 
                            chuoi.Append("<span class=\"PriceSale\">" + string.Format("{0:#,#}", listProduct[y].PriceSaleActive) + "đ</span>");
                            if (listProduct[y].PriceSaleActive > listProduct[y].PriceSale)
                            {
                                note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[y].PriceSaleActive - listProduct[y].PriceSale) + "đ hoặc cao hơn nữa cho bạn !";

                            }
                        }
                        else
                            chuoi.Append("<span class=\"PriceSale\">Liên hệ</span>");
                        if (listProduct[y].Price < 10)
                        { chuoi.Append("<span class=\"Price\">Liên hệ</span>"); }
                        else
                            chuoi.Append("<span class=\"Price\">" + string.Format("{0:#,#}", listProduct[y].Price) + "đ</span>");
                        chuoi.Append("</div>");
                        chuoi.Append("<div class=\"RightInfo\">");
                        chuoi.Append("<div class=\"Top_RightInfo\">");
                        chuoi.Append("<a href=\"/Order/OrderIndex?idp=" + listProduct[y].id + "\" title=\"Đặt hàng\" rel=\"nofollow\"><span></span></a>");
                        chuoi.Append("</div>");
                        chuoi.Append("<div class=\"Bottom_RightInfo\">");
                        int id = int.Parse(listProduct[y].id.ToString());
                        var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                        var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == id).ToList();
                        if (checkfun.Count > 0)
                        {
                            for (int j = 0; j < listfuc.Count; j++)
                            {
                                int idfun = int.Parse(listfuc[j].id.ToString());
                                var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == id).ToList();
                                if (connectfun.Count > 0)
                                {
                                    chuoi.Append("<a href=\"" + listfuc[j].Url + "\" rel=\"nofollow\" title=\"" + listfuc[j].Name + "\"><img src=\"" + listfuc[j].Images + "\" alt=\"" + listfuc[j].Name + "\" /></a>");
                                }
                            }

                        }
                        chuoi.Append("</div>");
                        chuoi.Append("</div>");
                        chuoi.Append("</div>");
                        chuoi.Append("<div class=\"Qua\">");
 
                        chuoi.Append("<span class=\"ng ng1_" + id + "\">Đang khuyến mại giá rẻ nhất Hà Nội,để biết giá hãy :</span>");
                        //chuoi.Append(" <div class=\"Content_Qua\">");
                        //chuoi.Append("<span class=\"dt\" onclick=\"javascript:return Goidien(" + id + ")\">Gọi điện</span><span class=\"gd\"  onclick=\"javascript:return Dentructiep(" + id + ")\">Đến trực tiếp</span><span class=\"ch\"  onclick=\"javascript:return Chat(" + id + ")\">Chát</span>");
                        //chuoi.Append(" </div>");
                        chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[y].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[y].id + "','" + listProduct[y].Name + "','" + String.Format("{0:#,#}", listProduct[y].PriceSale) + "','"+note+"');\" title=\"Để lấy giá hỗ trợ tốt nhất Hà Nội\">Lấy giá tốt nhất</span></div>");

                        chuoi.Append("</div>");
                        chuoi.Append("</div>");
                    }
               
                chuoi.Append("</div>");
                chuoi.Append("</div>");
                chuoi.Append("</div>");
                chuoi.Append("</div>");
                Mangphantu.Clear();
                }
            
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public ActionResult ProductDetail(string idpd)
        {
            bool Kiemtra = idpd.All(char.IsDigit);
            int id;
            if(Kiemtra==false)
            {
               id = int.Parse(Regex.Match(idpd, @"\d+").Value.ToString());
               tblProduct Productkt = db.tblProducts.First(p => p.id == id);
               return Redirect("/1/"+Productkt.Tag+"-"+id+".aspx");

            }
            id = int.Parse(idpd);
            tblProduct Product = db.tblProducts.First(p => p.id==id);
             int visit = int.Parse(Product.Visit.ToString());
            if (visit > 0)
            {
                Product.Visit = Product.Visit + 1;
                db.SaveChanges();
            }
            else
            {
                Product.Visit = Product.Visit + 1;
                db.SaveChanges();
            }
            float nPrice = float.Parse(Product.Price.ToString());
            float nPriceSale = float.Parse(Product.PriceSale.ToString());
            float nSum = 100 - ((nPriceSale * 100) / nPrice);
            ViewBag.phantram = Convert.ToInt32(nSum);
            ViewBag.Title = "<title>" + Product.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + Product.Title + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + Product.Description + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + Product.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + Product.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Inaxgroup.vn/1/" + StringClass.NameToTag(Product.Tag) + "-" + Product.id + ".aspx\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + Product.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"http://Inaxgroup.vn/1/" + StringClass.NameToTag(Product.Tag) + "-" + Product.id + ".aspx\" />";
            meta += "<meta itemprop=\"description\" content=\"" + Product.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Inaxgroup.vn" + Product.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + Product.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Inaxgroup.vn" + Product.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Inaxgroup.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + Product.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;  
            //Load Images Liên Quan
            var listImage = db.tblImageProducts.Where(p => p.idProduct == id).ToList();
            string chuoiimages = "";
            for (int i = 0; i < listImage.Count; i++)
            {
                chuoiimages += " <li class=\"Tear_pl\"><a href=\"" + listImage[i].Images + "\" rel=\"prettyPhoto[gallery1]\" title=\"" + listImage[i].Name + "\"><img src=\"" + listImage[i].Images + "\"   alt=\"" + listImage[i].Name + "\" title=\"" + listImage[i].Name + "\" /></a></li>";
            }
            ViewBag.chuoiimage = chuoiimages;

            int idMenu = int.Parse(Product.idCate.ToString());
            ViewBag.Nhomsp = db.tblGroupProducts.First(p => p.id == idMenu).Name;
            string code = Product.Code;
            //Load sản phẩm đổng bộ
            var ProductSyn = db.ProductConnects.Where(p => p.idpd == code).ToList();
            List<int> exceptionList = new List<int>();
            for (int i = 0; i < ProductSyn.Count; i++)
            {
                exceptionList.Add(int.Parse(ProductSyn[i].idSyn.ToString()));
            }
            StringBuilder chuoisym = new StringBuilder();
            var listSyn = db.tblProductSyns.Where(x => exceptionList.Contains(x.id)).ToList();
            if (listSyn.Count > 0)
            {
                chuoisym.Append("<div id=\"Top7\">");
                chuoisym.Append("<div id=\"iCon\"></div>");
                chuoisym.Append("<div id=\"Content_Top7\"><p>Hiện tại sản phẩm <span>" + Product.Name + "</span> có giá rẻ hơn khi mua sản phẩm đồng bộ, bạn có thể xem sản phẩm đồng bộ này</p>");
                chuoisym.Append("<ul>");
                for (int i = 0; i < listSyn.Count; i++)
                {
                    chuoisym.Append("<li><a href=\"/Syn/" + listSyn[i].Tag + "\" title=\"" + listSyn[i].Name + "\" class=\"Syn\" rel=\"nofollow\"><span></span> " + listSyn[i].Name + "</a></li>");
                }
                chuoisym.Append("</ul>");
                chuoisym.Append(" </div>");
                chuoisym.Append("</div>");
            }
            ViewBag.chuoisym = chuoisym;
             int idCate = int.Parse(Product.idCate.ToString());
            tblGroupProduct grouproduct = db.tblGroupProducts.Find(idCate);
           //int idparent=int.Parse(grouproduct.ParentID.ToString());
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a> /" + UrlProduct(idCate);
            // Load màu sản phẩm
            StringBuilder chuoicolor = new StringBuilder();
            ViewBag.nhom = grouproduct.Name;
            var listcolor = db.tblColorProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            var kiemtracolor = db.tblConnectColorProducts.Where(p => p.idPro == id).ToList();
            if (kiemtracolor.Count > 0)
            {

                chuoicolor.Append("<div id=\"Top4\">");
                chuoicolor.Append("<span> Màu sản phẩm : </span>");
                chuoicolor.Append(" <div id=\"Left_Top4\">");
                for (int i = 0; i < listcolor.Count; i++)
                {
                    int idcolor = int.Parse(listcolor[i].id.ToString());
                    var listconnectcolor = db.tblConnectColorProducts.Where(p => p.idPro == id && p.idColor == idcolor).ToList();
                    if (listconnectcolor.Count > 0)
                    {
                        chuoicolor.Append("<span class=\"Mau\" style=\"background:url(" + listcolor[i].Images + ") no-repeat; background-size:100%\"></span> ");
                    }

                }
                chuoicolor.Append("</div>");
                chuoicolor.Append("</div>");
            }

            ViewBag.color = chuoicolor;
            //load tính năng
            StringBuilder chuoifun = new StringBuilder();
            var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == id).ToList();
            string chuoinhom = "";
            if (checkfun.Count > 0)
            {

                chuoifun.Append("<div id=\"Tech\">");
               chuoifun.Append("<span class=\"tinhnang\">Tính năng nổi bật của " + Product.Name + "</span>");
                for (int i = 0; i < listfuc.Count; i++)
                {
                    int idfun = int.Parse(listfuc[i].id.ToString());
                    var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == id).ToList();
                    if (connectfun.Count > 0)
                    {
                       chuoifun.Append("<div class=\"Tear_tech\">");
                        chuoifun.Append("<span class=\"Destech\">" + listfuc[i].Name + "</span>");
                       chuoifun.Append("<p>" + listfuc[i].Description + " <a href=\"" + listfuc[i].Url + "\" title=\"" + listfuc[i].Name + "\">. Xem chi tiết tại đây &raquo;</a></p>");
                       chuoifun.Append("</div>");
                        chuoinhom = listfuc[i].Name;
                    }
                }
               chuoifun.Append("</div>");

            }
            ViewBag.chuoinhom = chuoinhom;
            ViewBag.chuoifun = chuoifun;
            //Load file kỹ thuật

            var listfile = db.tblFiles.Where(p => p.idp == id & p.Cate == 1).Take(1).ToList();
            if (listfile.Count > 0)
            {
                ViewBag.files = "<div class=\"huondansudung\"><a href=\"" + listfile[0].File + "\" title=\"" + listfile[0].Name + "\"><span></span>Tải Hướng dẫn lắp đặt " + Product.Name + "</a></div>";
            }

            //Load tabs
            string kttab = Product.Tabs;
            if(kttab!=null)
            { 
            string[] tabs=Product.Tabs.Split(',');
            string chuoitabs = "";
            for (int i = 0; i < tabs.Length;i++)
            {
                string tagsp = StringClass.NameToTag(tabs[i]);
                chuoitabs += "<a href=\"/tabs/" + tagsp + "\" title=\"" + tabs[i] + "\">" + tabs[i] + "</a>";
            }
            ViewBag.chuoitab = chuoitabs;
            }
            string address = Product.Address.ToString();
            string resultAddress = "";
            if (address != null && address != "")
            {
                int idaddress = int.Parse(address);
                if (db.tblAddresses.FirstOrDefault(p => p.id == idaddress) != null)
                    resultAddress = db.tblAddresses.FirstOrDefault(p => p.id == idaddress).Name;
            }
            ViewBag.address = resultAddress;
            return View(Product);
        }
        public PartialViewResult PartialRightProductDetail(string idpd)
        {
            int id = int.Parse(Regex.Match(idpd, @"\d+").Value.ToString());
            tblProduct Product = db.tblProducts.First(p => p.id == id);
             tblConfig tblconfig = db.tblConfigs.First();
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
                chuoisupport.Append("<div class=\"topTearSupport\">");
                chuoisupport.Append("<a href=\"tel:" + listSupport[i].Mobile + "\" title=\"" + listSupport[i].Name + "\"><img src=\"/Content/Display/iCon/logo_zalo.png\" alt=\"" + listSupport[i].Name + "\" /></a>");
                chuoisupport.Append("<a href=\"tel:" + listSupport[i].Mobile + "\" title=\"" + listSupport[i].Name + "\"><img src=\"/Content/Display/iCon/Viber_logo.png\" alt=\"" + listSupport[i].Name + "\" /></a>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("<div class=\"bottomTearSupport\">");
                chuoisupport.Append("<span>" + listSupport[i].Mobile + "</span>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("</div>");
            }
            ViewBag.chuoisupport = chuoisupport;

            //lIST Menu
            int idCate = int.Parse(Product.idCate.ToString());
            tblGroupProduct grouproduct = db.tblGroupProducts.Find(idCate);
            int parent = int.Parse(grouproduct.ParentID.ToString());
         
            string chuoimenu = "";
            var listGroupProduct = db.tblGroupProducts.Where(p => p.ParentID == parent && p.Active == true ).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listGroupProduct.Count; i++)
            {
                string ntag = listGroupProduct[i].Tag;

                chuoimenu += "<h3><a href=\"/0/" + ntag + "-" + listGroupProduct[i].id + ".aspx\" title=\"" + listGroupProduct[i].Name + "\">› " + listGroupProduct[i].Name + "</a></h3>";

            }
            ViewBag.chuoimenu = chuoimenu;
            //Load sản phẩm liên quan
            string Url = grouproduct.Tag;
            StringBuilder chuoiproduct = new StringBuilder();
            var listProduct = db.tblProducts.Where(p => p.Active == true && p.idCate == idCate).OrderByDescending(p => p.Visit).OrderBy(p => p.Ord).Take(5).ToList();
            for (int i = 0; i < listProduct.Count; i++)
            {

                chuoiproduct.Append(" <div class=\"Tear_1\">");
               chuoiproduct.Append("<div class=\"Box_Tietkiem\">");
               chuoiproduct.Append("<span class=\"kt0\">Bạn hãy</span>");
               chuoiproduct.Append("<span class=\"kt1\">gọi điện</span>");
               chuoiproduct.Append("<span class=\"kt3\">giá rẻ nhất</span>");
                float nPrice = float.Parse(listProduct[i].Price.ToString());
                float nPriceSale = float.Parse(listProduct[i].PriceSale.ToString());
                float nSum = 100 - ((nPriceSale * 100) / nPrice);
                //chuoiproduct += "<span class=\"kt2\">" + Convert.ToInt32(nSum) + "<span>%</span></span>");
               chuoiproduct.Append(" </div>");
               chuoiproduct.Append("<div class=\"img\">");
               chuoiproduct.Append("<a href=\"/1/" + listProduct[i].Tag + "-" + listProduct[i].id + ".aspx\" title=\"" + listProduct[i].Name + "\">");
               chuoiproduct.Append("<img src=\"" + listProduct[i].ImageLinkThumb + "\" alt=\"" + listProduct[i].Name + "\" />");
               chuoiproduct.Append("</a>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<h3><a href=\"/1/" + listProduct[i].Tag + "-" + listProduct[i].id + ".aspx\" title=\"" + listProduct[i].Name + "\" class=\"Name\">" + listProduct[i].Name + "</a></h3>");
               chuoiproduct.Append("<div class=\"Info\">");
               chuoiproduct.Append("<div class=\"LeftInfo\">");
               string note = "";
               if (listProduct[i].PriceSaleActive > 10)
               {
                   chuoiproduct.Append("<span class=\"PriceSale\">" + string.Format("{0:#,#}", listProduct[i].PriceSaleActive) + "đ</span>");
                   if (listProduct[i].PriceSaleActive > listProduct[i].PriceSale)
                   {
                       note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[i].PriceSaleActive - listProduct[i].PriceSale) + "đ hoặc cao hơn nữa cho bạn !";

                   }
               }
               else
                   chuoiproduct.Append("<span class=\"PriceSale\">Liên hệ</span>");
                if (listProduct[i].Price < 10)
               chuoiproduct.Append("<span class=\"Price\">Liên hệ</span>");
                else
                   chuoiproduct.Append("<span class=\"Price\">" + string.Format("{0:#,#}", listProduct[i].Price) + "đ</span>");
               chuoiproduct.Append(" </div>");
               chuoiproduct.Append("<div class=\"RightInfo\">");
               chuoiproduct.Append("<div class=\"Top_RightInfo\">");
               chuoiproduct.Append("<a href=\"/Order/OrderIndex?idp=" + listProduct[i].id + "\" title=\"" + listProduct[i].Name + "\" rel=\"nofollow\"><span></span></a>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<div class=\"Bottom_RightInfo\">");
                int ids = int.Parse(listProduct[i].id.ToString());
                var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == ids).ToList();
                if (checkfun.Count > 0)
                {
                    for (int j = 0; j < listfuc.Count; j++)
                    {
                        int idfun = int.Parse(listfuc[j].id.ToString());
                        var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == ids).ToList();
                        if (connectfun.Count > 0)
                        {
                           chuoiproduct.Append("<a href=\"" + listfuc[j].Url + "\" rel=\"nofollow\" title=\"" + listfuc[j].Name + "\"><img src=\"" + listfuc[j].Images + "\" alt=\"" + listfuc[j].Name + "\" /></a>");
                        }
                    }

                }
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<div class=\"Qua\">");
               chuoiproduct.Append("<span class=\"ng ng1_" + ids + "\">Đang khuyến mại giá rẻ nhất Hà Nội,để biết giá hãy :</span>");
               //chuoiproduct.Append(" <div class=\"Content_Qua\">");
               //chuoiproduct.Append("<span class=\"dt\" onclick=\"javascript:return Goidien(" + ids + ")\">Gọi điện</span><span class=\"gd\"  onclick=\"javascript:return Dentructiep(" + ids + ")\">Đến trực tiếp</span><span class=\"ch\"  onclick=\"javascript:return Chat(" + ids + ")\">Chát</span>");
               //chuoiproduct.Append(" </div>");
               chuoiproduct.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[i].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[i].id + "','" + listProduct[i].Name + "','" + String.Format("{0:#,#}", listProduct[i].PriceSale) + "','" + note + "');\" title=\"Để lấy giá hỗ trợ tốt nhất Hà Nội\">Lấy giá tốt nhất</span></div>");

               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");
            }
            ViewBag.chuoiproduct = chuoiproduct;
            return PartialView(tblconfig);
         }
        public ActionResult ListProduct(string nid)
        {
            bool Kiemtra = nid.All(char.IsDigit);
            int id;
            if (Kiemtra == false)
            {
                id = int.Parse(Regex.Match(nid, @"\d+").Value.ToString());
                tblGroupProduct Productkt = db.tblGroupProducts.First(p => p.id == id);
                return Redirect("/0/" + Productkt.Tag + "-" + id + ".aspx");

            }
               id = int.Parse(nid);
            var GroupProduct = db.tblGroupProducts.First(p => p.id==id);
            ViewBag.Title = "<title>" + GroupProduct.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + GroupProduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + GroupProduct.Keyword + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + GroupProduct.Title + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + GroupProduct.Description + "\" />";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Inaxgroup.vn/0/" + GroupProduct.Tag + "-" + GroupProduct.id + ".aspx\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + GroupProduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"http://Inaxgroup.vn/0/" + GroupProduct.Tag + "-" + GroupProduct.id + ".aspx\" />";
            meta += "<meta itemprop=\"description\" content=\"" + GroupProduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Inaxgroup.vn" + GroupProduct.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + GroupProduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Inaxgroup.vn" + GroupProduct.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Inaxgroup.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + GroupProduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a> /" + UrlProduct(id)+"/ <h1>"+GroupProduct.Title+"</h1>";
             StringBuilder chuoi = new StringBuilder();
            var listGroupProduct = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID==id).OrderBy(p => p.Ord).ToList();
            if (listGroupProduct.Count > 0)
            {
                for (int i = 0; i < listGroupProduct.Count; i++)
                {
                    chuoi.Append(" <div class=\"List_product\">");
                    chuoi.Append(" <div id=\"Box_Name\">");
                    chuoi.Append("<div id=\"Leff_BoxName\">   <h2><a href=\"/0/" + listGroupProduct[i].Tag + "-" + listGroupProduct[i].id + ".aspx\" title=\"" + listGroupProduct[i].Name + "\">" + listGroupProduct[i].Name + "</a></h2></div>");
                    chuoi.Append("</div>");

                    chuoi.Append("<div class=\"Clear\"></div>");
                    var listImageadwcenter = (from a in db.tblConnectImages join b in db.tblImages on a.idImg equals b.id where a.idCate == id && b.idCate == 11  select b).ToList();
                    if (listImageadwcenter.Count > 0)
                    {
                        chuoi.Append("<div class=\"Img_adwcenter\">");
                        for (int j = 0; j < listImageadwcenter.Count; j++)
                        {
                            chuoi.Append("<a href=\"" + listImageadwcenter[j].Url + "\" title=\"" + listImageadwcenter[j].Name + "\"><img src=\"" + listImageadwcenter[j].Images + "\" alt=\"" + listImageadwcenter[j].Name + "\" /></a>");

                        }

                        chuoi.Append("</div>");
                    }
                    chuoi.Append("<div class=\"ContentProduct\">");
                    int idcate = int.Parse(listGroupProduct[i].id.ToString());
                    List<string> Mang = new List<string>();
                    Mang = Arrayid(idcate);
                    Mang.Add(idcate.ToString());
                    var listProduct = db.tblProducts.Where(p => Mang.Contains(p.idCate.ToString()) && p.Active == true).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        chuoi.Append("<div class=\"Tear_1\">");
                        if (listProduct[j].New == true)
                        {
                            chuoi.Append(" <div class=\"Box_ProductNews\"></div>");
                        }
                        chuoi.Append("<div class=\"Box_Tietkiem\">");
                        chuoi.Append("<span class=\"kt0\">Bạn hãy</span>");
                        chuoi.Append("<span class=\"kt1\">gọi điện</span>");
                        chuoi.Append("<span class=\"kt3\">giá rẻ nhất</span>");
                        float nPrice = float.Parse(listProduct[j].Price.ToString());
                        float nPriceSale = float.Parse(listProduct[j].PriceSale.ToString());
                        float nSum = 100 - ((nPriceSale * 100) / nPrice);
                        //chuoi += "<span class=\"kt2\">" + Convert.ToInt32(nSum) + "<span>%</span></span>");
                        chuoi.Append(" </div>");
                        chuoi.Append("<div class=\"img\">");
                        chuoi.Append("<a href=\"/1/" + listProduct[j].Tag + "-"+listProduct[j].id+".aspx\" title=\"" + listProduct[j].Name + "\">");
                        chuoi.Append("<img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" title=\"" + listProduct[j].Name + "\" />");
                        chuoi.Append("</a>");
                        chuoi.Append("</div>");
                        chuoi.Append("<h3><a href=\"/1/" + listProduct[j].Tag + "-" + listProduct[j].id + ".aspx\" title=\"" + listProduct[j].Name + "\" class=\"Name\">" + listProduct[j].Name + "</a></h3>");
                        chuoi.Append("<div class=\"Info\">");
                        chuoi.Append("<div class=\"LeftInfo\">");
                        string note = "";
                        if (listProduct[j].PriceSaleActive > 10)
                        {
                            chuoi.Append("<span class=\"PriceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSaleActive) + "đ</span>");

                            if (listProduct[j].PriceSaleActive > listProduct[j].PriceSale)
                            {
                                note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[j].PriceSaleActive - listProduct[j].PriceSale) + "đ hoặc cao hơn nữa cho bạn !";
                            }
                        }
                        else
                            chuoi.Append("<span class=\"PriceSale\">Liên hệ</span>");
                        if (listProduct[j].Price < 10)
                        { chuoi.Append("<span class=\"Price\">Liên hệ</span>"); }
                        else
                        chuoi.Append("<span class=\"Price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                        chuoi.Append("</div>");
                        chuoi.Append("<div class=\"RightInfo\">");
                        chuoi.Append("<div class=\"Top_RightInfo\">");
                        chuoi.Append("<a href=\"\" title=\"\"><span></span></a>");
                        chuoi.Append("</div>");
                        chuoi.Append(" <div class=\"Bottom_RightInfo\">");
                        int ids = int.Parse(listProduct[j].id.ToString());
                        var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                        var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == ids).ToList();
                        if (checkfun.Count > 0)
                        {
                            for (int z = 0; z < listfuc.Count; z++)
                            {
                                int idfun = int.Parse(listfuc[z].id.ToString());
                                var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == ids).ToList();
                                if (connectfun.Count > 0)
                                {
                                    chuoi.Append("<a href=\"" + listfuc[z].Url + "\" rel=\"nofollow\" title=\"" + listfuc[z].Name + "\"><img src=\"" + listfuc[z].Images + "\" alt=\"" + listfuc[z].Name + "\" /></a>");
                                }
                            }

                        } chuoi.Append("</div>");
                        chuoi.Append("</div>");
                        chuoi.Append("</div>");
                        chuoi.Append("<div class=\"Qua\">");
                        chuoi.Append("<span class=\"ng ng1_" + ids + "\">Đang khuyến mại giá rẻ nhất Hà Nội,để biết giá hãy :</span>");
                        //chuoi.Append(" <div class=\"Content_Qua\">");
                        //chuoi.Append("<span class=\"dt\" onclick=\"javascript:return Goidien(" + ids + ")\">Gọi điện</span><span class=\"gd\"  onclick=\"javascript:return Dentructiep(" + ids + ")\">Đến trực tiếp</span><span class=\"ch\"  onclick=\"javascript:return Chat(" + ids + ")\">Chát</span>");
                        //chuoi.Append(" </div>");
                        chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[j].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[j].id + "','" + listProduct[j].Name + "','" + String.Format("{0:#,#}", listProduct[j].PriceSale) + "','"+note+"');\" title=\"Để lấy giá hỗ trợ tốt nhất Hà Nội\">Lấy giá tốt nhất</span></div>");
                        chuoi.Append("</div>");
                        chuoi.Append("</div>");
                        Mangphantu.Clear();
                    }
                    chuoi.Append("<div class=\"Clear\"></div>");
                    chuoi.Append("</div>");
                    chuoi.Append("</div>");
                    Mangphantu.Clear();
                }
            }
            else
            {
                chuoi.Append(" <div class=\"List_product\">");
                chuoi.Append(" <div id=\"Box_Name\">");
                chuoi.Append("<div id=\"Leff_BoxName\">   <h2><a href=\"/0/" + GroupProduct.Tag + "-" + GroupProduct.id + ".aspx\" title=\"" + GroupProduct.Name + "\">" + GroupProduct.Name + "</a></h2></div>");
                 chuoi.Append("<div id=\"Rigt_Box_Name\">");
                         chuoi.Append("<select>");
                             chuoi.Append("<option value=\"0\"> - Sắp xếp -</option>");
                             chuoi.Append("<option value=\"1\"> - Giá tăng dần -</option>");
                             chuoi.Append("<option value=\"1\"> - GIá giảm giần -</option>");
                        chuoi.Append(" </select>");
                        chuoi.Append("</div>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"Clear\"></div>");
                var listImageadwcenter = (from a in db.tblConnectImages join b in db.tblImages on a.idImg equals b.id where a.idCate == id && b.idCate == 11 select b).ToList();
                if (listImageadwcenter.Count > 0)
                {
                    chuoi.Append("<div class=\"Img_adwcenter\">");
                    for (int j = 0; j < listImageadwcenter.Count; j++)
                    {
                        chuoi.Append("<a href=\"" + listImageadwcenter[j].Url + "\" title=\"" + listImageadwcenter[j].Name + "\"><img src=\"" + listImageadwcenter[j].Images + "\" alt=\"" + listImageadwcenter[j].Name + "\" /></a>");

                    }

                    chuoi.Append("</div>");
                }
                chuoi.Append("<div class=\"ContentProduct\">");
                int idcate = int.Parse(GroupProduct.id.ToString());
                var listProduct = db.tblProducts.Where(p => p.idCate == idcate && p.Active == true).OrderBy(p => p.Ord).ToList();
                for (int j = 0; j < listProduct.Count; j++)
                {
                    chuoi.Append("<div class=\"Tear_1\">");
                    if (listProduct[j].New == true)
                    {
                        chuoi.Append(" <div class=\"Box_ProductNews\"></div>");
                    }
                    chuoi.Append("<div class=\"Box_Tietkiem\">");
                    chuoi.Append("<span class=\"kt0\">Bạn hãy</span>");
                    chuoi.Append("<span class=\"kt1\">gọi điện</span>");
                    chuoi.Append("<span class=\"kt3\">giá rẻ nhất</span>");
                    float nPrice = float.Parse(listProduct[j].Price.ToString());
                    float nPriceSale = float.Parse(listProduct[j].PriceSale.ToString());
                    float nSum = 100 - ((nPriceSale * 100) / nPrice);
                    //chuoi += "<span class=\"kt2\">" + Convert.ToInt32(nSum) + "<span>%</span></span>");
                    chuoi.Append(" </div>");
                    chuoi.Append("<div class=\"img\">");
                    chuoi.Append("<a href=\"/1/" + listProduct[j].Tag + "-" + listProduct[j].id + ".aspx\" title=\"" + listProduct[j].Name + "\">");
                    chuoi.Append("<img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" title=\"" + listProduct[j].Name + "\" />");
                    chuoi.Append("</a>");
                    chuoi.Append("</div>");
                    chuoi.Append("<h3><a href=\"/1/" + listProduct[j].Tag + "-" + listProduct[j].id + ".aspx\" title=\"" + listProduct[j].Name + "\" class=\"Name\">" + listProduct[j].Name + "</a></h3>");
                    chuoi.Append("<div class=\"Info\">");
                    chuoi.Append("<div class=\"LeftInfo\">");
                    string note = "";
                    if (listProduct[j].PriceSaleActive>10)
                    {
                        chuoi.Append("<span class=\"PriceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSaleActive) + "đ</span>");

                           if (listProduct[j].PriceSaleActive > listProduct[j].PriceSale)
                            {
                                note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[j].PriceSaleActive - listProduct[j].PriceSale) + "đ hoặc cao hơn nữa cho bạn !";
                            }
                    }
                    else
                    chuoi.Append("<span class=\"PriceSale\">Liên hệ</span>");
                    if (listProduct[j].Price<10)
                    chuoi.Append("<span class=\"Price\">Liên hệ</span>");
                    else
                     chuoi.Append("<span class=\"Price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");   
                    chuoi.Append("</div>");
                    chuoi.Append("<div class=\"RightInfo\">");
                    chuoi.Append("<div class=\"Top_RightInfo\">");
                    chuoi.Append("<a href=\"\" title=\"\"><span></span></a>");
                    chuoi.Append("</div>");
                    chuoi.Append(" <div class=\"Bottom_RightInfo\">");
                    int ids = int.Parse(listProduct[j].id.ToString());
                    var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                    var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == ids).ToList();
                    if (checkfun.Count > 0)
                    {
                        for (int z = 0; z < listfuc.Count; z++)
                        {
                            int idfun = int.Parse(listfuc[z].id.ToString());
                            var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == ids).ToList();
                            if (connectfun.Count > 0)
                            {
                                chuoi.Append("<a href=\"" + listfuc[z].Url + "\" rel=\"nofollow\" title=\"" + listfuc[z].Name + "\"><img src=\"" + listfuc[z].Images + "\" alt=\"" + listfuc[z].Name + "\" /></a>");
                            }
                        }

                    }
                    chuoi.Append("</div>");
                    chuoi.Append("</div>");
                    chuoi.Append("</div>");
                    chuoi.Append("<div class=\"Qua\">");
                    chuoi.Append("<span class=\"ng ng1_" + ids + "\">Đang khuyến mại giá rẻ nhất Hà Nội,để biết giá hãy :</span>");
                    //chuoi.Append(" <div class=\"Content_Qua\">");
                    //chuoi.Append("<span class=\"dt\" onclick=\"javascript:return Goidien(" + ids + ")\">Gọi điện</span><span class=\"gd\"  onclick=\"javascript:return Dentructiep(" + ids + ")\">Đến trực tiếp</span><span class=\"ch\"  onclick=\"javascript:return Chat(" + ids + ")\">Chát</span>");
                    //chuoi.Append(" </div>");
                    chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[j].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[j].id + "','" + listProduct[j].Name + "','" + String.Format("{0:#,#}", listProduct[j].PriceSale) + "','" + note + "');\" title=\"Để lấy giá hỗ trợ tốt nhất Việt Nam\">Lấy giá tốt nhất</span></div>");

                    chuoi.Append("</div>");
                    chuoi.Append("</div>");
                }
                chuoi.Append("<div class=\"Clear\"></div>");
                chuoi.Append("</div>");
                chuoi.Append("</div>");

            }


            ViewBag.chuoi = chuoi;

            StringBuilder catalogis = new StringBuilder();
            int idg=int.Parse(GroupProduct.id.ToString());
            var tblcatalogis = db.tblFiles.Where(p => p.idg == idg).ToList();
            if(tblcatalogis.Count>0)
            { 
            catalogis.Append("<div id=\"Download\">");
           catalogis.Append(" <a href=\""+tblcatalogis[0].File+"\" title=\""+tblcatalogis[0].Name+"\"><span></span></a>");
           catalogis.Append("</div>");
            }
            ViewBag.catalogis = catalogis;
            return View(GroupProduct);
        }
        public ActionResult Command(FormCollection collection, string tag)
        {
            if (collection["btnOrder"] != null)
            {

                Session["idProduct"] = collection["idPro"];
                Session["idMenu"] = collection["idCate"];
                Session["OrdProduct"] = collection["txtOrd"];
                Session["Url"] = Request.Url.ToString();
                return RedirectToAction("OrderIndex", "Order");
            }
            return View();
        }
        public ActionResult TabProduct(string tabs)
        {
            StringBuilder chuoiproduct = new StringBuilder();
            string[] Mang1 = StringClass.COnvertToUnSign1(tabs.ToUpper()).Split('-');
            string chuoitag = "";
            for (int i = 0; i < Mang1.Length; i++)
            {
                if (i == 0)
                    chuoitag += Mang1[i];
                else
                    chuoitag += " " + Mang1[i];
            }
            int dem = 1;
            string name = "";
            List<tblProduct> ListProducts = (from c in db.tblProducts select c).ToList();
            List<tblProduct> listProduct = ListProducts.FindAll(delegate(tblProduct math)
            {
                string kd = "";
                if(math.Tabs!=null)
                {
                     kd = StringClass.COnvertToUnSign1(math.Tabs.ToUpper());
                     if (StringClass.COnvertToUnSign1(math.Tabs.ToUpper()).Contains(chuoitag.ToUpper()))
                     {

                         string[] Manghienthi = math.Tabs.Split(',');
                         foreach (var item in Manghienthi)
                         {
                             if (dem == 1)
                             {
                                 var kiemtra = StringClass.COnvertToUnSign1(item.ToUpper()).Contains(chuoitag.ToUpper());
                                 if (kiemtra == true)
                                 {
                                     name = item;
                                     dem = 0;
                                 }
                             }
                         }

                         return true;
                     }

                     else
                         return false;
                }

                return false;
            }
            );
            StringBuilder chuoi = new StringBuilder();
            ViewBag.Title = "<title> " + name + "</title>";
            ViewBag.name = name;
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
            chuoi.Append("   <div class=\"Name_Cate\">");
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + name + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + name + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://bigsea.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + name + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            for (int j = 0; j < listProduct.Count; j++)
            {
                int idcate = int.Parse(listProduct[j].idCate.ToString());
                string GroupProduct = db.tblGroupProducts.First(p => p.id == idcate).Tag;
                string Url = GroupProduct;
               chuoiproduct.Append(" <div class=\"Tear_1\">");
               chuoiproduct.Append("<div class=\"img\">");
               chuoiproduct.Append("<a href=\"/1/" + listProduct[j].Tag + "-" + listProduct[j].id + ".aspx\" title=\"" + listProduct[j].Name + "\">");
               chuoiproduct.Append("<img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" />");
               chuoiproduct.Append("</a>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<a href=\"/1/" + listProduct[j].Tag + "-" + listProduct[j].id + ".aspx\" title=\"" + listProduct[j].Name + "\" class=\"Name\">" + listProduct[j].Name + "</a>");
               chuoiproduct.Append("<div class=\"Info\">");
               chuoiproduct.Append("<div class=\"LeftInfo\">");
                if (listProduct[j].PriceSaleActive> 10)
                    chuoiproduct.Append("<span class=\"PriceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSaleActive) + "đ</span>");
                else
                   chuoiproduct.Append("<span class=\"PriceSale\">Liên hệ</span>");
                if (listProduct[j].Price < 10)
                {
                   chuoiproduct.Append("<span class=\"Price\">Liên hệ</span>");
                }
                else
                   chuoiproduct.Append("<span class=\"Price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
               chuoiproduct.Append(" </div>");
               chuoiproduct.Append("<div class=\"RightInfo\">");
               chuoiproduct.Append("<div class=\"Top_RightInfo\">");
               chuoiproduct.Append("<a href=\"/Order/OrderIndex?idp=" + listProduct[j].id + "\" title=\"" + listProduct[j].Name + "\" rel=\"nofollow\"><span></span></a>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<div class=\"Bottom_RightInfo\">");
                int ids = int.Parse(listProduct[j].id.ToString());
                var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == ids).ToList();
                if (checkfun.Count > 0)
                {
                    for (int z = 0; z < listfuc.Count; z++)
                    {
                        int idfun = int.Parse(listfuc[z].id.ToString());
                        var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == ids).ToList();
                        if (connectfun.Count > 0)
                        {
                           chuoiproduct.Append("<a href=\"" + listfuc[z].Url + "\" rel=\"nofollow\" title=\"" + listfuc[z].Name + "\"><img src=\"" + listfuc[z].Images + "\" alt=\"" + listfuc[z].Name + "\" /></a>");
                        }
                    }

                }
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");
                chuoi.Append("<div class=\"Qua\">");
                chuoi.Append("<span class=\"ng ng1_" + ids + "\">Giá khuyến mại ? Chúng tôi cam kết giá giá rẻ nhất Việt Nam khi khách hàng :</span>");
                //chuoi.Append(" <div class=\"Content_Qua\">");
                //chuoi.Append("<span class=\"dt\" onclick=\"javascript:return Goidien(" + ids + ")\">Gọi điện</span><span class=\"gd\"  onclick=\"javascript:return Dentructiep(" + ids + ")\">Đến trực tiếp</span><span class=\"ch\"  onclick=\"javascript:return Chat(" + ids + ")\">Chát</span>");
                //chuoi.Append("</div>");
                chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[j].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[j].id + "','" + listProduct[j].Name + "','" + String.Format("{0:#,#}", listProduct[j].PriceSale) + "');\" title=\"Để lấy giá hỗ trợ giá tốt nhất Hà Nội\">Lấy giá tốt nhất</span></div>");

                chuoi.Append("</div>");
               chuoiproduct.Append("</div>");
            }

            ViewBag.chuoisanpham = chuoiproduct;
            return View();
           
        }
        public ActionResult SearchProduct(string tag)
        {
            StringBuilder chuoi = new StringBuilder();
            ViewBag.Title = "<title> Tìm kiếm : " + tag + "</title>";
            ViewBag.name = tag;
            ViewBag.Description = "<meta name=\"description\" content=\"" + tag + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tag + "\" /> ";
            chuoi.Append("   <div class=\"Name_Cate\">");
            StringBuilder chuoiproduct = new StringBuilder();
            var listProduct = db.tblProducts.Where(p => p.Active == true && p.Name.Contains(tag)).OrderBy(p => p.Ord).ToList();
            for (int j = 0; j < listProduct.Count; j++)
            {
                int idcate = int.Parse(listProduct[j].idCate.ToString());
                string GroupProduct = db.tblGroupProducts.First(p => p.id == idcate).Tag;
                string Url = GroupProduct;
               chuoiproduct.Append(" <div class=\"Tear_1\">");
               chuoiproduct.Append("<div class=\"img\">");
               chuoiproduct.Append("<a href=\"/1/" + listProduct[j].Tag + "-" + listProduct[j].id + ".aspx\" title=\"" + listProduct[j].Name + "\">");
               chuoiproduct.Append("<img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" />");
               chuoiproduct.Append("</a>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<a href=\"/1/" + listProduct[j].Tag + "-" + listProduct[j].id + ".aspx\" title=\"" + listProduct[j].Name + "\" class=\"Name\">" + listProduct[j].Name + "</a>");
               chuoiproduct.Append("<div class=\"Info\">");
               chuoiproduct.Append("<div class=\"LeftInfo\">");
               string note = "";
                if(listProduct[j].PriceSaleActive>10)
                {
                    chuoiproduct.Append("<span class=\"PriceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSaleActive) + "đ</span>");

                     if (listProduct[j].PriceSaleActive > listProduct[j].PriceSale)
                            {
                                note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[j].PriceSaleActive - listProduct[j].PriceSale) + "đ hoặc cao hơn nữa cho bạn !";
                            }
                }
                else
               chuoiproduct.Append("<span class=\"PriceSale\">Liên hệ</span>");
                if (listProduct[j].Price<10)
                { 
               chuoiproduct.Append("<span class=\"Price\">Liên hệ</span>");
                } 
                else
               chuoiproduct.Append("<span class=\"Price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
               chuoiproduct.Append(" </div>");
               chuoiproduct.Append("<div class=\"RightInfo\">");
               chuoiproduct.Append("<div class=\"Top_RightInfo\">");
               chuoiproduct.Append("<a href=\"/Order/OrderIndex?idp=" + listProduct[j].id + "\" title=\"" + listProduct[j].Name + "\" rel=\"nofollow\"><span></span></a>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("<div class=\"Bottom_RightInfo\">");
                int ids = int.Parse(listProduct[j].id.ToString());
                var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == ids).ToList();
                if (checkfun.Count > 0)
                {
                    for (int z = 0; z < listfuc.Count; z++)
                    {
                        int idfun = int.Parse(listfuc[z].id.ToString());
                        var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == ids).ToList();
                        if (connectfun.Count > 0)
                        {
                           chuoiproduct.Append("<a href=\"" + listfuc[z].Url + "\" rel=\"nofollow\" title=\"" + listfuc[z].Name + "\"><img src=\"" + listfuc[z].Images + "\" alt=\"" + listfuc[z].Name + "\" /></a>");
                        }
                    }

                }
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");
               chuoiproduct.Append("</div>");
                chuoi.Append("<div class=\"Qua\">");
                chuoi.Append("<span class=\"ng ng1_" + ids + "\">Giá khuyến mại ? Chúng tôi cam kết giá giá rẻ nhất Việt Nam khi khách hàng :</span>");
                //chuoi.Append(" <div class=\"Content_Qua\">");
                //chuoi.Append("<span class=\"dt\" onclick=\"javascript:return Goidien(" + ids + ")\">Gọi điện</span><span class=\"gd\"  onclick=\"javascript:return Dentructiep(" + ids + ")\">Đến trực tiếp</span><span class=\"ch\"  onclick=\"javascript:return Chat(" + ids + ")\">Chát</span>");
                //chuoi.Append(" </div>");
                chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[j].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[j].id + "','" + listProduct[j].Name + "','" + String.Format("{0:#,#}", listProduct[j].PriceSale) + "','" + note + "');\" title=\"Để lấy giá hỗ trợ trợ giá tốt nhất Hà Nội\">Lấy giá tốt nhất</span></div>");

                chuoi.Append("</div>");
               chuoiproduct.Append("</div>");
            }

            ViewBag.chuoisanpham = chuoiproduct;
            return View();
        }
        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            string tag = collection["txtSearch"];
            return Redirect("/Search/" + tag + "");
        }
    }
}