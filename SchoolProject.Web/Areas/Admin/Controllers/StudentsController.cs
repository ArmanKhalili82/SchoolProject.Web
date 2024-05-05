using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProject.Services;
using SchoolProject.Utilities;
using SchoolProject.ViewModels;

namespace SchoolProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        private IStudentService _studentService;
        private IGradeService _gradeService;
        private ISessionService _sessionService;

        public StudentsController(IStudentService studentService, IGradeService gradeService, ISessionService sessionService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
            _sessionService = sessionService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.Grades = new SelectList(_gradeService.GetAll(), "Id", "Name");
            ViewBag.Session = new SelectList(_sessionService.GetAll(), "Id", "Combined");

            var students = _studentService.GetAll(pageNumber, pageSize);
            return View(students);
        }

        [HttpPost]
        public IActionResult EnrollStudent(PagedResult<StudentViewModel> vm, int GradeId, int SessionId, bool SelectAll)
        {
            if (GradeId == 0 || GradeId == -1 || SessionId == 0 || SessionId == -1)
            {
                TempData["error"] = "Please Select Both Grade and Session";
                return RedirectToAction("Index");
            }

            var StudentIdsList = vm.Data.Where(x => x.Selected == true).Select(y => y.Id).ToList();
            GradeViewModel grade = _gradeService.GetById(GradeId);
            var result = _gradeService.AddGradeWithStudent(grade, SessionId, StudentIdsList);
            if (result == 0)
                TempData["error"] = "Sorry Duplicated Are not Added";
            if (result > 0)
                TempData["success"] = $"No of ({result}) records are aded successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(CreateStudentViewModel vm)
        {
            await _studentService.AddStudent(vm);
            return RedirectToAction("Index");
        }
    }
}
