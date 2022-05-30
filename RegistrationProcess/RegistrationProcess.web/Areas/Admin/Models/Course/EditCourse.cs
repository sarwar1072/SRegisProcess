using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.Course
{
    public class EditCourse:CourseBaseModel
    {
        public string Title { get; set; }
        public  int SeatCount { get; set; }
        public int Fee { get; set; }
        public int Id { get; set; }
        public EditCourse() : base() { }     
        public EditCourse(ICourseService courseService) : base(courseService) { }

        public void Edit()
        {
            var edit = new Framework.Entities.Course() 
            { 
                Id=Id,
                SeatCount=SeatCount,
                Fee=Fee,
                Title=Title
            };
            _courseService.EditCourse(edit);
        }

        public void Load(int id)
        {
            var course = _courseService.GetCourse(id);
            if(course != null)
            {
                Id = course.Id;
                Title = course.Title;
                Fee = course.Fee;
                SeatCount = course.SeatCount;
            }
        }
       
    }
}
