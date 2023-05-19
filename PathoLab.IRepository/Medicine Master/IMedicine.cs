using PathoLab.Domain.MedicineMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.Medicine_Master
{
   public interface IMedicine
    {
        Task<int> insert(Medicine p);

        Task<int> delete(int p);

        Task<List<Medicine>> GetAllMedicine(Medicine p);

        Task<Medicine> GetMedicineById(int id);



        Task<List<Medicine>> UnitBind();
        Task<List<Medicine>> BrandBind();
        Task<List<Medicine>> HsnCodeBind();



        Task<List<Medicine>> CatagoryBind();
        Task<List<Subcatagory>> SubCatagoryBind(int cat);
       

    }
}
