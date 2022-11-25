using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Medicines.Repository
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        Task<List<Medicine>> GetAllMedicine();
    }
}