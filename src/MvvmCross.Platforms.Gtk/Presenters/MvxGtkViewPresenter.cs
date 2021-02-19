using Gtk;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using MvvmCross.Platforms.Gtk.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvvmCross.Platforms.Gtk.Presenters
{
    public class MvxGtkViewPresenter : MvxAttributeViewPresenter, IMvxGtkViewPresenter
    {
        private readonly Application _application;
        private readonly Dictionary<IMvxViewModel, Widget> _views = new Dictionary<IMvxViewModel, Widget>();

        public MvxGtkViewPresenter(Application application)
        {
            _application = application;
        }

        private IMvxGtkViewLoader _viewLoader;
        private IMvxGtkViewLoader ViewLoader => _viewLoader ??= Mvx.IoCProvider.Resolve<IMvxGtkViewLoader>();

        public Window MainWindow { get; set; }

        public override MvxBasePresentationAttribute CreatePresentationAttribute(Type viewModelType, Type viewType)
        {
            if (typeof(Window).IsAssignableFrom(viewType))
            {
                return new MvxWindowPresentationAttribute();
            }

            throw new NotImplementedException();
        }

        public override void RegisterAttributeTypes()
        {
            AttributeTypesToActionsDictionary.Register<MvxWindowPresentationAttribute>(
                (viewType, attribute, request) =>
                {
                    var view = ViewLoader.CreateWindow(viewType, request);
                    _views.Add(view.viewModel, view.window);
                    return ShowWindow(view.window, attribute, request);
                },
                CloseWindow
            );
        }

        private Task<bool> ShowWindow(Window view, MvxWindowPresentationAttribute attribute, MvxViewModelRequest request)
        {
            Window window = view switch
            {
                IMvxGtkWindow mvxWindow => (Window) mvxWindow,
                Window normalWindow => normalWindow,
                null => throw new ArgumentNullException(nameof(view))
            };

            if (!(window is ApplicationWindow))
            {
                window.Application = _application;
            }

            if (MainWindow is null)
            {
                MainWindow = window;
            }

            window.ShowAll();

            return Task.FromResult(true);
        }

        private Task<bool> CloseWindow(IMvxViewModel viewModel, MvxWindowPresentationAttribute attribute)
        {
            if (_views.Remove(viewModel, out var widget) && widget is Window window)
            {
                window.Close();
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
