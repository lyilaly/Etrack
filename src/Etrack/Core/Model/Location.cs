using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Etrack.Core.Model
{
    public class Location
    {
        [Key]
        [StringLength(3, ErrorMessage = "Location id must have 3 characters in lenght or less.")]
        [Display(Name = "Location Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "")]
        [StringLength(25, ErrorMessage = "Location name must have 25 characters in lenght or less.")]
        [Display(Name = "Location Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Location Type")]
        public LocationType? Type { get; set; }

        public virtual ICollection<UserLocation> AssignedUsers { get; set; }
    }

    public enum LocationType
    {
        Office,
        Rig,
        Yard
    }
}
