using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomBedService : IRoomBedService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomBedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoomBed>> GetAll()
        {
            return await _unitOfWork.RoomBedRepository.GetAllAsync();
        }
        public async Task UpdateRoomAvailability(PatientAdmission admission)
        {
            var updateBed = await _unitOfWork.RoomBedRepository.GetByIdAsync(admission.SelectedBedId);
            if (updateBed == null) throw new PatientAdmissionException("Bed doesn't exists!");
            updateBed.Update(true);
            await _unitOfWork.RoomBedRepository.UpdateAsync(updateBed);
            await _unitOfWork.CompleteAsync();
        }
    }
}