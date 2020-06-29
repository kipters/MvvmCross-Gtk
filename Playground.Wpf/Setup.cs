using MvvmCross;
using MvvmCross.Platforms.Wpf.Core;
using Playground.Core.Services;
using Playground.Wpf.Services;

namespace Playground.Wpf
{
    public class Setup : MvxWpfSetup<Core.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.RegisterSingleton<IDialogService>(new WpfDialogService());
        }
    }
}
