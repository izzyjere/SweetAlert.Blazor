﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.RenderTree;
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

        public async Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, string title, DialogOptions options)
        {
            return await Show(contentComponent, title, options,default);
        }

        public async Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, string? title, DialogOptions? options, DialogParameters? parameters)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent?.FullName} must be a Blazor Component");
            }
            var alertReference = CreateReference();
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
                builder.AddComponentReferenceCapture(i++, inst => { alertReference.InjectDialog(inst); });
                builder.CloseComponent();
            }));
            var dialogInstance = new RenderFragment(builder =>
            {
                builder.OpenComponent<SweetAlertInstance>(0);
                builder.SetKey(alertReference.Id);
                builder.AddAttribute(1, nameof(SweetAlertInstance.Options), options);
                builder.AddAttribute(2, nameof(SweetAlertInstance.Title), title);
                builder.AddAttribute(3, nameof(SweetAlertInstance.Content), dialogContent);
                builder.AddAttribute(4, nameof(SweetAlertInstance.Id), alertReference.Id);
                builder.CloseComponent();
            });
            alertReference.InjectRenderFragment(dialogInstance);
            sweetAlertInterop.NotifyDialogInstanceAdded(alertReference);
            var confirmed = await sweetAlertInterop.Dialog(alertReference.RenderFragment, title, options);
            return alertReference;
        }

        public async Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, string title)
        {
            return await Show(contentComponent, title,default,default);
        }

        public async Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, DialogParameters parameters)
        {
            return await Show(contentComponent, default, default, parameters);
        }

        public async Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)(-1))] Type contentComponent, DialogOptions options)
        {
            return await Show(contentComponent,default, options,default);
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
    }
}