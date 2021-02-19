using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Gtk.Core;
using Playground.Core.Services;
using Playground.Gtk.Services;
using System.Collections.Generic;
using System.Reflection;

namespace Playground.Gtk
{
    internal class Setup : MvxGtkSetup<Core.App>
    {
        public override IEnumerable<Assembly> GetViewAssemblies() => new[] { GetType().Assembly };

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDialogService, GtkDialogService>();
        }
    }
}
