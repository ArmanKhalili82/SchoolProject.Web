using SchoolProject.Models;
using SchoolProject.Repositories;
using SchoolProject.Utilities;
using SchoolProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Services
{
    public class SessionService: ISessionService
    {
        private IUnitOfWork _unitOfWork;
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(CreateSessionViewModel vm)
        {
            var model = new CreateSessionViewModel().Convert(vm);
            await _unitOfWork.GenericRepository<Session>().AddAsync(model);
            _unitOfWork.Save();
        }

        public PagedResult<SessionViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount = 0;
            List<SessionViewModel> vmList = new List<SessionViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Session>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<Session>().GetAll().ToList().Count();
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception ex) { throw; }
            var result = new PagedResult<SessionViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public IEnumerable<SessionViewModel> GetAll()
        {
            var modelList = _unitOfWork.GenericRepository<Session>().GetAll();
            var vmList = ConvertModelToViewModelList(modelList.ToList());
            return vmList;
        }

        public SessionViewModel GetById(int sessionId)
        {
            return new SessionViewModel();
        }

        private List<SessionViewModel> ConvertModelToViewModelList(List<Session> modelList)
        {
            return modelList.Select(x => new SessionViewModel(x)).ToList();
            //you should add x in parantess in seconde line
        }
    }
}
