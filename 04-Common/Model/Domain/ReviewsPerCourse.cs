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
    public class ReviewsPerCourse : AuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        public decimal Vote { get; set; }

        public string Comment { get; set; }

        public bool Deleted { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}