using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.Patients.Service
{
    public class PatientAdmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeneratePdfReportService _reportService;
        private readonly IRoomBedService _bedService;

        public PatientAdmissionService(IUnitOfWork unitOfWork, IGeneratePdfReportService reportService, IRoomBedService bedService)
        {
            _unitOfWork = unitOfWork;
            _reportService = reportService;
            _bedService = bedService;
        }

        public async Task<object> GetAll()
        {
            return await _unitOfWork.PatientAdmissionRepository.GetAllPatientAdmissions();
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
            var newAdmission = await _unitOfWork.PatientAdmissionRepository.CreateAsync(admission);
            TreatmentReport report = new TreatmentReport
            {
                PatientAdmissionId = newAdmission.Id
            };
            var newReport = await _unitOfWork.TreatmentReportRepository.CreateAsync(report);
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

        public async Task<PatientAdmission> GetAdmissionWithPatientById(Guid id)
        {
            return await _unitOfWork.PatientAdmissionRepository.GetPatientAdmissionByIdAsync(id);
        }

        public async Task<Boolean> DischargePatient(PatientAdmission admissionRequest)
        {
            var admission = await _unitOfWork.PatientAdmissionRepository.GetByIdAsync(admissionRequest.Id);
            CheckDischargePatientRequest(admission);
            admission.Update(admissionRequest.ReasonOfDischarge,DateTime.Now);
            await _bedService.UpdateRoomAvailability(admission);
            await CollectDataForGeneratingReport(admission);
            await _unitOfWork.PatientAdmissionRepository.UpdateAsync(admission);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private async Task CollectDataForGeneratingReport(PatientAdmission admission)
        {
            var admissionReport = await _unitOfWork.PatientAdmissionRepository.GetPatientAdmissionByIdAsync(admission.Id);
            var treatmentReport = await _unitOfWork.TreatmentReportRepository.FindByPatientAdmission(admission.Id);
            if (treatmentReport == null) throw new TreatmentReportException("Treatment report not found!");
            _reportService.GeneratePdfReport(admissionReport, treatmentReport);
        }

        private static void CheckDischargePatientRequest(PatientAdmission admission)
        {
            if (admission == null) throw new PatientAdmissionException("Patient admission not found!");
            if (admission.DateOfDischarge != null) throw new PatientDischargeException("Patient is already discharged!");
        }
        
        public async Task<object> GetAllHospitalized()
        {
            return await _unitOfWork.PatientAdmissionRepository.GetAllHospitalized();
        }
        
    }
}