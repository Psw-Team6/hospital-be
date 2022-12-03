using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Examinations.Exceptions;

namespace HospitalLibrary.Examinations.Model
{
    public class Examination
    {
        public Guid Id { get; set; }
        public List<Symptom> Symptoms { get; private set; }
        public Appointment Appointment { get; private set; }
        public string Report { get; private set;}
        public const string InvalidDateMessage = "Invalid examination date.";
        public const string InvalidAppointmentStateMessage = "Invalid appointment state.";

        private void AddSymptoms(List<Symptom> symptoms)
        {
            Symptoms = symptoms;
        }

        public ReadOnlyCollection<Symptom> GetSymptoms()
        {
            var symptoms = Symptoms.AsReadOnly();
            return symptoms;
        }
        

        public Examination(Appointment appointment, string report,List<Symptom> symptoms)
        {
            Validate(appointment);
            Appointment = appointment;
            Appointment.AppointmentState = AppointmentState.Finished;
            AddSymptoms(symptoms);
            Report = report;
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