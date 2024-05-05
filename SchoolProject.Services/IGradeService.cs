using SchoolProject.Utilities;
using SchoolProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Services
{
    public interface IGradeService
    {
        Task Add(CreateGradeViewModel vm);
        int AddGradeWithStudent(GradeViewModel grade, int SessionId, List<int> StudentList);
        PagedResult<GradeViewModel> GetAll(int pageNumber, int pageSize);
        IEnumerable<GradeViewModel> GetAll();
        GradeViewModel GetById(int gradeId);
    }
}
