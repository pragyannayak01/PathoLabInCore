using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.DesignationMaster
{
     public class DesignationName
    {
        //DesignationId,Designation
        public int  DesignationId { get; set; }
        public string Designation { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }
    }
}
