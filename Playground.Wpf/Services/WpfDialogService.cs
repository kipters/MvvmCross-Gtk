using Playground.Core.Services;
using System.Threading.Tasks;
using System.Windows;

namespace Playground.Wpf.Services
{
    public class WpfDialogService : IDialogService
    {
        public Task ShowAlert(string title, string text) =>
            Task.Run(() => MessageBox.Show(text, title));
    }
}
