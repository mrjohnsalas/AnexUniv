﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain
{
    public class Income : AuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        public Enums.EntityType EntityType { get; set; }

        public Enums.IncomeType IncomeType { get; set; }

        public decimal Total { get; set; }

        public int EntityId { get; set; }

        public bool Deleted { get; set; }
    }
}