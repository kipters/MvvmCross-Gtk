using GLib;
using MvvmCross.Core;
using MvvmCross.ViewModels;
using System;
using Application = Gtk.Application;

namespace MvvmCross.Platforms.Gtk.Core
{
    public abstract class MvxGtkApplication : Application, IMvxGtkApplication
    {
        protected MvxGtkApplication(string application_id, ApplicationFlags flags) : base(application_id, flags)
        {
            RegisterSetup();
            Activated += OnActivatedInternal;
        }

        private void OnActivatedInternal(object sender, EventArgs e)
        {
            MvxGtkSetupSingleton.EnsureSingletonAvailable(this).EnsureInitialized();
            OnAppActivated();
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

        public abstract void OnAppActivated();
        public abstract void RegisterSetup();
    }

    public class MvxGtkApplication<TSetup, TApplication> : MvxGtkApplication
        where TSetup : MvxGtkSetup<TApplication>, new()
        where TApplication : class, IMvxApplication, new()
    {
        public MvxGtkApplication(string application_id, ApplicationFlags flags)
            : base(application_id, flags)
        {
        }

        public override void OnAppActivated()
        {
        }

        public override void RegisterSetup() => this.RegisterSetupType<TSetup>();
    }
}
