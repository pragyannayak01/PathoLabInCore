using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.TestType
{
     public class TestTypeMaster
    {

        public int TestTypeID { get; set; } 
        public string TestType { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool DeletedFlag { get; set; }
    }
   
       


    
}
