using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace SweetAlert.Blazor.Services
{
    public interface IAlertService
    {
        void Close(ISweetDialogReference instance);
        void Close(ISweetDialogReference sweetAlertReference, DialogResult dialogResult);

        ISweetDialogReference Show<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>( string title, DialogOptions options) where TComponent : ComponentBase;
        ISweetDialogReference Show<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>( string title, DialogOptions options, DialogParameters parameters) where TComponent : ComponentBase;
        ISweetDialogReference Show<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(string title) where TComponent : ComponentBase;
        ISweetDialogReference Show<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(DialogParameters parameters) where TComponent : ComponentBase;
        ISweetDialogReference Show<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(DialogOptions options) where TComponent : ComponentBase;
        ISweetDialogReference Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title, DialogOptions options);
        ISweetDialogReference Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title, DialogOptions options, DialogParameters parameters);
        ISweetDialogReference Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, string title);
        ISweetDialogReference Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, DialogParameters parameters);
        ISweetDialogReference Show([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type contentComponent, DialogOptions options);
        Task<bool> ShowConfirm(string title, string message, Severity severity, AlertOptions? options = default);
        Task ShowAlert(string title, string message, Severity severity);
    }
}
