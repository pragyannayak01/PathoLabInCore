using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.DepartmentMaster
{
    public class DepartmentName
    {
        //DepartmentId,Department
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }
    }
}
