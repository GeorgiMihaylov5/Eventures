using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Models
{
    public class UserListingViewModel : IdentityUser
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FistName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
