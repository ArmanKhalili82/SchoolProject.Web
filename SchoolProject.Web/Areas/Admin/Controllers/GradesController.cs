using Microsoft.AspNetCore.Mvc;
using SchoolProject.Services;
using SchoolProject.Utilities;
using SchoolProject.ViewModels;

namespace SchoolProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GradesController : Controller
    {
        private IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpGet]
        public IActionResult Index(int pageNumber, int pageSize = 10)
        {
            PagedResult<GradeViewModel> grade = _gradeService.GetAll(pageNumber, pageSize);
            return View(grade);
        }

        [HttpGet]
        public IActionResult AddGrade()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(CreateGradeViewModel vm)
        {
            await _gradeService.Add(vm);
            return RedirectToAction("Index");
        }
    }
}
