using System;
using System.Threading.Tasks;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using GtkApp = Gtk.Application;
using GtkWindow = Gtk.Window;
using GtkWidget = Gtk.Widget;
using GtkAppWindow = Gtk.ApplicationWindow;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Platforms.Gtk.Views;
using MvvmCross.Exceptions;

namespace MvvmCross.Platforms.Gtk.Presenters
{
    public class MvxGtkViewPresenter : MvxAttributeViewPresenter, IMvxGtkViewPresenter
    {
        private readonly GtkApp _application;
        private readonly List<GtkWindow> _windows = new();

        public MvxGtkViewPresenter(GtkApp application)
        {
            _application = application;
        }

        private IMvxGtkViewLoader? _viewLoader;
        protected IMvxGtkViewLoader ViewLoader => _viewLoader ??= Mvx.IoCProvider.Resolve<IMvxGtkViewLoader>();

        public GtkWindow? MainWindow => _windows.FirstOrDefault();

        public override MvxBasePresentationAttribute CreatePresentationAttribute(Type viewModelType, Type viewType)
        {
            if (typeof(GtkWindow).IsAssignableFrom(viewType))
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
                    var view = attribute switch
                    {
                        { ResourceName: not null, FileName: not null } => throw new MvxException("ResourceName and FileName are mutually exclusive"),
                        { ResourceName: not null } => ViewLoader.CreateGladeView(request, attribute),
                        { FileName: not null } => ViewLoader.CreateGladeView(request, attribute),
                        _ => ViewLoader.CreateView(request)
                    };
                    return ShowWindow(view, attribute, request);
                },
                CloseWindow
            );
        }

        private Task<bool> CloseWindow(IMvxViewModel viewModel, MvxWindowPresentationAttribute attribute)
        {
            var window = _windows.Cast<IMvxGtkView>().Single(x => x.ViewModel == viewModel);

            if (window is GtkWindow gtkWin)
            {
                _windows.Remove(gtkWin);
                gtkWin.Close();
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        private Task<bool> ShowWindow(GtkWidget view, MvxWindowPresentationAttribute attribute, MvxViewModelRequest request)
        {
            if (view is not GtkWindow window)
            {
                throw new MvxException($"{view.GetType().Name} is not a Gtk.Window");
            }

            if (window is not GtkAppWindow)
            {
                window.Application = _application;
            }

            _windows.Add(window);
            window.ShowAll();
            window.Activate();
            return Task.FromResult(true);
        }

        public override Task<bool> Show(MvxViewModelRequest request)
        {
            return base.Show(request);
        }
    }
}
