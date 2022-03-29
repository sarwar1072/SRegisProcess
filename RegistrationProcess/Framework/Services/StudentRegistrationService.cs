using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
   public class StudentRegistrationService : IStudentRegistrationService
    {
        private ISMUnitOfWork _sMUnitOfWork;
        public StudentRegistrationService(ISMUnitOfWork sMUnitOfWork)
        {
            _sMUnitOfWork = sMUnitOfWork;
        }

        public void AddRegistration(StudentRegistration studentRegistration)
        {
            _sMUnitOfWork.StudentRegistrationRepository.Add(studentRegistration);
            _sMUnitOfWork.Save();

        }

        public StudentRegistration DeleteRegistration(int Id)
        {
            var rgname = _sMUnitOfWork.StudentRegistrationRepository.GetById(Id);
            _sMUnitOfWork.StudentRegistrationRepository.Remove(rgname);
            _sMUnitOfWork.Save();
            return rgname;
        }

        public void Dispose()
        {
            _sMUnitOfWork.Dispose();
        }

        public void EditRegistration(StudentRegistration studentRegistration)
        {
            var editregis = _sMUnitOfWork.StudentRegistrationRepository.GetById(studentRegistration.Id);
            editregis.StudentId = studentRegistration.StudentId;
            editregis.CourseId = studentRegistration.CourseId;
            editregis.EnrollDate = studentRegistration.EnrollDate;
            editregis.IspaymentComplete = studentRegistration.IspaymentComplete;
            _sMUnitOfWork.Save();
        }

        public StudentRegistration GetRegistration(int Id)
        {
            return _sMUnitOfWork.StudentRegistrationRepository.GetById(Id);
        }

        public (IList<StudentRegistration> studentRegistrations, int total, int totalDisplay) GetStudentRegistrations(int pageindex, int Pagesize, string searchText, string orderBy)
        {
            var result = _sMUnitOfWork.StudentRegistrationRepository.GetDynamic(null, orderBy, "Course,Student", pageindex, Pagesize, true);
            return (result.data, result.total, result.totalDisplay);
        }

        public IList<Student> GetStudents()
        {
            return _sMUnitOfWork.StudentRepository.GetAll();
        }

        public IList<Course> GetCourses()
        {
            return _sMUnitOfWork.CourseRepository.GetAll();
        }

        public int Count()
        {
            return _sMUnitOfWork.StudentRegistrationRepository.GetCount();
        }
    }
}
