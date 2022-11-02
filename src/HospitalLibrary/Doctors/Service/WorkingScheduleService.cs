using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Doctors.Service
{
    public class WorkingScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkingScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
    }
}