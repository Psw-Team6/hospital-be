using System;
using System.Collections.Generic;
using HospitalAPI.Dtos.Response;

namespace HospitalAPI.Dtos.Request
{
    public class ExaminationRequest
    {
        public Guid IdApp { get; set; }
        public List<SymptomResponse> Symptoms { get; set; }
        public List<ExaminationPrescriptionRequest> Prescriptions { get; set; }
        public string Anamnesis { get; set; }
        public List<DomainEventRequest> Changes { get; set; }
    }
}