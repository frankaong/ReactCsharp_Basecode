using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await _feedbackRepository.GetAllAsync();
        }

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _feedbackRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Feedback feedback)
        {
            if (feedback == null)
                throw new ArgumentNullException(nameof(feedback));

            feedback.Title ??= "";
            feedback.Content ??= "";

            await _feedbackRepository.AddAsync(feedback);
        }

        public async Task DeleteAsync(int id)
        {
            await _feedbackRepository.DeleteAsync(id);
        }

        public async Task<double> GetAverageRatingAsync()
        {
            return await _feedbackRepository.GetAverageRatingAsync();
        }
    }
}