using Microsoft.AspNetCore.Components;
using SweetAlert.Blazor.Services;

namespace SweetAlert.Blazor
{
    public class SweetDialogReference : ISweetDialogReference
    {
        public Guid Id { get; }

        public RenderFragment RenderFragment { get; private set; }
        private readonly TaskCompletionSource<DialogResult> _resultCompletion = new();
        private readonly IAlertService alertService;
        internal SweetDialogReference(Guid id, IAlertService alertService, DialogOptions options, Type type)
        {
            Id = id;
            this.alertService = alertService;              
            Options = options;
            Type = type;
        }

         public Task<DialogResult> Result { get=> _resultCompletion.Task; }

        public void Close()
        {
            alertService.Close(this);
        }

        public void Close(DialogResult dialogResult)
        {
            alertService.Close(this, dialogResult);
        }

        public bool Dismiss(DialogResult dialogResult)
        {
            return _resultCompletion.TrySetResult(dialogResult);
        }
        public object Dialog { get; private set; }
        public DialogOptions Options { get; }

        public RenderFragment? Header { get; private set; }

        public RenderFragment? Footer { get; private set; }

        public Type Type { get;  }

        public void InjectDialog(object instance)
        {
            Dialog = instance;
        }

        public void InjectRenderFragment(RenderFragment fragment)
        {
            RenderFragment = fragment;
        }

        public void SetHeader(RenderFragment? header)
        {
            Header = header;
        }

        public void SetFooter(RenderFragment? footer)
        {
            Footer = footer;
        }
    }
}
