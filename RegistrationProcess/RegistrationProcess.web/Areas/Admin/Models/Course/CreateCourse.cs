using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.Course
{
    public class CreateCourse:CourseBaseModel
    {
        public string Title { get; set; }
        public int SeatCount { get; set; }
        public int Fee { get; set; }
        public CreateCourse() : base() { }
        public CreateCourse(ICourseService courseService) : base(courseService) { }

        public void Create()
        {
            var course = new Framework.Entities.Course()
            {
                Title=Title,
                SeatCount=SeatCount,
                Fee=Fee
            };
            _courseService.AddCourse(course);

        }
        
    }
}
