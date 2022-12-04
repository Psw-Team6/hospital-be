using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Examinations.Exceptions;

namespace HospitalLibrary.Examinations.Model
{
    public class Examination
    {
        public Guid Id { get; private set; }
        public List<Symptom> Symptoms { get; private set; }
        public Appointment Appointment { get; private set; }
        public string Anamnesis { get; private set;}
        public const string InvalidDateMessage = "Invalid examination date.";
        public const string InvalidAppointmentStateMessage = "Invalid appointment state.";
        public List<ExaminationPrescription> Prescriptions { get; private set; }

        private void AddSymptoms(List<Symptom> symptoms)
        {
            Symptoms = symptoms;
        }
        private void AddPrescription(List<ExaminationPrescription> prescriptions)
        {
            Prescriptions = prescriptions;
        }

        public ReadOnlyCollection<Symptom> GetSymptoms()
        {
            var symptoms = Symptoms.AsReadOnly();
            return symptoms;
        }
        

        public Examination(Appointment appointment, string anamnesis,List<Symptom> symptoms,List<ExaminationPrescription> prescriptions)
        {
            Validate(appointment);
            Appointment = appointment;
            Appointment.AppointmentState = AppointmentState.Finished;
            AddSymptoms(symptoms);
            AddPrescription(prescriptions);
            Anamnesis = anamnesis;
        }
        public Examination()
        {
        }

        private static void Validate(Appointment appointment)
        {
            if (!appointment.CanBeExamined())
            {
                throw new ExaminationInvalidDate(InvalidDateMessage);
            }

            if (appointment.AppointmentState != AppointmentState.Pending)
            {
                throw new AppointmentExaminationInvalidState(InvalidAppointmentStateMessage);
            }
        }
    }
}