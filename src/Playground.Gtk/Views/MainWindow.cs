using Gtk;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Gtk.Views;
using Playground.Core.ViewModels;

namespace Playground.Gtk.Views
{
    public class MainWindow : MvxGtkApplicationWindow<MainViewModel>
    {
        private readonly Entry _entry;
        private readonly Label _label;  
        private readonly Button _button;
        private readonly Button _secondButton;

        public MainWindow(Application application) : base(application)
        {
            DefaultSize = new Gdk.Size(600, 450);

            var grid = new Grid
            {
                Margin = 10,
                Expand = true
            };

            var label = new Label
            {
                Expand = true,
                Valign = Align.Center,
                Halign = Align.Fill
            };

            var entry = new Entry
            {
                Expand = true,
                Valign = Align.Center,
                Halign = Align.Fill
            };

            var button = new Button("Show Dialog");
            var secondButton = new Button("Show second view");
            secondButton.StyleContext.AddClass("suggested-action");

            var header = new HeaderBar
            {
                ShowCloseButton = true
            };

            grid.Attach(label, 0, 1, 1, 1);
            grid.Attach(entry, 0, 2, 1, 1);
            header.Add(button);
            header.Add(secondButton);

            Add(grid);
            Titlebar = header;
            Title = "Playground.Gtk";
            ShowMenubar = true;

            _entry = entry;
            _label = label;
            _button = button;
            _secondButton = secondButton;
        }

        public override void OnViewModelSet()
        {
            var set = this.CreateBindingSet<MainWindow, MainViewModel>();
            set.Bind(_entry).To(vm => vm.Message).TwoWay();
            set.Bind(_label).To(vm => vm.Message).OneWay();
            set.Bind(_button).To(vm => vm.DialogCommand).OneTime();
            set.Bind(_secondButton).To(vm => vm.SecondViewCommand).OneTime();
            set.Apply();
        }
    }
}
