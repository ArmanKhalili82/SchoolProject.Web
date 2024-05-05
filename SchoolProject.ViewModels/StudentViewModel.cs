using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateOfJoin { get; set; } = DateTime.Now;
        public bool Selected { get; set; }
        public string KeyId { get; set; }
        public bool SelectAll { get; set; }

        public StudentViewModel()
        {
            
        }

        public StudentViewModel(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            DOB = student.DOB;
            DateOfJoin = student.DateOfJoin;
            KeyId = student.KeyId;
        }
    }
}
