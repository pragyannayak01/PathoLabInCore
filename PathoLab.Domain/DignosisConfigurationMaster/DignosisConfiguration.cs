using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.DignosisConfigurationMaster
{
    public class DignosisConfiguration
    {
        // UserId,DignosisConfigId ,LabTestId ,DignosisID,InvestigationName,MinimumPercentage,	MaximumPercentage ,Unit
        public int UserId { get; set; }
        public int DignosisConfigId { get; set; }
        public int LabTestId { get; set; }
        public string LabTestName { get; set; }
        public int DignosisID { get; set; }
        public string Name { get; set; }//Dignosis Name
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }
        public string InvestigationName { get; set; }
        public decimal MinimumPercentage { get; set; }
        public decimal MaximumPercentage { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
    }
}
