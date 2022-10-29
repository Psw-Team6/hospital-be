using System;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Feedbacks.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly HospitalDbContext _context;

        public FeedbackRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _context.Feedback.ToList();
        }

        public Feedback GetById(Guid id)
        {
            return _context.Feedback.Find(id);
        }

        public void Create(Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
        }

        public void Update(Feedback feedback
            )
        {
            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Feedback feedback)
        {
            _context.Feedback.Remove(feedback);
            _context.SaveChanges();
        }
    }
}
