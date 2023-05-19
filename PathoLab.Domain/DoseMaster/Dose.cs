using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.DoseMaster
{
   public class Dose
    {
        public int MedicineId { get; set; }
        public int UserId { get; set; }
         public int DoseId { get; set; }

        public string DoseIdString { get; set; }///////////for storing encrypt data//
        public string SubCatagoryName { get; set; }
        public int SubCatagoryId { get; set; }

        public string DoseName { get; set; }

        public string Name { get; set; }///Medicine Name
        public int id { get; set; }///Medicine Id
        public string CatagoryName { get; set; }
        public int CatagoryId { get; set; } 
    }
}
