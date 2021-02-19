namespace MvvmCross.Platforms.Gtk.Core
{
    public interface IMvxGtkApplication
    {
        void OnAppActivated();
        void RegisterSetup();
    }
}
