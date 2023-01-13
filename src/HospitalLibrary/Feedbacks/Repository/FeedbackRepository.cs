using System;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;

namespace HospitalLibrary.Feedbacks.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            
        }
        
        public async Task<IEnumerable<Feedback>> GetAllFeedback()
        {
            return await DbSet.Include(p => p.Root)
                .ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetAllPublic()
        {
            return await DbSet.Include(p => p.Root).Where(feedback => feedback.IsPublic && feedback.Status == Status.APPROVED)
                .ToListAsync();
        }

       
    }
}
