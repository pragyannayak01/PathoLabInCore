using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.PatientAppointmentMaster
{
    public class PatientAppointment
    {
        public int AppointmentId { get; set; }
        public int RegdNo { get; set; }
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int SlotID { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public string Password { get; set; }
        public int CreatedBy { get; set; }
        public int UserId { get; set; }
        public string SlotName { get; set; }
        public string Slot_TimeFrom { get; set; }
        public string Slot_TimeTo { get; set; }
        public string FullName { get; set; }
    }
}
