using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor
{
    public class DialogResult
    {
        public object? Data { get; init; }
        public bool Cancelled { get; init; }
        private DialogResult(object? data, bool cancel)
        {
            Data = data;
            Cancelled = cancel;
        }
        internal static DialogResult Cancel() => new(default, true);
        internal static DialogResult Ok() => new(default, false);
        internal static DialogResult Ok(object data)=> new(data, false);
        
    }
}
