using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.Account
{
    public class User 
    {
        //id, UserId, Password, Name, Email, Mobile, Gender, Address, RoleId, CreatedOn, CreatedBy, UpdatedBy, UpdatedOn, DeletedFlag
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int HospitalID { get; set; } 
        public string HospitalName { get; set; }       
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public int DesignationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool DeletedFlag { get; set; } 
    }
}
