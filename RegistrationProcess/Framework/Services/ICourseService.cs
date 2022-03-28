using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
   public interface ICourseService
    {
        void AddCourse(Course course);
        Course Delete(int Id);
        void EditCourse(Course course);
        Course GetCourse(int Id);
        (IList<Course> courses, int total, int totalDisplay) GetCourses(int pageindex, int Pagesize,
                                                                         string searchText, string orderBy);
    }
}
