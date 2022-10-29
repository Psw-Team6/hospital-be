using System;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Common;

namespace HospitalLibrary.Feedbacks.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
