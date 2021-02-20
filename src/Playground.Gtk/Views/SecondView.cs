using System;
using Gtk;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using MvvmCross.Platforms.Gtk.Views;
using Playground.Core.ViewModels;

namespace Playground.Gtk.Views
{
    [MvxWindowPresentation(ResourceName = "glade/second_window.glade", ObjectId = "window1")]
    public class SecondView : MvxGtkApplicationWindow<SecondViewModel>
    {
        private readonly Label _label;
        private readonly Button _closeButton;
        
        public SecondView(IntPtr raw, Builder builder) : base(raw, builder)
        {
            _closeButton = (Button) Builder!.GetObject("button1");
            _label = (Label) Builder!.GetObject("timeLabel");
        }

        public override void OnViewModelSet()
        {
            var set = this.CreateBindingSet<SecondView, SecondViewModel>();
            set.Bind(_label).To(vm => vm.Time).OneWay();
            set.Bind(_closeButton).To(vm => vm.CloseCommand).OneTime();
            set.Apply();
        }
    }
}
