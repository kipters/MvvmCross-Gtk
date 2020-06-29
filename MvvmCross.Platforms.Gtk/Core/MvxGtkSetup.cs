using Gtk;
using MvvmCross.Binding;
using MvvmCross.Core;
using MvvmCross.Platforms.Gtk.Binding;
using MvvmCross.Platforms.Gtk.Presenters;
using MvvmCross.Platforms.Gtk.Views;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MvvmCross.Platforms.Gtk.Core
{
    public abstract class MvxGtkSetup : MvxSetup, IMvxGtkSetup
    {
        private IMvxGtkViewPresenter _presenter;
        private Application _application;
        protected IMvxGtkViewPresenter Presenter => _presenter ??= CreateViewPresenter();

        protected override void InitializeFirstChance()
        {
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

        public virtual void PlatformInitialize(Application application)
        {
            _application = application;
        }

        protected virtual IMvxGtkViewPresenter CreateViewPresenter()
        {
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
            return new MvxGtkViewsContainer();
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
