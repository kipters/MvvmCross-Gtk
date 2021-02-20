using MvvmCross.Presenters.Attributes;

namespace MvvmCross.Platforms.Gtk.Presenters.Attributes
{
    public class MvxWindowPresentationAttribute : MvxBasePresentationAttribute, IMvxGladeProperties
    {
        public string? ResourceName { get; set; }
        public string? FileName { get; set; }
        public string? ObjectId { get; set; }
        public bool AutoConnectSignals { get; set; } = true;
    }
}
