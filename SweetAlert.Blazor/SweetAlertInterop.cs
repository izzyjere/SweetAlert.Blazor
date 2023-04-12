using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;

namespace SweetAlert.Blazor
{
  

    internal class SweetAlertInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        public async ValueTask Initialize()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("loadSweetAlert");
        }
        public SweetAlertInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SweetAlert.Blazor/SweetAlert.Blazor.js").AsTask());
        }

        public async ValueTask<string> Prompt(string message)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("showPrompt", message);
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
            var module = await moduleTask.Value;

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

            var html = await module.InvokeAsync<string>("DOMPurify.sanitize", writer.ToString());

            return html;
        }

        public async Task Swal(RenderFragment componentToRender, string title ,AlertOptions options)
        {
            var module = await moduleTask.Value;
            var swalOptions = new
            {
                title,
                html = await ConvertBlazorComponentToHtml(componentToRender),
                icon = options.Icon,
                showCancelButton = options.ShowCancelButton,
                showCloseButton = options.ShowCloseButton,
                showConfirmButton = options.ShowConfirmButton,
                confirmButtonText = options.ConfirmButtonText,
                cancelButtonText = options.CancelButtonText,
                allowOutsideClick = options.AllowOutSideClick,
                allowEscapeKey = options.AllowEscapeKey
            };
            await module.InvokeAsync<object>("showAlertComplex", swalOptions);
        }
    }
}