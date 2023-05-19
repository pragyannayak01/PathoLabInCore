using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.DoctorMaster 
{
    public class Doctor
    {
     //DoctorID ,Prefix ,DoctorName ,Designation ,Department ,HospitalName ,RegnNo ,Mobile ,Fees 
        public int DoctorID  { get; set; }
        public string Prefix { get; set; }
        public string DoctorName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string HospitalName { get; set; }
        public string RegnNo { get; set; }
        public string Mobile { get; set; }
        public decimal Fees { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }


    }
}
