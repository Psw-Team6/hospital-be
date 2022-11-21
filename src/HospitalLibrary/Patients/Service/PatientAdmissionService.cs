using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.Patients.Service
{
    public class PatientAdmissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientAdmissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetAll()
        {
            return (List<PatientAdmission>)await _unitOfWork.PatientAdmissionRepository.GetAllAsync();
        }
        
        public async Task<PatientAdmission> GetById(Guid id)
        {
            var admission = await _unitOfWork.PatientAdmissionRepository.GetByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return admission;
        }
        
        public async Task<PatientAdmission> CreateAdmission(PatientAdmission admission)
        {
            List<RoomBed> beds = (List<RoomBed>)await _unitOfWork.RoomBedRepository.GetAllAsync();
            RoomBed bed = new RoomBed();
            Room room = new Room();
            Boolean bedExists = false;
            foreach (var b in beds) {
                if (b.IsFree)
                {
                    bed = b;
                    room = await _unitOfWork.RoomRepository.GetByIdAsync(b.RoomId);
                    bed.IsFree = false;
                    bedExists = true;
                    break;
                }
            }
            if (!(bedExists))
            {
                return null;
            }
            
            await _unitOfWork.RoomBedRepository.UpdateAsync(bed);
            admission.SelectedBedId = bed.Id;
            admission.SelectedRoomId = room.Id;
            TreatmentReport report = new TreatmentReport();
            report.PatientId = admission.PatientId;
            var newReport = await _unitOfWork.TreatmentReportRepository.CreateAsync(report);
            var newAdmission = await _unitOfWork.PatientAdmissionRepository.CreateAsync(admission);
            await _unitOfWork.CompleteAsync();
            return newAdmission;
        }

        public async Task<Boolean> IsHospitalized(PatientAdmission admission)
        {
            List<PatientAdmission> patientAdmissions = (List<PatientAdmission>)await _unitOfWork.PatientAdmissionRepository.GetAllAsync();
            foreach(var p in patientAdmissions)
            {
                if (p.PatientId.Equals(admission.PatientId) && p.DateOfDischarge == null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Boolean> DischargePatient(PatientAdmission admissionRequest)
        {
            var admission = await _unitOfWork.PatientAdmissionRepository.GetByIdAsync(admissionRequest.Id);
            if (admission == null) throw new PatientAdmissionException("Patient admission not found!");
            if (admission.DateOfDischarge != null) throw new PatientDischargeException("Patient is already discharged!");
            admission.Update(admissionRequest.ReasonOfDischarge,DateTime.Now);
            await UpdateRoomAvailability(admission);
            await _unitOfWork.PatientAdmissionRepository.UpdateAsync(admission);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private async Task UpdateRoomAvailability(PatientAdmission admission)
        {
            var updateBed = await _unitOfWork.RoomBedRepository.GetByIdAsync(admission.SelectedBedId);
            if (updateBed == null) throw new PatientAdmissionException("Bed doesn't exists!");
            updateBed.Update(true);
            await _unitOfWork.RoomBedRepository.UpdateAsync(updateBed);
            await _unitOfWork.CompleteAsync();
        }
    }
}