using Microsoft.Extensions.DependencyInjection;
using SweetAlert.Blazor.Services;

namespace SweetAlert.Blazor
{
    public static class Extensions
    {
        public static IServiceCollection AddSweetAlert(this IServiceCollection services)
        {
            services.AddScoped<SweetAlertInterop>();
            services.AddScoped<IAlertService,SweetAlertService>();
            return services;
        }
    }
}
