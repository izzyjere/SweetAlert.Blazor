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
        public static DialogResult Cancel() => new(default, true);
        public static DialogResult Ok() => new(default, false);
        public static DialogResult Ok(object? data)=> new(data, false);
        
    }
}
