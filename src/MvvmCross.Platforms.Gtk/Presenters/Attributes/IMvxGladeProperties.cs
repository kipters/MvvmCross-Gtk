namespace MvvmCross.Platforms.Gtk.Presenters.Attributes
{
    public interface IMvxGladeProperties
    {
        string? ResourceName { get; }
        string? FileName { get; }
        string? ObjectId { get; }
        bool AutoConnectSignals { get; }
    }
}
