using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
  public  interface IStudentService:IDisposable
   {
        void AddStudent(Student student);
        Student Delete(int Id);
        void EditStudent(Student student);
        Student GetStudent(int Id);
        (IList<Student> students, int total, int totalDisplay) GetStudents(int pageindex, int Pagesize,
                                                                 string searchText, string orderBy);
   }
}
