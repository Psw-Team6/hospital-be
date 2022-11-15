using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Repository
{
    public interface IFloorRepository: IGenericRepository<Floor>
    {
        Task<List<Floor>> GetAllFloors();
    }
}