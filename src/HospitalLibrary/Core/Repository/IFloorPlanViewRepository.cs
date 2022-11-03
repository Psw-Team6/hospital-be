using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository
{
    public interface IFloorPlanViewRepository: IGenericRepository<FloorPlanView>
    {
        
        Task<List<FloorPlanView>> GetAllFloorPlanViews();
        
    }
}