using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Entities
{
   public class StudentRegistration:IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        //[ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public int CourseId { get; set; }
        //[ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public DateTime EnrollDate { get; set; }
        public bool IspaymentComplete { get; set; }
    }
}
