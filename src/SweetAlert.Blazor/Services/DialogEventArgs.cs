namespace SweetAlert.Blazor
{
    internal class DialogEventArgs : EventArgs
    {
          public DialogResult? DialogResult { get; set; }
          public ISweetDialogReference SweetAlert { get; set; }
    }
}