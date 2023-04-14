using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using SweetAlert.Blazor;
using SweetAlert.Blazor.Services;

namespace SweetAlert.Blazor
{
    public partial class SweetDialog
    {
        [Inject]
        public IAlertService AlertService { get; set; }

        [CascadingParameter]
        SweetDialogInstance DialogInstance { get; set; }

        [Parameter]
        public RenderFragment DialogContent { get; set; }

        [Parameter]
        public RenderFragment DialogFooter { get; set; }

        [Parameter]
        public RenderFragment DialogHeader { get; set; }

        [Parameter]
        public DialogOptions Options { get; set; }
        private Guid Id { get; set; }

        private ISweetDialogReference? dialogReference;
        public ISweetDialogReference Show(string? title = null, DialogOptions? options = null)
        {
            if (dialogReference != null)
            {
                Close();
            }

            var parameters = new DialogParameters()
            {
                [nameof(DialogContent)] = DialogContent,
                [nameof(DialogFooter)] = DialogFooter,
                [nameof(DialogHeader)] = DialogHeader
            };
            dialogReference = AlertService.Show<SweetDialog>(title: title, options: Options ?? options, parameters: parameters);
            return dialogReference;
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            DialogInstance?.Register(this);
        }
        public void Close(DialogResult? result = null)
        {
            if (dialogReference == null)
            {
                return;
            }

            dialogReference.Close(result);
            dialogReference = null;
        }
    }
}