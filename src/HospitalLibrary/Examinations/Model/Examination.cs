﻿using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Exceptions;

namespace HospitalLibrary.Examinations.Model
{
    public class Examination : EventSourcedAggregate<EventStoreExaminationType>
    {

        public IEnumerable<Symptom> Symptoms { get;  set; }

        public Appointment Appointment { get;  set; }
        public string Anamnesis { get; private set;}
        public const string InvalidDateMessage = "Invalid examination date.";
        public const string InvalidAppointmentStateMessage = "Invalid appointment state.";
        public const string InvalidPrescriptionsMessage = "One or more prescriptions is not valid.";
        public const string InvalidAnamnesisMessage = "Anamnesis is not valid";

        public IEnumerable<ExaminationPrescription> Prescriptions { get;  set; }

        private void AddSymptoms(IEnumerable<Symptom> symptoms)
        {
            Symptoms = symptoms;
            
        }
        private void AddPrescriptions(IEnumerable<ExaminationPrescription> prescriptions)
        {
            Prescriptions = prescriptions;
        }
        public Examination(Appointment appointment, string anamnesis,IEnumerable<Symptom> symptoms,IEnumerable<ExaminationPrescription> prescriptions)
        {
            Appointment = appointment;
            AddSymptoms(symptoms);
            AddPrescriptions(prescriptions);
            Anamnesis = anamnesis;
        }

        public void ValidateExamination()
        {
            ValidateAppointment();
            ValidatePrescriptions();
            ValidateAnamnesis();
        }
        private  void ValidateAppointment()
        {
            // if (!Appointment.CanBeExamined())
            // {
            //     throw new ExaminationInvalidDate(InvalidDateMessage);
            // }

            if (Appointment.AppointmentState != AppointmentState.Pending)
            {
                throw new AppointmentExaminationInvalidState(InvalidAppointmentStateMessage);
            }
        }

        private void ValidatePrescriptions()
        {
            if (!Prescriptions.All(prescription => prescription.Validate()))
            {
                throw new ExaminationPrescriptionException(InvalidPrescriptionsMessage);
            }
        }

        private void ValidateAnamnesis()
        {
            if (string.IsNullOrEmpty(Anamnesis))
            {
                throw new ExaminationInvalidAnamnesis(InvalidAnamnesisMessage);
            }
        }
        public Guid IdApp { get; private set; }
        public Examination() : base()
        {
        }

        public override void Apply(DomainEvent<EventStoreExaminationType> @event)
        {
            Changes.Add(@event);
        }
    }
}