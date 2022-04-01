using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.StudentRegistration
{
    public class CreatStRegistration:StudentRegitrationBaseModel
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public bool IspaymentComplete { get; set; }
        public DateTime EnrollDate { get; set; }
        public CreatStRegistration() : base() { }
        public CreatStRegistration(IStudentRegistrationService studentRegistrationService)
            : base(studentRegistrationService) { }
        
        public void AddRegistration()
        {
            var add = new Framework.Entities.StudentRegistration() 
            {
                CourseId=CourseId,
                StudentId=StudentId,
                EnrollDate=EnrollDate,
                IspaymentComplete= IspaymentComplete,
            
            };
            _studentRegistrationService.AddRegistration(add);
        }
    }
}
