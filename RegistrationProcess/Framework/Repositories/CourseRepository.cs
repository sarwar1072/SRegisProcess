using DataAccess;
using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories
{
   public class CourseRepository:Repository<Course,int,SMDbContext>,ICourseRepository
    {
        public CourseRepository(SMDbContext sMDbContext):base(sMDbContext)
        {

        }
    }
}
