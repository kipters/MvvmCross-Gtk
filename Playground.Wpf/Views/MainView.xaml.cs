using MvvmCross.Platforms.Wpf.Views;
using Playground.Core.ViewModels;

namespace Playground.Wpf.Views
{
    public partial class MainView : MvxWpfView<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
