using DataAccess;
using Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
namespace Framework
{
    public class SMUnitOfWork : UnitOfWork, ISMUnitOfWork
    {
        public IStudentRepository StudentRepository { get; set; }
        public ICourseRepository CourseRepository { get; set; }
        public IStudentRegistrationRepository StudentRegistrationRepository { get; set; }


        public SMUnitOfWork( SMDbContext frameworkContext  , IStudentRepository studentRepository,
                                                             ICourseRepository courseRepository, 
                                                             IStudentRegistrationRepository studentRegistrationRepository)
            :base(frameworkContext)
        {
            StudentRepository = studentRepository;
            CourseRepository = courseRepository;
            StudentRegistrationRepository = studentRegistrationRepository;
         
        }
     }
}
