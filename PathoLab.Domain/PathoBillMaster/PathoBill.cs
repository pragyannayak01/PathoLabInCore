using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PathoLab.Domain.PathoBilMasterl
{
    public class PathoBill
    {
        //UserId,PathoBillId ,CollectionId ,LabTestId ,LabTestName ,Price ,SGST,CGST ,PayMode,Mobile ,Age ,Email,FullName,DateOfAppointment,DoctorName
        public int SampleColNo { get; set; } //for sampleCollection
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Barcode { get; set; }
        public string BarcodeNo { get; set; } //for sampleCollection
        public int PathoBillId { get; set; }//for sampleCollection
        public string DignosisConfigId { get; set; }
        public string Value { get; set; }
        public string CollectionId { get; set; }//for sampleCollection
        public int LabTestId { get; set; }//for sampleCollection
        public string LabTestName { get; set; }
        public int Price { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public string PayMode { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string DateOfAppointment { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }
        public bool CollectionFlag { get; set; }
        public string ReportedOn { get; set; }
        //HospitalID,HospitalName,HEmail,HWebsite,Address,City,PinCode,GSTNo,State,RegstrationNo,LandlineNo,MobielNo
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string HEmail { get; set; }
        public string HWebsite { get; set; }
        public string HAddress { get; set; }
        public string HCity { get; set; }
        public int HPinCode { get; set; }
        public string HGSTNo { get; set; }
        public string HState { get; set; }
        public string HRegstrationNo { get; set; }
        public int HLandlineNo { get; set; }
        public int HMobielNo { get; set; }
        public List<LabTests> Tests { get; set; }
        public List<PathoTestValue> TestValues { get; set; }
        public List<Sample> CollectionSample { get; set; }


    }
    public class Sample
    {
        public int Sid { get; set; }
        public int SampleColNo { get; set; }
        public int PathoBillId { get; set; }
        public int LabTestId { get; set; }
        public string Barcode { get; set; }
        // public string CollectionId { get; set; }
    }
    public class LabTests
    {
        public int PathoBillId { get; set; }
        public int LabTestId { get; set; }
        public string LabTestName { get; set; }
        public int Price { get; set; }
    }
    public class PathoTestValue
    {
        public int PathoBillId { get; set; }
        public int LabTestId { get; set; }
        public int DignosisConfigId { get; set; }
        public string Value { get; set; }
    }


}
