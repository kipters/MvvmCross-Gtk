using Gtk;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using MvvmCross.Platforms.Gtk.Views;
using Playground.Core.ViewModels;

namespace Playground.Gtk.Views
{
    [MvxWindowPresentation]
    public class SecondView : MvxGtkApplicationWindow<SecondViewModel>
    {
        private readonly Label _label;
        private readonly Button _closeButton;

        public SecondView(Application application) : base(application)
        {
            DefaultSize = new Gdk.Size(400, 300);

            _label = new Label();
            Add(_label);

            _closeButton = new Button("Close");
            var header = new HeaderBar { ShowCloseButton = true };
            header.Add(_closeButton);
            Titlebar = header;
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
