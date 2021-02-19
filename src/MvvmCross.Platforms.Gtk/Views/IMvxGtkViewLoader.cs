using System;
using Gtk;
using MvvmCross.ViewModels;

namespace MvvmCross.Platforms.Gtk.Views
{
    public interface IMvxGtkViewLoader
    {
        (Window window, IMvxViewModel viewModel) CreateWindow(Type viewType, MvxViewModelRequest request);
    }
}
