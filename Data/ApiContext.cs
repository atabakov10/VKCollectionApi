using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using VKCollectionApi.Data.Models;

namespace VKCollectionApi.Data
{
    public class ApiContext : IdentityDbContext<Client>
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

       
    }
}
