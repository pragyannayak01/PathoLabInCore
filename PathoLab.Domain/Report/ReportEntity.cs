using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.Report
{
    public class ReportEntity
    {
        public int SlNo { get; set; }
        public int RegdNo { get; set; }
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int SlotID { get; set; }
        public string SlotName { get; set; }
        public int PatientID { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public int AppointmentCount { get; set; }
        public int UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PatientName { get; set; }
    }
}
