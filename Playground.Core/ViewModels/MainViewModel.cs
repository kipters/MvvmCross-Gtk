using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Playground.Core.Services;

namespace Playground.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IDialogService _dialog;
        public MainViewModel(IDialogService dialog)
        {
            _dialog = dialog;

            DialogCommand = new MvxAsyncCommand(
                async () => await _dialog.ShowAlert("Debug", Message.Trim()),
                () => !string.IsNullOrWhiteSpace(Message));
        }

        private string _message = "Hello MvvmCross!";
        public string Message
        {
            get => _message;
            set
            {
                SetProperty(ref _message, value);
                DialogCommand.RaiseCanExecuteChanged();
            }
        }

        public IMvxAsyncCommand DialogCommand { get; }
    }
}
