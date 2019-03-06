using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public User CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public User UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
