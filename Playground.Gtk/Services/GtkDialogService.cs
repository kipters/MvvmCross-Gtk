using Gtk;
using MvvmCross.Platforms.Gtk.Presenters;
using Playground.Core.Services;
using System.Threading.Tasks;

namespace Playground.Gtk.Services
{
    public class GtkDialogService : IDialogService
    {
        private readonly IMvxGtkViewPresenter _presenter;

        public GtkDialogService(IMvxGtkViewPresenter presenter)
        {
            _presenter = presenter;
        }

        public Task ShowAlert(string title, string text)
        {
            var dialog = new MessageDialog(_presenter.MainWindow, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, text);
            dialog.Run();
            dialog.Dispose();
            return Task.CompletedTask;
        }
    }
}
