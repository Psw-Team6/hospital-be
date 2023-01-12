using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Patients.Repository
{
    public interface IPatientHealthStateNotificationRepository:IGenericRepository<PatientHealthStateNotification>
    {
        public Task<List<PatientHealthStateNotification>> GetAllNotifications(Guid patientId);
    }
}