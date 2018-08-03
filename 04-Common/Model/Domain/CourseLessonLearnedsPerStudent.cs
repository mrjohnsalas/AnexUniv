using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.CustomFilters;
using Model.Auth;
using Model.Helper;

namespace Model.Domain
{
    public class CourseLessonLearnedsPerStudent : AuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public int LessonId { get; set; }

        public virtual LessonsPerCourse Lesson { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
