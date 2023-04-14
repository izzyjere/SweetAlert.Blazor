using Microsoft.AspNetCore.Components;

namespace SweetAlert.Blazor
{
    public interface ISweetDialogReference
    {
        Guid Id { get; }
        RenderFragment RenderFragment { get; }
        void Close();
        void Close(DialogResult dialogResult);
        bool Dismiss(DialogResult dialogResult);
        void InjectRenderFragment(RenderFragment fragment);
        void InjectDialog(object instance);
        Task<DialogResult> Result { get; }
        object Dialog { get; }
        DialogOptions Options { get; set; }
    }
}
