using System;
using MvvmCross.Binding;
using MvvmCross.Core;
using MvvmCross.Platforms.Gtk.Binding;
using MvvmCross.Platforms.Gtk.Presenters;
using MvvmCross.Platforms.Gtk.Views;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using GtkApp = Gtk.Application;

namespace MvvmCross.Platforms.Gtk.Core
{
    public abstract class MvxGtkSetup : MvxSetup, IMvxGtkSetup
    {
        private IMvxGtkViewPresenter? _presenter;
        private GtkApp? _application;
        protected IMvxGtkViewPresenter Presenter => _presenter ??= CreateViewPresenter();

        protected override void InitializeFirstChance()
        {
            if (_application is null)
            {
                throw new Exception("Setup.PlatformInitialize() hasn't been called");
            }

            RegisterPresenter();
            Mvx.IoCProvider.RegisterSingleton(_application);
            base.InitializeFirstChance();
        }

        protected override void InitializeLastChance()
        {
            InitializeBindingBuilder();
            base.InitializeLastChance();
        }

        protected virtual void InitializeBindingBuilder()
        {
            var builder = CreateBindingBuilder();
            builder.DoRegistration();
        }

        protected virtual MvxBindingBuilder CreateBindingBuilder()
        {
            return new MvxGtkBindingBuilder();
        }

        internal virtual void PlatformInitialize(MvxGtkApplication application)
        {
            _application = application;
        }

        protected virtual IMvxGtkViewPresenter CreateViewPresenter()
        {
            if (_application is null)
            {
                throw new Exception("Setup.PlatformInitialize() hasn't been called");
            }

            return new MvxGtkViewPresenter(_application);
        }

        protected virtual void RegisterPresenter()
        {
            var presenter = Presenter;
            Mvx.IoCProvider.RegisterSingleton(presenter);
            Mvx.IoCProvider.RegisterSingleton<IMvxViewPresenter>(presenter);
        }

        protected override IMvxViewDispatcher CreateViewDispatcher()
        {
            return new MvxGtkViewDispatcher(Presenter);
        }

        protected override IMvxViewsContainer CreateViewsContainer()
        {
            var container = CreateGtkViewsContainer();
            Mvx.IoCProvider.RegisterSingleton<IMvxGtkViewLoader>(container);
            return container;
        }

        protected virtual IMvxGtkViewsContainer CreateGtkViewsContainer()
        {
            if (_application is null)
            {
                throw new Exception("Setup.PlatformInitialize() hasn't been called");
            }

            return new MvxGtkViewsContainer(_application);
        }

        protected override IMvxNameMapping CreateViewToViewModelNaming()
        {
            return new MvxPostfixAwareViewToViewModelNameMapping("View", "Window");
        }
    }

    public class MvxGtkSetup<TApplication> : MvxGtkSetup
        where TApplication : class, IMvxApplication, new()
    {
        protected override IMvxApplication CreateApp()
        {
            return Mvx.IoCProvider.IoCConstruct<TApplication>();
        }
    }
}
