using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor
{
    public class SweetAlertProvider : ComponentBase
    {
        [Inject] SweetAlertInterop? SweetAlertInterop { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(SweetAlertInterop is null)
            {
                throw new InvalidOperationException("Sweet alert services have not been registered. Add a call to builder.Services.AddSweetAlert() to your Program.cs");
            }
            if (firstRender)
            {
                await SweetAlertInterop.Initialize();
            }
        }
    }
}
