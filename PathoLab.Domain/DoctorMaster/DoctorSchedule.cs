using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.DoctorMaster
{
    public class DoctorSchedule
    {
        public int AppointmentId { get; set; }
        public int RegdNo { get; set; }
        public int HospitalID { get; set; }
        public int DepartmentId { get; set; }
        public int SlotID { get; set; }
        public int PatientID { get; set; }
        public int CreatedBy { get; set; }
        public int UserId { get; set; }
        public string HospitalName { get; set; }
        public string SlotName { get; set; }
        public string Slot_TimeFrom { get; set; }
        public string Slot_TimeTo { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfAppointment { get; set; }

    }
}
