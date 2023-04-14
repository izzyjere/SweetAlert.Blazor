using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

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
 
        
       private ISweetDialogReference CreateReference()
       {
            return new SweetDialogReference(Guid.NewGuid(),this);
       }

        public void Close(ISweetDialogReference instance)
        {
            Close(instance,DialogResult.Ok(null));
        }

        public void Close(ISweetDialogReference sweetAlertReference, DialogResult dialogResult)
        {
            sweetAlertInterop.NotifyDialogCloseRequested(sweetAlertReference, dialogResult);
        }

        public ISweetDialogReference Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, string title, DialogOptions options)         {
            return Show(contentComponent, title, options,default);
        }

        public ISweetDialogReference Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, string? title, DialogOptions? options, DialogParameters? parameters)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent?.FullName} must be a Blazor Component");
            }
            var dialogReference = CreateReference();
            options ??= new DialogOptions();
            title ??= "";
            var dialogContent = DialogHelperComponent.Wrap(new RenderFragment(builder =>
            {
                var i = 0;
                builder.OpenComponent(i++, contentComponent);
                if(parameters!= null)
                {
                    foreach (var parameter in parameters)
                    {
                        builder.AddAttribute(i++, parameter.Key, parameter.Value);
                    }
                }               
                builder.AddComponentReferenceCapture(i++, inst => { dialogReference.InjectDialog(inst); });
                builder.CloseComponent();
            }));
            var dialogInstance = new RenderFragment(builder =>
            {
                builder.OpenComponent<SweetDialogInstance>(0);
                builder.SetKey(dialogReference.Id);
                builder.AddAttribute(1, nameof(SweetDialogInstance.Options), options);
                builder.AddAttribute(2, nameof(SweetDialogInstance.DialogHeader), title);
                builder.AddAttribute(3, nameof(SweetDialogInstance.Content), dialogContent);
                builder.AddAttribute(4, nameof(SweetDialogInstance.Id), dialogReference.Id);
                builder.CloseComponent();
            });
            dialogReference.Options = options;
            dialogReference.InjectRenderFragment(dialogInstance);
            sweetAlertInterop.NotifyDialogInstanceAdded(dialogReference);           
            return dialogReference;
        }

        public ISweetDialogReference Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, string title)
        {
            return Show(contentComponent, title,default,default);
        }

        public ISweetDialogReference Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, DialogParameters parameters) 
        {
            return Show(contentComponent, default, default, parameters);
        }

        public ISweetDialogReference Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, DialogOptions options) 
        {
            return Show(contentComponent,default, options,default);
        }

        public Task<bool> ShowConfirm(string title, string message, Severity severity, AlertOptions? options = default)
        {
            options ??= new AlertOptions();
            return sweetAlertInterop.Confirm(title,message,severity,options);
        }

        public Task ShowAlert(string title, string message, Severity severity)
        {
            return sweetAlertInterop.Alert(title,message,severity);
        }

        public ISweetDialogReference Show<[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] TComponent>(string title, DialogOptions options) where TComponent : ComponentBase
        {
            return Show(typeof(TComponent),title,options);
        }

        public ISweetDialogReference Show<[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] TComponent>(string title, DialogOptions options, DialogParameters parameters) where TComponent : ComponentBase
        {
            return Show(typeof(TComponent), title,options,parameters);
        }

        public ISweetDialogReference Show<[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] TComponent>(string title) where TComponent : ComponentBase
        {
            return Show(typeof(TComponent), title);
        }

        public ISweetDialogReference Show<[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] TComponent>(DialogParameters parameters) where TComponent : ComponentBase
        {
            return Show(typeof(TComponent),parameters);
        }

        public ISweetDialogReference Show<[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] TComponent>(DialogOptions options) where TComponent : ComponentBase
        {
            return Show(typeof(TComponent),options);
        }
    }
}
