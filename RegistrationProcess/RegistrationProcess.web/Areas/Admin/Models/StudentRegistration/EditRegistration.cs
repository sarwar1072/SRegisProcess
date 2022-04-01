using Framework.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.StudentRegistration
{
    public class EditRegistration: StudentRegistrationModel
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public DateTime EnrollDate { get; set; }

        public bool IspaymentComplete { get; set; }
        public EditRegistration() : base() { }
        public EditRegistration(IStudentRegistrationService studentRegistrationService)
                                : base(studentRegistrationService) { }
        
        public void Edit()
        {
            var edit = new Framework.Entities.StudentRegistration() { 
                    Id=Id,
                    CourseId=CourseId,
                    StudentId=StudentId,
                    EnrollDate=EnrollDate,
                    IspaymentComplete=IspaymentComplete
            };
            _studentRegistrationService.EditRegistration(edit);
        }
        
        internal void Load(int id)
        {
            var count = _studentRegistrationService.GetRegistration(id);
            if (count != null)
            {
                Id = count.Id;
                CourseId = count.CourseId;
                StudentId = count.StudentId;
                IspaymentComplete = count.IspaymentComplete;
                EnrollDate = count.EnrollDate;
            }
        }       
    }
}
