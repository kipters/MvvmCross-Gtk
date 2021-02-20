using System;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using MvvmCross.ViewModels;
using GtkWidget = Gtk.Widget;

namespace MvvmCross.Platforms.Gtk.Views
{
    public interface IMvxGtkViewLoader
    {
        GtkWidget CreateView(MvxViewModelRequest request);
        GtkWidget CreateView(Type viewType);
        GtkWidget CreateGladeView(MvxViewModelRequest request, IMvxGladeProperties gladeProperties);
    }
}
