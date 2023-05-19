using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.PrescriptionMaster
{
    public class Prescription
    {
        public int UserId { get; set; }
        public int PatientID { get; set; }
        public string GuardianName { get; set; }
        public int PrescriptionId { get; set; }
        public string BloodPressure { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string OxygenLevel { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string Symptoms { get; set; }
        public string PatientHistory { get; set; }
        public int DignosisID { get; set; }
        public string DignosisCommaSepareted { get; set; } 
        //public int Id { get; set; }//Medicine Id
        //public string MedicineCommaSepareted { get; set; }
        public string Name { get; set; }//Medicine Name
        public int PresDignosisId { get; set; }
        public string OtherAdvice { get; set; }
        public string FullName { get; set; }
        public string DateOfAppointment { get; set; }
        //public string DateOfAppointmentString { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string DoctorName { get; set; }
     
        public int RegdNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; }
        //HospitalID,HospitalName,HEmail,HWebsite,Address,City,PinCode,GSTNo,State,RegstrationNo,LandlineNo,MobielNo
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string HEmail { get; set; }
        public string HWebsite { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public string GSTNo { get; set; }
        public string State { get; set; }
        public string RegstrationNo { get; set; }
        public string LandlineNo { get; set; }
        public string MobielNo { get; set; }


        //PrescribedMedicine
        public int PrdMedId { get; set; }
        public string MorningAfterMeal { get; set; }
        public string MorningBeforeMeal { get; set; }
        public string AfternoonAfterMeal { get; set; }
        public string AfternoonBeforeMeal { get; set; }
        public string NightAfterMeal { get; set; }
        public string NightBeforeMeal { get; set; }
        public List<Medicinee> Medicines { get; set; }

    }
    public class Medicinee
    {
        public int PrescriptionId { get; set; }
        public string MedicineName { get; set; }
        public int DoseId { get; set; }
        public string Duration { get; set; } 
        public string Id { get; set; }
        public int PrdMedId { get; set; }
        public string MorningAfterMeal { get; set; }
        public string MorningBeforeMeal { get; set; }
        public string AfternoonAfterMeal { get; set; }
        public string AfternoonBeforeMeal { get; set; }
        public string NightAfterMeal { get; set; }
        public string NightBeforeMeal { get; set; }
    }
}