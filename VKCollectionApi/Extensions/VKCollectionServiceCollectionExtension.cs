using VKCollectionApi.Core.Contracts;
using VKCollectionApi.Core.Services;

namespace VKCollectionApi.Extensions
{
	public static class VKCollectionServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();


			return services;
		}
	}
}
