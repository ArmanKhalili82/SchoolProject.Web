using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using SchoolProject.Repositories;
using SchoolProject.Utilities;
using SchoolProject.ViewModels;

namespace SchoolProject.Services
{
    public class GradeService: IGradeService
    {
        private IUnitOfWork _unitOfWork;
        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(CreateGradeViewModel vm)
        {
            var model = new CreateGradeViewModel().Convert(vm);
            await _unitOfWork.GenericRepository<Grade>().AddAsync(model);
            _unitOfWork.Save();
        }

        public int AddGradeWithStudent(GradeViewModel grade, int sessionId, List<int> StudentList)
        {
            int Count = 0;
            var model = new GradeViewModel().Convert(grade);
            foreach (var item in StudentList)
            {
                if (!_unitOfWork.GenericRepository<Enroll>()
                    .Exists(x => x.SessionId == sessionId && x.StudentId == item))
                {
                    model.Enrolls.Add(new Enroll()
                    {
                        StudentId = item,
                        GradeId = grade.Id,
                        SessionId = sessionId,
                    });
                    Count ++;
                }
            }
            _unitOfWork.GenericRepository<Grade>().Update(model);
            _unitOfWork.Save();
            return Count;
        }

        public PagedResult<GradeViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount = 0;
            List<GradeViewModel> vmList = new List<GradeViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Grade>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<Grade>().GetAll().ToList().Count();
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception ex) { throw; }
            var result = new PagedResult<GradeViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public IEnumerable<GradeViewModel> GetAll()
        {
            var modelList = _unitOfWork.GenericRepository<Grade>().GetAll();
            var vmList = ConvertModelToViewModelList(modelList.ToList());
            return vmList;
        }

        public GradeViewModel GetById(int gradeId)
        {
            var model = _unitOfWork.GenericRepository<Grade>().GetByIdAsync(x => x.Id == gradeId, include: y => y.Include(a => a.Enrolls));
            var vm = new GradeViewModel(model);
            return vm;
            //you should add model in parantess in seconde line
        }

        private List<GradeViewModel> ConvertModelToViewModelList(List<Grade> modelList)
        {
            return modelList.Select(x => new GradeViewModel(x)).ToList();
            //you should add x in parantess in seconde line
        }
    }
}
