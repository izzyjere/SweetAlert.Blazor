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
        /// <summary>
        /// This internal wrapper components prevents overwriting parameters of once
        /// instanciated dialog instances
        /// </summary>
        private class DialogHelperComponent : IComponent
        {
            const string ChildContent = nameof(ChildContent);
            RenderFragment _renderFragment;
            RenderHandle _renderHandle;
            void IComponent.Attach(RenderHandle renderHandle) => _renderHandle = renderHandle;
            Task IComponent.SetParametersAsync(ParameterView parameters)
            {
                if (_renderFragment == null)
                {
                    if (parameters.TryGetValue(ChildContent, out _renderFragment))
                    {
                        _renderHandle.Render(_renderFragment);
                    }
                }
                return Task.CompletedTask;
            }
            public static RenderFragment Wrap(RenderFragment renderFragment)
                => new RenderFragment(builder =>
                {
                    builder.OpenComponent<DialogHelperComponent>(1);
                    builder.AddAttribute(2, ChildContent, renderFragment);
                    builder.CloseComponent();
                });
        }

        private readonly SweetAlertInterop sweetAlertInterop;
       
        public SweetAlertService(SweetAlertInterop sweetAlertInterop)
        {
            this.sweetAlertInterop = sweetAlertInterop;
        }

        public async Task<ISweetAlertDialogReference> Show<TComponent>(string? title, DialogOptions? options) where TComponent : IComponent
        {
            var alertReference = CreateReference();
            options ??= new DialogOptions();
            title ??= "";
            RenderFragment componentToRender = builder => builder.OpenComponent<TComponent>(0);
            alertReference.InjectRenderFragment(componentToRender);
            var confirmed = await sweetAlertInterop.Swal(componentToRender,title,options);
            
            return alertReference;
        }
       private ISweetAlertDialogReference CreateReference()
       {
            return new SweetAlertReference(Guid.NewGuid(),this);
       }
       public void Close(ISweetAlertDialogReference instance)
       {
            throw new NotImplementedException();
       }

        public void Close(SweetAlertReference sweetAlertReference, DialogResult dialogResult)
        {
            throw new NotImplementedException();
        }
    }
}
