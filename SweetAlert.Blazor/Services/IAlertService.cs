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
        void Close(ISweetAlertDialogReference instance);
        void Close(SweetAlertReference sweetAlertReference, DialogResult dialogResult);
        Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title, DialogOptions options);
        Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title, DialogOptions options, DialogParameters parameters);
        Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title);
        Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, DialogParameters parameters);
        Task<ISweetAlertDialogReference> Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, DialogOptions options);
        Task<bool> ShowConfirm(string title, string message, Severity severity);
        Task ShowAlert(string title, string message, Severity severity);
    }
}
