using Gtk;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using MvvmCross.Platforms.Gtk.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace MvvmCross.Platforms.Gtk.Presenters
{
    public class MvxGtkViewPresenter : MvxAttributeViewPresenter, IMvxGtkViewPresenter
    {
        private readonly Application _application;

        public MvxGtkViewPresenter(Application application)
        {
            _application = application;
        }

        private IMvxViewModelLoader _loader;
        private IMvxViewModelLoader ViewModelLoader => _loader ??= Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();

        public Window MainWindow { get; private set; }

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
            AttributeTypesToActionsDictionary.Register<MvxWindowPresentationAttribute>(CreateAndShowWindow, CloseWindow);
        }

        private Task<bool> CloseWindow(IMvxViewModel viewModel, MvxWindowPresentationAttribute attribute)
        {
            throw new NotImplementedException();
        }

        private Task<bool> CreateAndShowWindow(Type viewType, MvxWindowPresentationAttribute attribute, MvxViewModelRequest request)
        {
            if (!typeof(Window).IsAssignableFrom(viewType))
            {
                throw new Exception();
            }

            var mvxWindow = (IMvxGtkView) Mvx.IoCProvider.IoCConstruct(viewType);
            var window = (Window)mvxWindow;

            if (MainWindow is null)
            {
                MainWindow = window;
            }

            mvxWindow.ViewModel = request is MvxViewModelInstanceRequest instance
                ? instance.ViewModelInstance
                : ViewModelLoader.LoadViewModel(request, null);

            window.ShowAll();

            return Task.FromResult(true);
        }
    }
}
