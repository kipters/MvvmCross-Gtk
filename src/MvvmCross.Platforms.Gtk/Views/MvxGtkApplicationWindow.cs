using System;
using System.Diagnostics;
using Gtk;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace MvvmCross.Platforms.Gtk.Views
{
    public abstract class MvxGtkApplicationWindow : global::Gtk.ApplicationWindow, IMvxGtkWindow
    {
        private IMvxBindingContext? _bindingContext;
        protected MvxGtkApplicationWindow(IntPtr raw, Builder builder) : base(raw)
        {
            Builder = builder;
        }

        protected MvxGtkApplicationWindow(global::Gtk.Application application)
            : base(application)
        {
        }

        public IMvxViewModel ViewModel
        {
            get => (IMvxViewModel)DataContext;
            set
            {
                DataContext = value;
                value.ViewCreated();
                value.ViewAppearing();
                OnViewModelSet();
                value.ViewAppeared();
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

        public virtual void OnViewModelSet() { }
        public Builder? Builder { get; }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            ViewModel.ViewDisappearing();
        }
    }

    public abstract class MvxGtkApplicationWindow<TViewModel> : MvxGtkApplicationWindow, IMvxGtkView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected MvxGtkApplicationWindow(global::Gtk.Application application)
            : base(application)
        {
        }

        protected MvxGtkApplicationWindow(IntPtr raw, Builder builder) : base(raw, builder)
        {
        }

        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
