using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PathoLab.IRepository.Account;
using PathoLab.IRepository.Client;
using PathoLab.IRepository.DegisnationMaster;
using PathoLab.IRepository.DepartmentMaster;
using PathoLab.IRepository.DignosisMaster;
using PathoLab.IRepository.DoctorMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.PatientMaster;
using PathoLab.IRepository.RoleMaster;
using PathoLab.IRepository.TestType;
using PathoLab.IRepository.UserRegistration;
using PathoLab.Repository.Account;
using PathoLab.Repository.Client;
using PathoLab.Repository.DepartmentMaster;
using PathoLab.Repository.DesignationMaster;
using PathoLab.Repository.DignosisMaster;
using PathoLab.Repository.DoctorMaster;
using PathoLab.Repository.Factory;
using PathoLab.Repository.PatientMaster;
using PathoLab.Repository.RoleMaster;
using PathoLab.Repository.TestType;
using PathoLab.Repository.UserRegistration;
using PathoLab.Repository.TestMaster;
using PathoLab.IRepository.TestMaster;
using PathoLab.IRepository.HospitalMaster;
using PathoLab.Repository.HospitalMaster;
using PathoLab.IRepository.SlotMaster;
using PathoLab.Repository.SlotMaster;
using PathoLab.IRepository.SlotMappingMaster;
using PathoLab.Repository.SlotMappingMaster;
using PathoLab.IRepository.PatientAppointmentMaster;
using PathoLab.Repository.PatientAppointmentMaster;
using PathoLab.IRepository.Medicine_Master;
using PathoLab.Repository.MedicineMaster;
using PathoLab.IRepository.PrescriptionMaster;
using PathoLab.Repository.PrescriptionMaster;
using PathoLab.IRepository.DoseMaster;
using PathoLab.Repository.DoseMaster;
using PathoLab.IRepository.LabTestMaster;
using PathoLab.Repository.LabMaster;
using PathoLab.IRepository.DignosisConfigurationMaster;
using PathoLab.Repository.DignosisConfigurationMaster;
using PathoLab.IRepository.PathoBillMaster;
using PathoLab.Repository.PathoBillMaster;
using PathoLab.IRepository.MenuMaster;
using PathoLab.Repository.MenuMaster;
using PathoLab.Repository.SubMenuMaster;
using PathoLab.IRepository.SubMenuMaster;
using PathoLab.IRepository.PermissionMaster;
using PathoLab.Repository.PermissionMaster;
using PathoLab.IRepository.Report;
using PathoLab.Repository.Report;

namespace PathoLab.Web.DIContainer
{
    public static class CustomContainer
    {
        public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
        {
            IConnectionFactory connectionFactory = new ConnectionFactory(configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(connectionFactory);
            //Pragyan
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IDoctorRepository, DoctorRepository>();
            services.AddSingleton<IDesignationRepository, DesignationRepository>();
            services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
            services.AddSingleton<ISlotMappingRepository, SlotMappingRepository>();
            services.AddSingleton<IPatientAppointmentRepository, PatientAppointmentRepository>();
            services.AddSingleton<IPatientBookingRepository, PatientBookingRepository>();
            services.AddSingleton<IDignosisConfigurationRepository, DignosisConfigurationRepository>();
            services.AddSingleton<IPathoBillRepository, PathoBillRepository>();
            services.AddSingleton<IMenuRepository, MenuRepository>();
            services.AddSingleton<ISubMenuRepository, SubMenuRepository>();
            services.AddSingleton<IPermissionRepository, PermissionRepository>();
            services.AddSingleton<IReportRepository, ReportRepository>();

            //Subhasmita
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<ITestType, TestTypeRepository>();
            services.AddSingleton<ITestRepository, TestRepository>();
            services.AddSingleton<IHospitalRepository, HospitalRepository>();
            services.AddSingleton<IDoctorSchedule, DoctorScheduleRepository>();
            services.AddSingleton<IPrescriptionRepository, PrescriptionRepository>();
            services.AddSingleton<ILabtest, LabTestRepository>();
            //Sushant
            services.AddSingleton<IUser, UserRegitrationRepositry>();
            services.AddScoped<patientInterface, PatientRepositary>();
            services.AddScoped<IRole, RoleRepository>();
            services.AddScoped<Slot_interface, Slot_Repository>();
            services.AddScoped<IMedicine, MedicineRepository>();
            services.AddScoped<DoseInterface, DoseRepository>();
            services.AddSingleton<IDignosisRepository, DignosisRepository>();

        }
    }
}
