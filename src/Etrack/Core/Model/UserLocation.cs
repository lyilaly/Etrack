namespace Etrack.Core.Model
{
    public class UserLocation
    {
        public string UserId { get; set; }
        public string LocationId { get; set; }
        public virtual User User { get; set; }
        public virtual Location Location { get; set; }
    }
}
