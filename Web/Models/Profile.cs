using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public class Profile : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; } 
        public string UserName { get; set; }
        public string InstagramUrl { get; set; }
        public ICollection<RoutineProfile> RoutineProfiles { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
}
