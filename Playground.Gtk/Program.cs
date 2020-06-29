using Gtk;
using MvvmCross.Platforms.Gtk.Core;
using System;

namespace Playground.Gtk
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            Application.Init();

            var app = new MvxGtkApplication<Setup, Core.App>("dev.kipters.mvxgtk", GLib.ApplicationFlags.None);

            Environment.ExitCode = app.Run(null, args);
        }
    }
}
