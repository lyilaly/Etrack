using System.Collections.Generic;

namespace Etrack.Core.Model
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
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
