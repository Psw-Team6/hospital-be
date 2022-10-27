using System;
using HospitalLibrary.Feedbacks.Model;
using System.Collections.Generic;


namespace HospitalLibrary.Feedbacks.Repository
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAll();
        Feedback GetById(Guid id);
        void Create(Feedback feedback);
        void Update(Feedback feedback);
        void Delete(Feedback feedback);
    }
}
