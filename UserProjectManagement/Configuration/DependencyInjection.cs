using Domain.Interfaces;
using Infrastructure.Persistence;
using MediatR;
using System.Reflection;
using UserProjectManagement.Services;

namespace UserProjectManagement.Configuration
{
    public static class DependencyInjection
    {
        public static void AddPresentationLayerServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddMediatR(typeof(IApplicationDbContext).GetTypeInfo().Assembly);

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        }
    }
}
