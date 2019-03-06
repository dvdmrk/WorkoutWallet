using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.ViewModels
{
    public class ProfileViewModel : BaseEntity
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public int HeightInInches => Feet * 12 + Inches;
        public int Inches { get; set; }
        public int Feet { get; set; }
        [Display(Name = "Height")]
        public string HeightDisplay => Math.Floor(Height/12) + "ft " + Height % 12 + "inches";
        public double Height { get; set; }
        public double Weight { get; set; }
        [Display(Name = "Instagram Profile URL")]
        public string InstagramUrl { get; set; }
        [Display(Name="Routine Profiles")]
        public List<DropdownViewModel> RoutineProfiles { get; set; }
        public List<DropdownViewModel> Workouts { get; set; }
    }
}
