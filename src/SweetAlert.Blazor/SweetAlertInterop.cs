using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;

namespace SweetAlert.Blazor
{
  

    internal class SweetAlertInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly IJSRuntime javaScript;

        public event Action<ISweetDialogReference> OnDialogInstanceAdded;
        public event Action<ISweetDialogReference, DialogResult> OnDialogCloseRequested;
        public SweetAlertInterop(IJSRuntime jSRuntime)
        {
            javaScript = jSRuntime;
            moduleTask = new(() => javaScript.InvokeAsync<IJSObjectReference>(
             "import", "./_content/SweetAlert.Blazor/SweetAlert.Blazor.js").AsTask());
        }
        public async ValueTask Initialize()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("loadSweetAlert");
            await module.InvokeVoidAsync("loadPurify");
        }
       

        

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
        private async Task<string> ConvertBlazorComponentToHtml(RenderFragment component)
        {
       
            var builder = new RenderTreeBuilder();
            component(builder);
            var frames = builder.GetFrames().Array;

            var writer = new StringWriter();
            foreach (var frame in frames)
            {
                if (frame.FrameType == RenderTreeFrameType.Markup)
                {
                    writer.Write(frame.MarkupContent);
                }
                else if (frame.FrameType == RenderTreeFrameType.Text)
                {
                    writer.Write(frame.TextContent);
                }
            }

            var html = await javaScript.InvokeAsync<string>("DOMPurify.sanitize", writer.ToString());

            return html;
        }       
        private async Task<bool> Dialog(RenderFragment content ,DialogOptions options, RenderFragment? header, RenderFragment? footer, string? title)
        { 

            var module = await moduleTask.Value;

            var swalOptions = new
            {
                title = title??"",
                header = header==null?"":await ConvertBlazorComponentToHtml(header),
                footer = footer==null?"":await ConvertBlazorComponentToHtml(footer),
                html = await ConvertBlazorComponentToHtml(content),
                icon = options.Icon,
                showCancelButton = options.ShowCancelButton,
                showCloseButton = options.ShowCloseButton,
                showConfirmButton = options.ShowConfirmButton,
                confirmButtonText = options.ConfirmButtonText,
                cancelButtonText = options.CancelButtonText,
                allowOutsideClick = options.AllowOutSideClick,
                allowEscapeKey = options.AllowEscapeKey
            };
           return await module.InvokeAsync<bool>("showAlertComplex", swalOptions);
        }

        internal async void NotifyDialogInstanceAdded(ISweetDialogReference alertReference,string? title)
        {
            //TODO: Opening Dialog implementation here.
            await Dialog(alertReference.RenderFragment, alertReference.Options, alertReference.Header, alertReference.Footer, title);
            OnDialogInstanceAdded?.Invoke(alertReference);
        }

        internal async Task<bool> Confirm(string title, string message, Severity severity, AlertOptions options)
        {
            var module = await moduleTask.Value;            
            return await module.InvokeAsync<bool>("showConfirm", title, message, severity.ToString().ToLower(),options.ConfirmButtonText,options.CancelButtonText,options.ConfirmButtonClass,options.CancelButtonClass, options.DangerMode);   
        }

        internal async Task Alert(string title, string message, Severity severity)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("showAlert", title, message, severity.ToString().ToLower());
        }

        internal void NotifyDialogCloseRequested(ISweetDialogReference sweetAlertReference, DialogResult dialogResult)
        {
            //TODO: Closing Dialog implementation here.
            OnDialogCloseRequested?.Invoke(sweetAlertReference, dialogResult);
        }
    }
}