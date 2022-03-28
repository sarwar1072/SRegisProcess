using DataAccess;
using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories
{
   public class StudentRegistrationRepository:Repository<StudentRegistration,int,SMDbContext>,IStudentRegistrationRepository
    {
        public StudentRegistrationRepository(SMDbContext sMDbContext):base(sMDbContext)
        {

        }
    }
}
