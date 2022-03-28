using DataAccess;
using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories
{
    public class StudentRepository :Repository<Student,int,SMDbContext>,IStudentRepository
    {
        public StudentRepository(SMDbContext sMDbContext):base(sMDbContext)
        {

        }
    }
}
