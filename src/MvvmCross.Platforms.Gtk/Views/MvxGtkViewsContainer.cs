using System;
using Gtk;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MvvmCross.Platforms.Gtk.Views
{
    public class MvxGtkViewsContainer : MvxViewsContainer, IMvxGtkViewsContainer
    {
        private IMvxViewModelLoader _viewModelLoader;
        private IMvxViewModelLoader ViewModelLoader => _viewModelLoader ??= Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();

        public (Window window, IMvxViewModel viewModel) CreateWindow(Type viewType, MvxViewModelRequest request)
        {
            if (!typeof(Window).IsAssignableFrom(viewType))
            {
                throw new ArgumentException($"Type {viewType.Name} is not a Window");
            }

            var view = (IMvxGtkView) Mvx.IoCProvider.IoCConstruct(viewType);
            var widget = (Window)view;
            view.ViewModel = request is MvxViewModelInstanceRequest instance
                ? instance.ViewModelInstance
                : ViewModelLoader.LoadViewModel(request, null);

            return (widget, view.ViewModel);
        }
    }
}
