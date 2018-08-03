using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.CustomFilters;
using Model.Auth;
using Model.Helper;

namespace Model.Domain
{
    public class Course : AuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public Enums.Status Status { get; set; }

        public decimal Vote { get; set; }

        public bool Deleted { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<LessonsPerCourse> Lessons { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        public virtual ICollection<UsersPerCourse> Users { get; set; }

        public virtual ICollection<ReviewsPerCourse> Reviews { get; set; }
    }
}