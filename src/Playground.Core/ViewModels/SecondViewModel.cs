using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Playground.Core.ViewModels
{
    public class SecondViewModel : MvxViewModel
    {
        private readonly Timer _timer;

        public SecondViewModel(IMvxNavigationService navigation)
        {
            _timer = new Timer(OnTick, null, 0, 1000);
            CloseCommand = new MvxAsyncCommand(async () => await navigation.Close(this));
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            _timer.Change(-1, -1);
            _timer.Dispose();
        }

        private void OnTick(object? state)
        {
            var time = DateTime.UtcNow.ToLongTimeString();
            Debug.WriteLine(time);
            Time = time;
        }

        private string _time = string.Empty;
        public string Time { get => _time; set => SetProperty(ref _time, value); }

        public ICommand CloseCommand { get; }
    }
}
