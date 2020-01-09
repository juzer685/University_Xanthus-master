using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository
{
    public class FeedbackRepository: IFeedbackRepository
    {
        public bool SaveFeedback(ProductFeedback productFeedback)
        {
            using (var context = new UniversityEntities())
            {
                context.ProductFeedback.Add(productFeedback);
                context.SaveChanges();
                return true;
            }
        }

        public bool SaveGeneralFeedback(GeneralFeedback generalFeedback)
        {
            using (var context = new UniversityEntities())
            {
                context.GeneralFeedback.Add(generalFeedback);
                context.SaveChanges();
                return true;
            }
        }

        public List<GeneralFeedback> GetGeneralFeedback()
        {
            using (var context = new UniversityEntities())
            {
                var generalFeedback = context.GeneralFeedback.Where(y => y.IsDeleted != true).ToList();
                return generalFeedback;
            }
        }
    }
}
