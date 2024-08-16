using Domain.Interfaces;
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

            services.AddMediatR(typeof(IApplicationDbContext).GetTypeInfo().Assembly);
        }
    }
}
