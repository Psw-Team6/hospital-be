﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients.Repository
{
    public class PatientHealthStateNotificationRepository:GenericRepository<PatientHealthStateNotification>,IPatientHealthStateNotificationRepository
    {
        public PatientHealthStateNotificationRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PatientHealthStateNotification>> GetAllNotifications(Guid doctorId)
        {
            return await DbSet.Where(n => n.Patient.Doctor.Id == doctorId)
                .Include(n=> n.Patient).ToListAsync();
        }
    }
}