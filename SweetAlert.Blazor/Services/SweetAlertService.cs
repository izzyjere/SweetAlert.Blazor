using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.RenderTree;

namespace SweetAlert.Blazor.Services
{
    internal class SweetAlertService : IAlertService
    {
        private readonly SweetAlertInterop sweetAlertInterop;
        public SweetAlertService(SweetAlertInterop sweetAlertInterop)
        {
            this.sweetAlertInterop = sweetAlertInterop;
        }

        public async Task Show<TComponent>(string? title, AlertOptions? options) where TComponent : IComponent
        {
            options ??= new AlertOptions();
            RenderFragment componentToRender = builder => builder.OpenComponent<TComponent>(0);
            await sweetAlertInterop.Swal(componentToRender,options);
        }
       

    }
}
