namespace Talabat.APIs.Extensions
{
	public static class SwaggerServiceExtension
	{

		public static IServiceCollection AddSwaggerService(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			return services;
		}

		public static WebApplication UseSwaggerMiddleWare(this WebApplication application)
		{
			application.UseSwagger();
			application.UseSwaggerUI();
			return application;
		}
	}
}
