using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain
{
    public class Category : AuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Icon { get; set; }

        public bool Deleted { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}