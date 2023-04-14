using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor
{
    public partial class SweetAlertInstance
    {
        internal static object Content;

        public static object Options { get; internal set; }
        public static object Title { get; internal set; }
        public static object Id { get; internal set; }
    }
}
