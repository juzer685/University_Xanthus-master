using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service.Interface
{
    public interface IFeedbackService
    {
        bool SaveFeedback(ProductFeedback productFeedback);

        bool SaveGeneralFeedback(GeneralFeedback generalFeedback);
        List<GeneralFeedback> GetGeneralFeedback();
    }
}
