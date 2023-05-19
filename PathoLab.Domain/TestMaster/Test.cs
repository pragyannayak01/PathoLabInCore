using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.TestMaster
{
    public class Test
    {
        public int TestID { get; set; }
        public string TestType { get; set; }
        public  string TestName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool DeletedFlag { get; set; }
    }
}
