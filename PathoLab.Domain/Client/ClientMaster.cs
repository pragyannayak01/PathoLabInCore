using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.Client
{
  public  class ClientMaster
    {
        //ClintID, Name ,Address, City, phoneno, WhatsAppNo, ReferByClientId, EDate,Status
        public int ClintID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string phoneno { get; set; }
        public string WhatsAppNo { get; set; }
        public int ReferByClientId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool DeletedFlag { get; set; }
        //public DateTime EDate { get; set; }
        //public string Status { get; set; }


    }
}
