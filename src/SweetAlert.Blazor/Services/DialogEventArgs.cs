namespace SweetAlert.Blazor
{
    internal class DialogEventArgs : EventArgs
    {
          public DialogResult? DialogResult { get; set; }
          public ISweetAlertDialogReference SweetAlert { get; set; }
    }
}