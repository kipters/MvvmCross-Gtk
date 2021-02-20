using System.Threading;
using GLib;
using Gtk;
using MvvmCross.Core;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MvvmCross.Platforms.Gtk.Core
{
    public abstract class MvxGtkApplication : global::Gtk.Application, IMvxGtkApplication
    {
        protected MvxGtkApplication(string application_id, ApplicationFlags flags)
            : base(application_id, flags)
        {
            SynchronizationContext.SetSynchronizationContext(new GLib.GLibSynchronizationContext());
            RegisterSetup();
        }

        protected override void OnStartup()
        {
            base.OnStartup();
            MvxGtkSetupSingleton.EnsureSingletonAvailable(this).EnsureInitialized();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            RunAppStart();
        }

        protected virtual void RunAppStart()
        {
            if (Mvx.IoCProvider.TryResolve<IMvxAppStart>(out var startup) && !startup.IsStarted)
            {
                // TODO where to get the hint from?
                startup.Start(null);
            }
        }
        public abstract void RegisterSetup();

        protected override void OnWindowRemoved(Window window)
        {
            base.OnWindowRemoved(window);
            if (window is IMvxView mvx)
            {
                mvx.ViewModel.ViewDisappeared();
                mvx.ViewModel.ViewDestroy();
            }
        }
    }

    public class MvxGtkApplication<TSetup, TApplication> : MvxGtkApplication
        where TSetup : MvxGtkSetup<TApplication>, new()
        where TApplication : class, IMvxApplication, new()
    {
        public MvxGtkApplication(string application_id, ApplicationFlags flags)
            : base(application_id, flags)
        {
        }

        public override void RegisterSetup() => this.RegisterSetupType<TSetup>();
    }
}
