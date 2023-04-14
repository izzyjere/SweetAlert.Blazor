using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor.Services
{
    public interface IAlertService
    {
        void Close(ISweetDialogReference instance);
        void Close(SweetDialogReference sweetAlertReference, DialogResult dialogResult);
        Task<ISweetDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title, DialogOptions options);
        Task<ISweetDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title, DialogOptions options, DialogParameters parameters);
        Task<ISweetDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title);
        Task<ISweetDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, DialogParameters parameters);
        Task<ISweetDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, DialogOptions options);
        Task<bool> ShowConfirm(string title, string message, Severity severity, AlertOptions? options = default);
        Task ShowAlert(string title, string message, Severity severity);
    }
}
