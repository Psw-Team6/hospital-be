using System;
using HospitalLibrary.Feedbacks.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;

namespace HospitalLibrary.Feedbacks.Repository
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetAllPublic();
    }
}
