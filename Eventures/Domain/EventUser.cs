using Microsoft.AspNetCore.Identity;

namespace Eventures.Domain
{
    public class EventUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
