using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
   public class CourseService: ICourseService
    {
        private ISMUnitOfWork _sMUnitOfWork;

        public CourseService(ISMUnitOfWork sMUnitOfWork)
        {
            _sMUnitOfWork = sMUnitOfWork;
        }

        public void AddCourse(Course course)
        {
            var count = _sMUnitOfWork.CourseRepository.GetCount(c => c.Title == course.Title);

            if (count > 0)
                throw new DuplicationException("Course name already exist", nameof(course.Title));

            _sMUnitOfWork.CourseRepository.Add(course);
            _sMUnitOfWork.Save();

        }

        public Course Delete(int Id)
        {
            var course = _sMUnitOfWork.CourseRepository.GetById(Id);
            _sMUnitOfWork.CourseRepository.Remove(Id);
            _sMUnitOfWork.Save();
            return course;
        }

        public void Dispose()
        {
            _sMUnitOfWork.Dispose();
        }

        public void EditCourse(Course course)
        {
            var count = _sMUnitOfWork.CourseRepository.GetCount(c => c.Title == course.Title && c.Id != course.Id);
            if (count > 0)
                throw new DuplicationException("Course name exist", nameof(course.Title));

            var editcourse = _sMUnitOfWork.CourseRepository.GetById(course.Id);
            editcourse.Title = course.Title;
            editcourse.Fee = course.Fee;
            editcourse.SeatCount = course.SeatCount;
            _sMUnitOfWork.Save();
        }

        public Course GetCourse(int Id)
        {
            return _sMUnitOfWork.CourseRepository.GetById(Id);
        }

        public (IList<Course> courses, int total, int totalDisplay) GetCourses(int pageindex, int Pagesize,
                                                                         string searchText, string orderBy)
        {
            var result = _sMUnitOfWork.CourseRepository.GetDynamic(null, orderBy, "", pageindex, Pagesize, true);

            return (result.data, result.total, result.totalDisplay);
        }

    }
}
