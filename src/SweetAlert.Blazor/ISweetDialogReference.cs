using Microsoft.AspNetCore.Components;

namespace SweetAlert.Blazor
{
    public interface ISweetDialogReference
    {
        Guid Id { get; }
        RenderFragment RenderFragment { get; }
        RenderFragment? Header { get; }
        RenderFragment? Footer { get; }
        void Close();
        void Close(DialogResult dialogResult);
        bool Dismiss(DialogResult dialogResult);
        void InjectRenderFragment(RenderFragment fragment);
        internal void InjectDialog(object instance);
        Task<DialogResult> Result { get; }
        object Dialog { get; }
        DialogOptions Options { get; }
        internal void SetHeader(RenderFragment? header);
        internal void SetFooter(RenderFragment? footer);
    }
}
