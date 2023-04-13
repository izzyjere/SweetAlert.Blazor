using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor
{
    public partial class SweetAlertProvider 
    {
        [Inject] SweetAlertInterop SweetAlertInterop { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        private Collection<ISweetAlertDialogReference> sweetAlerts = new();
        protected override void OnInitialized()
        {
            Navigation.LocationChanged += LocationChanged;
            SweetAlertInterop.OnDialogInstanceAdded += AddInstance;
            SweetAlertInterop.OnDialogCloseRequested += DismissInstance;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {    
            if(SweetAlertInterop is null)
            {
                throw new ArgumentException("Sweet alert services have not been registered. Add a call to builder.Services.AddSweetAlert() to your Program.cs");
            }
            if (firstRender)
            {
                await SweetAlertInterop.Initialize();
            }
        }
        private void AddInstance(ISweetAlertDialogReference sweetAlert)
        {
            sweetAlerts.Add(sweetAlert);
            StateHasChanged();
        }
        public void DismissAll()
        {
            sweetAlerts.ToList().ForEach(r=>DismissInstance(r,DialogResult.Cancel()));
            StateHasChanged() ;
        }
        private void LocationChanged(object sender, LocationChangedEventArgs args)
        {
            DismissAll();
        }
        private void DismissInstance(ISweetAlertDialogReference sweetAlert, DialogResult result)
        {
            if (!sweetAlert.Dismiss(result)) return;
            sweetAlerts.Remove(sweetAlert);
            StateHasChanged();
        }
    }
}
