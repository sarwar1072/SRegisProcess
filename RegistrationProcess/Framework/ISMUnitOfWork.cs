
using DataAccess;
using Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public interface ISMUnitOfWork:IUnitOfWork
    {
        IStudentRepository StudentRepository { get; set; }
        ICourseRepository CourseRepository { get; set; }
        IStudentRegistrationRepository StudentRegistrationRepository { get; set; }
        
    }
}
