﻿using DataAccess;
using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories
{
   public interface IStudentRegistrationRepository:IRepository<StudentRegistration,int,SMDbContext>
    {
    }
}
