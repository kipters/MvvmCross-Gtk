using System;

namespace Playground.Gtk
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new GtkApp();
            Environment.ExitCode = app.Run(null, args);
        }
    }
}
