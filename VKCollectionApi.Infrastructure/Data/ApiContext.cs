using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VKCollectionApi.Infrastructure.Data.Models;

namespace VKCollectionApi.Infrastructure.Data
{
	public class ApiContext : IdentityDbContext<Client>
	{
		public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

		public DbSet<Product> Products { get; init; }

		public DbSet<ProductCategories> ProductCategories { get; init; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

	}
}
