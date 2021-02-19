using Gtk;
using MvvmCross.Presenters;

namespace MvvmCross.Platforms.Gtk.Presenters
{
    public interface IMvxGtkViewPresenter : IMvxViewPresenter
    {
        Window MainWindow { get; }
    }
}
