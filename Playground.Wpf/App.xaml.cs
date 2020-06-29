using MvvmCross.Platforms.Wpf.Views;

namespace Playground.Wpf
{
    public abstract class AppBase : MvxApplication<Setup, Core.App> { }
    public partial class App : AppBase
    {
    }
}
