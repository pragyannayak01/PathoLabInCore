using Dapper;
using PathoLab.Domain.MedicineMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.Medicine_Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.MedicineMaster
{
   public class MedicineRepository : RepositoryBase, IMedicine
    {
        public MedicineRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public async Task<List<Medicine>> GetAllMedicine(Medicine p)
        {
            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "A");
                ObjParm.Add("@CatagoryId", p.CatagoryId);
                ObjParm.Add("@SubCatagoryId", p.SubCatagoryId);
                ObjParm.Add("@Name", p.Name);
                var query = "USP_PL_MedicineMaster";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var GetAppById = Connection.Query<Medicine>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;


            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<Medicine> GetMedicineById(int MedicineID)
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Id", MedicineID);
                ObjParm.Add("@mode", "S");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PL_MedicineMaster";
                var GetAppById = Connection.Query<Medicine>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById[0];



            }
            catch (Exception ex)
            {
                return null;

            }
        }


        public async Task<int> insert(Medicine om)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@Id", om.Id);
                param.Add("@SubCatagoryId", om.SubCatagoryId);
                param.Add("@CatagoryId", om.CatagoryId);
                param.Add("@Name", om.Name);
                param.Add("@ShortName", om.ShortName);
                param.Add("@ProductCode", om.ProductCode);
                param.Add("@MRP", om.MRP);
                param.Add("@HsnId", om.HsnId);////////////
                param.Add("@Expiry", om.Expiry);
                param.Add("@Manufacture", om.Manufacture);
                param.Add("@Description", om.Description);
                param.Add("@UnitId", om.UnitId);
                param.Add("@BrandId", om.BrandId);////////////
            

                param.Add("@mode", "IU");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                Connection.Execute("[USP_PL_MedicineMaster]", param, commandType: CommandType.StoredProcedure);
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

                param.Add("@Id", om);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                param.Add("@mode", "D");
                Connection.Execute("[USP_PL_MedicineMaster]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<Medicine>> UnitBind()
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
              
                ObjParm.Add("@mode", "UnitBind");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PL_MedicineMaster";
                var GetAppById = Connection.Query<Medicine>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;



            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<List<Medicine>> BrandBind()
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();

                ObjParm.Add("@mode", "BrandBind");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PL_MedicineMaster";
                var GetAppById = Connection.Query<Medicine>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;



            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<List<Medicine>> HsnCodeBind()
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();

                ObjParm.Add("@mode", "HsnCodeBind");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PL_MedicineMaster";
                var GetAppById = Connection.Query<Medicine>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;



            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<List<Medicine>> CatagoryBind()
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();

                ObjParm.Add("@mode", "CatagoryBind");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PL_MedicineMaster";
                var GetAppById = Connection.Query<Medicine>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;



            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<List<Subcatagory>> SubCatagoryBind(int catid)
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@CatagoryId", catid);
                ObjParm.Add("@mode", "SubCatagoryBind");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PL_MedicineMaster";
                var GetAppById = Connection.Query<Subcatagory>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;



            }
            catch (Exception ex)
            {
                return null;

            }
        }

      
    }
}

