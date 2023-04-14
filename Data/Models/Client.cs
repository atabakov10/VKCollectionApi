using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VKCollectionApi.Data.Models
{
    public class Client :IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        
    }
}
