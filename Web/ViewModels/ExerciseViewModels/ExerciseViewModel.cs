﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;
using Web.Types;

namespace Web.ViewModels.ExerciseViewModels
{
    public class ExerciseViewModel : BaseNamedEntity
    {
        public Guid RoutineId { get; set; }
        public Guid CreatedById { get; set; }
        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public string VideoUrl { get; set; }
        public List<BaseNamedEntity> MuscleGroups { get; set; }
    }
}
