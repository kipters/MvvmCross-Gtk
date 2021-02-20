using GLib;
using MvvmCross.Platforms.Gtk.Core;

namespace Playground.Gtk
{
    internal class GtkApp : MvxGtkApplication<Setup, Core.App>
    {
        public GtkApp() : base("dev.kipters.mvxgtk", GLib.ApplicationFlags.None)
        {
        }
    }
}
