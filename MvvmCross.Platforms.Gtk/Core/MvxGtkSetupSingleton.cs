using MvvmCross.Core;

namespace MvvmCross.Platforms.Gtk.Core
{
    public class MvxGtkSetupSingleton : MvxSetupSingleton
    {
        public static MvxGtkSetupSingleton EnsureSingletonAvailable(MvxGtkApplication application)
        {
            var instance = EnsureSingletonAvailable<MvxGtkSetupSingleton>();
            instance.PlatformSetup<MvxGtkSetup>()?.PlatformInitialize(application);
            return instance;
        }
    }
}
