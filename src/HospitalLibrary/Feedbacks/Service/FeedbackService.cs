using System;
using HospitalLibrary.Feedbacks.Model;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Common;
using System.Threading.Tasks;

namespace HospitalLibrary.Feedbacks.Service
{
    public class FeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Feedback>> GetAll()
        {
            return  await _unitOfWork.FeedbackRepository.GetAllFeedback();
        }

        public async Task<IEnumerable<Feedback>> GetAllPublic()
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetAllPublic();
            return  feedbacks;
        }

        public async Task<Feedback> CreateFeedback(Feedback feedback)
        {
            var newFeedback = await _unitOfWork.FeedbackRepository.CreateAsync(feedback);
            await _unitOfWork.CompleteAsync();
            return newFeedback;
        }

        public async Task<Feedback> GetById(Guid id)
        {
            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return feedback;
        }

        public async Task<bool> Update(Feedback feedback)
        {
            await _unitOfWork.FeedbackRepository.UpdateAsync(feedback);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
