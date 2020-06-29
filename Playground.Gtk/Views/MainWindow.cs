using Gtk;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using MvvmCross.Platforms.Gtk.Views;
using Playground.Core.ViewModels;

namespace Playground.Gtk.Views
{
    [MvxWindowPresentation]
    public class MainWindow : MvxGtkApplicationWindow<MainViewModel>
    {
        public MainWindow(Application application) : base(application)
        {
            DefaultSize = new Gdk.Size(400, 300);
        }

        public override void OnViewModelSet()
        {
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

            var header = new HeaderBar
            {
                ShowCloseButton = true
            };

            grid.Attach(label, 0, 1, 1, 1);
            grid.Attach(entry, 0, 2, 1, 1);
            //grid.Attach(button, 0, 3, 1, 1);
            header.Add(button);

            Add(grid);
            Titlebar = header;
            Title = "Playground.Gtk";
            ShowMenubar = true;

            var set = this.CreateBindingSet<MainWindow, MainViewModel>();
            set.Bind(label).To(vm => vm.Message).OneWay();
            set.Bind(entry).To(vm => vm.Message).TwoWay();
            set.Bind(button).To(vm => vm.DialogCommand).OneTime();
            set.Apply();
        }
    }
}
