using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Entities
{
   public class Student:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<StudentRegistration> StudentRegistrations { get; set; }
    }
}
