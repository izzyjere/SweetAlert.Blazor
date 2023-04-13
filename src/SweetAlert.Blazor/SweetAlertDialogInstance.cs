using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor
{
    internal class SweetAlertDialogInstance : ISweetAlertDialogInstance
    {
        private readonly SweetAlertInterop sweetAlertInterop;

        public SweetAlertDialogInstance(SweetAlertInterop sweetAlertInterop)
        {
            this.sweetAlertInterop = sweetAlertInterop;
        }

        public void Close(object data)
        {

        }
        public void Close()
        {

        }
        public void Cancel()
        {

        }
        public void CloseAll()
        {

        }
    }
}
