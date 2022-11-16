using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Repository
{
    public interface IBuildingRepository: IGenericRepository<Building>
    {
        Task<IEnumerable<Building>> GetAllBuildings();
        
    }
}