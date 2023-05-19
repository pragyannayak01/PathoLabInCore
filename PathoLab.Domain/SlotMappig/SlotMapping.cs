using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.SlotMappig
{
    public class SlotMapping
    {
        public int SMId { get; set; }
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public int SlotID { get; set; }
        public string SlotName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int DaysId { get; set; }
        public string Day { get; set; }
        public string Slot_TimeFrom { get; set; }
        public string Slot_TimeTo { get; set; }
        public int AvailableCapacity { get; set; }

    }
}
