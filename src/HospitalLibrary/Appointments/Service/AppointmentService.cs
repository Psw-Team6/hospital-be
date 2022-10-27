﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.Appointments.Service
{
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            var appointments =  await _unitOfWork.AppointmentRepository.GetAllAsync();
            return appointments;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
           //Task.FromException(new Exception("aaa"));
            var appointmentCreated = await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return appointmentCreated;
        }

        public async Task<Appointment> GetById(Guid id)
        {
            return await _unitOfWork.AppointmentRepository.GetByIdAsync(id);
        }
    }
}