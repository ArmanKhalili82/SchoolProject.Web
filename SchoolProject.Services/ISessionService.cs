using SchoolProject.Utilities;
using SchoolProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Services
{
    public interface ISessionService
    {
        Task Add(CreateSessionViewModel vm);
        PagedResult<SessionViewModel> GetAll(int pageNumber, int pageSize);
        IEnumerable<SessionViewModel> GetAll();
        SessionViewModel GetById(int gradeId);
    }
}
