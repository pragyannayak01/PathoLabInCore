using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PathoLab.Domain.SloteMaster
{
    public class SlotEntity
    {
        public int SlotID { get; set; }
        public string SlotName { get; set; }
        public string Slot_TimeFrom { get; set; }
        public string Slot_TimeTo { get; set; }
        [NotMapped]
        public int HospitalID { get; set; }
        [NotMapped]
        public string HospitalName { get; set; }
    }
}
