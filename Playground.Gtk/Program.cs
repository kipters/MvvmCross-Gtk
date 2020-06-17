using Gdk;
using Gtk;
using Window = Gtk.Window;
using WindowType = Gtk.WindowType;

namespace Playground.Gtk
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();

            var window = new Window(WindowType.Toplevel)
            {
                DefaultSize = new Size(300, 200),
                WindowPosition = WindowPosition.Center
            };

            window.Destroyed += (s, e) => Application.Quit();

            var label = new Label("Hello World!")
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            window.Add(label);
            window.ShowAll();

            Application.Run();
        }
    }
}
