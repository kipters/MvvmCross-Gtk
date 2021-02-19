using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MvvmCross.Platforms.Gtk.Views
{
    public interface IMvxGtkView : IMvxView, IMvxBindingContextOwner
    {
        void OnViewModelSet();
    }

    public interface IMvxGtkView<T> : IMvxGtkView, IMvxView<T>
        where T : class, IMvxViewModel
    {
    }
}
