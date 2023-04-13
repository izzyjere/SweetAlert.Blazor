namespace SweetAlert.Blazor
{
    public class DialogOptions
    {
        public Severity Severity { get; set; } 
        public bool AllowOutSideClick { get; set; } = true;
        public bool ShowCancelButton { get; set; }
        public bool ShowConfirmButton { get; set; }
        public bool ShowCloseButton { get; set; }
        public bool AllowEscapeKey { get; set; } = true;
        public string? ConfirmButtonText { get; set; }
        public string? CancelButtonText { get;  set; }
        public string? Icon { get;  set; }
    }
}
