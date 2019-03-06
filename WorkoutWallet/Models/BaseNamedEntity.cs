using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutWallet.Models
{
    public class BaseNamedEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
