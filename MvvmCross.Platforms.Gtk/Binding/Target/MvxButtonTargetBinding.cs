using Gtk;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.WeakSubscription;
using System;
using System.Windows.Input;

namespace MvvmCross.Platforms.Gtk.Binding.Target
{
    public class MvxButtonTargetBinding : MvxConvertingTargetBinding<Button, ICommand>
    {
        private ICommand _command;
        private IDisposable _clickSubscription;
        private IDisposable _canExecuteChangedSubscription;
        private readonly EventHandler<EventArgs> _canExecuteEventHandler;

        public MvxButtonTargetBinding(Button target) : base(target)
        {
            if (target is null)
            {
                MvxBindingLog.Error($"Target of {nameof(MvxButtonTargetBinding)} is null");
            }
            else
            {
                _clickSubscription = target.WeakSubscribe(nameof(Button.Clicked), OnClicked);
                _canExecuteEventHandler = OnCanExecuteChanged;
            }
        }

        protected override void SetValueImpl(Button target, ICommand value)
        {
            if (!(_canExecuteChangedSubscription is null))
            {
                _canExecuteChangedSubscription?.Dispose();
                _canExecuteChangedSubscription = null;
            }

            _command = value;

            if (!(_command is null))
            {
                _canExecuteChangedSubscription = _command.WeakSubscribe(_canExecuteEventHandler);
            }

            RefreshEnabledState();
        }

        private void OnCanExecuteChanged(object sender, EventArgs e) => RefreshEnabledState();

        private void OnClicked(object sender, EventArgs e)
        {
            if (_command is null)
            {
                return;
            }

            if (!_command.CanExecute(null))
            {
                return;
            }

            _command.Execute(null);
        }

        private void RefreshEnabledState()
        {
            var view = Target;
            if (view is null)
            {
                return;
            }

            var shouldBeEnabled = false;
            if (!(_command is null))
            {
                shouldBeEnabled = _command.CanExecute(null);
            }

            view.Sensitive = shouldBeEnabled;
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _clickSubscription?.Dispose();
                _canExecuteChangedSubscription?.Dispose();
                _canExecuteChangedSubscription = null;
                _clickSubscription = null;
            }

            base.Dispose(isDisposing);
        }
    }
}
