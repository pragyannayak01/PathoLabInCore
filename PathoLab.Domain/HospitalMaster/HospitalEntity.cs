using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.HospitalMaster
{
    public class HospitalEntity
    {
        // HospitalID,HospitalName,RegstrationNo,LandlineNo,Address,City,State,PinCode,ContactPerson,MobielNo,GSTNo
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string RegstrationNo { get; set; }
        public string LandlineNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }
        public string ContactPerson { get; set; }
        public string MobielNo { get; set; }
        public string GSTNo { get; set; }
        public string HEmail { get; set; }
        public string HWebsite { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }
       // public string HWebsite { get; set; }
        //public string HEmail { get; set; }
    }
}
