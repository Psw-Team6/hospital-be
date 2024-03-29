﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Appointments.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsForDoctor(Guid doctorId)
        {
           return await DbSet.Where(x => x.DoctorId == doctorId)
                .Include(x => x.Duration).Include(x => x.Patient)
                .ToListAsync();
        }
        
        public async Task<List<Appointment>> GetAllAppointmentsForRoom(Guid roomId)
        {
            return await DbSet.Where(x => x.Doctor.RoomId == roomId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsForPatient(Guid patientId)
        {
            return await DbSet.Where(x => x.PatientId == patientId)
                .Include(x => x.Duration).Include(x => x.Doctor)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsForExamination(Guid doctorId)
        {
            return await DbSet.Where(x => x.DoctorId == doctorId)
                .Where(x => x.AppointmentState == AppointmentState.Pending 
                            && x.Duration.To < DateTime.Now.AddHours(2) && x.Duration.From > DateTime.Now.AddDays(-2))
                .Include(x => x.Duration).Include(x => x.Patient)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentsById(Guid appointmentId)
        {
            return await DbSet.Where(x => x.Id == appointmentId)
                .Include(x => x.Duration).Include(x => x.Patient).FirstAsync();
        }
    }
}