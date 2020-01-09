using University.Data;
using University.Repository;
using University.Repository.Interface;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service
{
   public class FeedbackService: IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public bool SaveFeedback(ProductFeedback productFeedback)
        {
            return _feedbackRepository.SaveFeedback(productFeedback);
        }
        public bool SaveGeneralFeedback(GeneralFeedback generalFeedback)
        {
            return _feedbackRepository.SaveGeneralFeedback(generalFeedback);
        }
        public List<GeneralFeedback> GetGeneralFeedback()
        {
            return _feedbackRepository.GetGeneralFeedback();
        }
    }
}
