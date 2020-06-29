using System;
using Gtk;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace MvvmCross.Platforms.Gtk.Views
{
    public abstract class MvxGtkApplicationWindow : ApplicationWindow, IMvxGtkWindow
    {
        private IMvxBindingContext _bindingContext;
        protected MvxGtkApplicationWindow(Application application) : base(application)
        {
        }

        public IMvxViewModel ViewModel
        {
            get => (IMvxViewModel)DataContext;
            set
            {
                DataContext = value;
                OnViewModelSet();
            }
        }

        public object DataContext
        {
            get => BindingContext.DataContext;
            set => BindingContext.DataContext = value;
        }

        public IMvxBindingContext BindingContext
        {
            get => _bindingContext ??= Mvx.IoCProvider.Resolve<IMvxBindingContext>();
            set => _bindingContext = value;
        }

        public abstract void OnViewModelSet();
    }

    public abstract class MvxGtkApplicationWindow<TViewModel> : MvxGtkApplicationWindow, IMvxGtkView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected MvxGtkApplicationWindow(Application application) : base(application)
        {
        }

        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
