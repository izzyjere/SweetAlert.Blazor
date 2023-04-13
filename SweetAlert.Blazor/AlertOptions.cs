namespace SweetAlert.Blazor
{
    public class AlertOptions
    {
        public string ConfirmButtonText { get; set; } = "OK";
        public string CancelButtonText { get; set; } = "Cancel";
        public string ConfirmButtonClass { get; set; } = "";
        public string CancelButtonClass { get; set; } = "";
        public bool DangerMode { get; set; } = false;
    }
}