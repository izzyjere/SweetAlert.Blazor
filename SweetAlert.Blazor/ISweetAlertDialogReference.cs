using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor
{
    public interface ISweetAlertDialogReference
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
    }
}
