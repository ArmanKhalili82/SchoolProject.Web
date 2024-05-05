using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels;

public class GradeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GradeViewModel()
    {
        
    }
    public GradeViewModel(Grade grade)
    {
        Id = grade.Id;
        Name = grade.Name;
    }
    //private List<SessionViewModel> ConvertModelToViewModelList(List<YearlySession> modelList)
    //{
    //    return modelList.Select(x => new SessionViewModel(x)).ToList();
    //}

    public Grade Convert(GradeViewModel grade)
    {
        return new Grade { Id = grade.Id, Name = grade.Name };
    }
}