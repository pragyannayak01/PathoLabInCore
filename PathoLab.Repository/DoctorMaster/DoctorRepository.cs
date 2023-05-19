using Dapper;
using PathoLab.Domain.DoctorMaster;
using PathoLab.IRepository.DoctorMaster;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.DoctorMaster
{
    public class DoctorRepository:RepositoryBase, IDoctorRepository
    {
        public DoctorRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> Create(Doctor entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DoctorID", entity.DoctorID);
                param.Add("@Prefix", entity.Prefix);
                param.Add("@DoctorName", entity.DoctorName);
                param.Add("@Designation ", entity.Designation);
                param.Add("@Department", entity.Department);
                param.Add("@HospitalName", entity.HospitalName);
                param.Add("@RegnNo ", entity.RegnNo);
                param.Add("@Mobile", entity.Mobile);
                param.Add("@Fees", entity.Fees);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.DoctorID==0)
                {
                    param.Add("@action", "Insert");
                }
                else
                {
                    param.Add("@action", "Update");
                }
                var query = "USP_PL_DoctorMaster";
                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<int> Delete(int DoctorID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DoctorID", DoctorID);
                param.Add("@action", "Delete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_DoctorMaster", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Doctor>> GetAll(Doctor doctor)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SelectAll");
                param.Add("@DoctorName", doctor.DoctorName);
                param.Add("@Designation ", doctor.Designation);
                param.Add("@Department", doctor.Department);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<Doctor>("USP_PL_DoctorMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Doctor> GetOne(int DoctorID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DoctorID", DoctorID);
                param.Add("@action", "SelectOne");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<Doctor>("USP_PL_DoctorMaster", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
