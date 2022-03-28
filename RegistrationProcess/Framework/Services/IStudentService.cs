using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
  public  interface IStudentService
   {
        void AddStudent(Student student);
        Student Deleteint(int Id);
        void EditStudent(Student student);
        Student GetStudent(int Id);
        (IList<Student> students, int total, int totalDisplay) GetStudents(int pageindex, int Pagesize,
                                                                 string searchText, string orderBy);
   }
}
