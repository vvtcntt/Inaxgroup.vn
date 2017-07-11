using System;
using System.Collections.Generic;

namespace INAXGROUP.Models
{
    public partial class tblProductSyn
    {
        public int id { get; set; }
        public string Code { get; set; }
        public string CodeSyn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Info { get; set; }
        public string Content { get; set; }
        public string Parameter { get; set; }
        public string ImageLinkThumb { get; set; }
        public string ImageLinkDetail { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> PriceSale { get; set; }
        public Nullable<bool> Vat { get; set; }
        public string Warranty { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Transport { get; set; }
        public string Access { get; set; }
        public string Sale { get; set; }
        public Nullable<int> Ord { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> New { get; set; }
        public Nullable<bool> ViewHomes { get; set; }
        public Nullable<int> Visit { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> idUser { get; set; }
    }
}
