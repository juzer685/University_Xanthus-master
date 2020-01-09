using University.Data;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.UI.Areas.Admin.Controllers
{
    public class GeneralFeedbackController : Controller
    {
        private IFeedbackService _feedbackService;

        public GeneralFeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        // GET: Admin/ProductFeedback
        public ActionResult Index()
        {
            var model = _feedbackService.GetGeneralFeedback().OrderByDescending(y => y.CreatedDate).ToList();
            var viewModel = AutoMapper.Mapper.Map<List<GeneralFeedback>, List<GeneralFeedbackViewModel>>(model);
            return View(viewModel);
        }
    }
}