﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Common
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
