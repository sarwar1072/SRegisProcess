using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
   public interface IStudentRegistrationService
    {
        (IList<StudentRegistration> studentRegistrations, int total, int totalDisplay) GetStudentRegistrations(int pageindex,
                                                                          int Pagesize,
                                                                          string searchText,
                                                                          string ordeBy);
        void AddRegistration(StudentRegistration studentRegistration);
        void EditRegistration(StudentRegistration studentRegistration);
        StudentRegistration GetRegistration(int Id);
        StudentRegistration DeleteRegistration(int Id);
        IList<Student> GetStudents();
        IList<Course> GetCourses();
        int Count();
    }
}
