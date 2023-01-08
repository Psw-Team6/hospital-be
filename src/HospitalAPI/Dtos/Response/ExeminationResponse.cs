using System;
using System.Collections.Generic;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.Appointments.Model;

namespace HospitalAPI.Dtos.Response
{
    public class ExeminationResponse
    {
        public Guid Id { get; set; }
        public Guid IdApp { get; set; }
        public List<SymptomResponse> Symptoms { get; set; }
        public List<ExaminationPrescriptionRequest> Prescriptions { get; set; }
        public string Anamnesis { get; set; }
        public Appointment Appointment { get; set; }
    }
}