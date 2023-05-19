using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PathoLab.Domain.UserRegistration
{
    public class UserModel
    {
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = null;
        public string Password { get; set; } = null;
        public string Passwordconfirm { get; set; } = null;
        public string FullName { get; set; } = null;
        public string Email { get; set; } = null;
        public string Mobile { get; set; } = null;
        public string Gender { get; set; } = null;
        public string Address { get; set; } = null;
        public string Address1 { get; set; } = null;
        public string City { get; set; } = null;
        public int Age { get; set; } = 0;
        public string DOB { get; set; } = null;
        public int DesignationId { get; set; } = 0;//[Designation]
        [NotMapped]
        public string Designation { get; set; } = null;
        public int DepartmentId { get; set; } = 0;
        public int HospitalID { get; set; } = 0;
        public int CreatedBy { get; set; } = 0;
        public int UpdatedBy { get; set; } = 0;
        public int DeletedFlag { get; set; } = 0;
    }
}
