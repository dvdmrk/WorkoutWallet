using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutWallet.Models
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
