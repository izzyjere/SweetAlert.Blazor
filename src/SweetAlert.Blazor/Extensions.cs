using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SweetAlert.Blazor.Services;
using System.Text;

namespace SweetAlert.Blazor
{
    public static class Extensions
    {
        internal static string RenderAsSimpleString(this RenderFragment childContent)
        {
            using RenderTreeBuilder builder = new RenderTreeBuilder();
            builder.AddContent(0, childContent);
            ArrayRange<RenderTreeFrame> array = builder.GetFrames();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Count; i++)
            {
                ref RenderTreeFrame frame = ref array.Array[i];
                if (frame.FrameType == RenderTreeFrameType.Text)
                {
                    sb.Append(frame.TextContent);
                }
                if (frame.FrameType == RenderTreeFrameType.Markup)
                {
                    sb.Append(frame.MarkupContent);
                }
            }
            return sb.ToString();
        }
        public static IServiceCollection AddSweetAlert(this IServiceCollection services)
        {
            services.AddScoped<SweetAlertInterop>();
            services.AddScoped<IAlertService,SweetAlertService>();
            return services;
        }
    }
}
