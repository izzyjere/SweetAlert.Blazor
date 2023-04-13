using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor.Services
{
    public interface IAlertService
    {
        void Close(ISweetAlertDialogReference instance);
        void Close(SweetAlertReference sweetAlertReference, DialogResult dialogResult);
        Task<ISweetAlertDialogReference> Show<TComponent>(string title, DialogOptions options) where TComponent : IComponent;
        Task<ISweetAlertDialogReference> Show<TComponent>(string title, DialogOptions options, DialogParameters parameters) where TComponent : IComponent;
        Task<ISweetAlertDialogReference> Show<TComponent>(string title) where TComponent : IComponent;
        Task<ISweetAlertDialogReference> Show<TComponent>(DialogParameters parameters) where TComponent : IComponent;
        Task<ISweetAlertDialogReference> Show<TComponent>(DialogOptions options) where TComponent : IComponent;
        Task<bool> ShowConfirm(string title, string message, Severity severity);
        Task ShowAlert(string title, string message, Severity);
    }
}
