using Microsoft.Extensions.DependencyInjection;
using SweetAlert.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
