using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
   public class StudentService :IStudentService
    {
        private  ISMUnitOfWork _sMUnitOfWork;
        public StudentService(ISMUnitOfWork sMUnitOfWork)
        {
            _sMUnitOfWork = sMUnitOfWork;
        }

        public void AddStudent(Student student)
        {
            _sMUnitOfWork.StudentRepository.Add(student);
            _sMUnitOfWork.Save();

        }

        public Student Delete(int Id)
        {
            var delete = _sMUnitOfWork.StudentRepository.GetById(Id);
            _sMUnitOfWork.StudentRepository.Remove(delete);
            _sMUnitOfWork.Save();

            return delete;
        }

        public void Dispose()
        {
            _sMUnitOfWork.Dispose();
        }

        public void EditStudent(Student student)
        {
            var std = _sMUnitOfWork.StudentRepository.GetById(student.Id);
            std.Name = student.Name;
            std.DateOfBirth = student.DateOfBirth;
            _sMUnitOfWork.Save();
        }

        public Student GetStudent(int Id)
        {
            return _sMUnitOfWork.StudentRepository.GetById(Id);
        }

        public (IList<Student> students, int total, int totalDisplay) GetStudents(int pageindex, int Pagesize,
            string searchText, string orderBy)
        {
            var result = _sMUnitOfWork.StudentRepository.GetDynamic(null, orderBy, "", pageindex, Pagesize, true);
            return (result.data, result.total, result.totalDisplay);
        }

    }
}
