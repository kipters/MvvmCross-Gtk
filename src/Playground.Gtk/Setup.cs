using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Gtk.Core;
using Playground.Core.Services;
using Playground.Gtk.Services;

namespace Playground.Gtk
{
    public class Setup : MvxGtkSetup<Core.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDialogService, GtkDialogService>();
        }
    }
}
