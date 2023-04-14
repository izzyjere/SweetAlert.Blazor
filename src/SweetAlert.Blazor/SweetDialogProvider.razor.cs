using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.ObjectModel;

namespace SweetAlert.Blazor
{
    public partial class SweetDialogProvider 
    {
        [Inject] SweetAlertInterop SweetAlertInterop { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        private Collection<ISweetDialogReference> sweetDialogs = new();
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
        private void AddInstance(ISweetDialogReference sweetDialog)
        {
            sweetDialogs.Add(sweetDialog);
            StateHasChanged();
        }
        public void DismissAll()
        {
            sweetDialogs.ToList().ForEach(r=>DismissInstance(r,DialogResult.Cancel()));
            StateHasChanged() ;
        }
        private void LocationChanged(object sender, LocationChangedEventArgs args)
        {
            DismissAll();
        }
        public void DismissInstance(ISweetDialogReference sweetDialog, DialogResult result)
        {
            if (!sweetDialog.Dismiss(result)) return;
            sweetDialogs.Remove(sweetDialog);
            StateHasChanged();
        }
        public void DismissInstance(Guid id, DialogResult result)
        {
            var sweetDialog = sweetDialogs.FirstOrDefault(r => r.Id == id);
            if(sweetDialog is null)
            {
                return;
            }
            if (!sweetDialog.Dismiss(result)) return;
            sweetDialogs.Remove(sweetDialog);
            StateHasChanged();
        }
    }
}
