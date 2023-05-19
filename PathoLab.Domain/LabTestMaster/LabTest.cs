using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.LabTestMaster
{
    public class LabTest
    {
        public int LabTestId { get; set; }
        public string LabTestName { get; set; }
        public int DignosisID { get; set; }
        public String Name { get; set; }
        public string Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool DeletedFlag { get; set; }

    }
}
