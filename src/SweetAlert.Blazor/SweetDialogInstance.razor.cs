using Microsoft.AspNetCore.Components;

namespace SweetAlert.Blazor
{
    public partial class SweetDialogInstance
    {
        [Parameter]
        public DialogOptions Options { get; set; }

        [Parameter]
        public RenderFragment DialogHeader { get; set; } 
        [Parameter]
        public RenderFragment DialogFooter { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        private string elementId = "sweet-dialog-instance" + Guid.NewGuid().ToString().Substring(0, 8);
        [CascadingParameter]
        private SweetDialogProvider Parent { get; set; }

        public void Register(SweetDialog dialog)
        {
            if (dialog == null)
            {
                return;
            }

            DialogHeader = dialog.DialogHeader;
            StateHasChanged();
        }

        public new void StateHasChanged() => base.StateHasChanged();
        public void CancelAll()
        {
            Parent?.DismissAll();
        }

        public void Close(DialogResult result)
        {
            Parent?.DismissInstance(Id, result);
        }

        public void Close()
        {
            Close(DialogResult.Ok(null));
        }

        public void Close(object data)
        {
            Close(DialogResult.Ok(data));
        }

        public void Cancel()
        {
            Close(DialogResult.Cancel());
        }
    }
}