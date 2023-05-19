using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.DignosisMaster
{
     public class Dignosis
    {
        //DignosisID,Name,Address,City,MobileNo)//DignosisCatagoryID//DignosisCategoryName
        public int DignosisID { get; set; }
        public string EncodDignosisID { get; set; }
        public int DignosisCatagoryID { get; set; }
        public string Name { get; set; }

        public string DignosisCategoryName { get; set; }
        public string Category { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }

    }
}
