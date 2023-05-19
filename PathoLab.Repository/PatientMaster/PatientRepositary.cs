using Dapper;
using PathoLab.Domain.PatientMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.PatientMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.PatientMaster
{
   public class PatientRepositary: RepositoryBase, patientInterface
    {
        public PatientRepositary(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public async Task<List<patient>> GetAllPatient(patient p)
        {
            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "A"); 
                ObjParm.Add("@pname",p.FullName);

                var query = "USP_PatientMaster";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<patient>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;


            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<patient> Getbyidpatient(int PatientID)
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@UserId", PatientID);
                ObjParm.Add("@mode", "S");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PatientMaster";
                var GetAppById = Connection.Query<patient>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById[0];



            }
            catch (Exception ex)
            {
                return null;

            }
        }


        public async Task<int> insert(patient om)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@UserId", om.UserId);
                param.Add("@UserName", om.UserName);
                param.Add("@Password", om.Password);
                param.Add("@FullName", om.FullName);
                param.Add("@Email", om.Email);
                param.Add("@Mobile", om.Mobile);
                param.Add("@Gender", om.Gender);
                param.Add("@Address", om.Address);////////////
                param.Add("@Age", om.Age);
                param.Add("@City", om.City);
                param.Add("@DesignationId", om.DesignationId);
                param.Add("@@DepartmentId", om.DepartmentId);
                param.Add("@HospitalID", om.HospitalID);////////////
                param.Add("@Address1", om.Address1);
                param.Add("@PatientHistory", om.PatientHistory);

                param.Add("@mode", "IU");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                Connection.Execute("[USP_PatientMaster]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> delete(int om)
        {
            try
            {


                DynamicParameters param = new DynamicParameters();

                param.Add("@UserId", om);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                param.Add("@mode", "D");
               Connection.Execute("[USP_PatientMaster]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}

