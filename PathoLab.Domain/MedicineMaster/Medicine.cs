using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PathoLab.Domain.MedicineMaster
{
    public class Medicine
    {


        public int Id { get; set; } = 0;
        public int SubCatagoryId { get; set; } = 0;
        public int CatagoryId { get; set; } = 0;
        public string Name { get; set; } = null;
        public string ShortName { get; set; } = null;
        public string ProductCode { get; set; } = null;
        public double MRP { get; set; } = 0.0;
        public int HsnId { get; set; } = 0;
        public string Expiry { get; set; } = null;
        public string ExpiryString { get; set; } = null;
        public string Manufacture { get; set; } = null;
        public string Description { get; set; } = null;
        public int UnitId { get; set; } = 0;
        public int BrandId { get; set; } = 0;
        [NotMapped]
        public string UnitName { get; set; } = null;
        public string BrandName { get; set; } = null;
        public string ddlHSnCode { get; set; } = null;
        public string SubCatagoryName { get; set; } = null;
        public string CatagoryName { get; set; } = null;










    }
    public class Subcatagory
    {
        public string SubCatagoryName { get; set; } = null;
        public int SubCatagoryId { get; set; } = 0;

    }
}
