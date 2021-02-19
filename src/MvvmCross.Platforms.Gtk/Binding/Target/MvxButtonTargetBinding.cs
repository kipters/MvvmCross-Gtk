using System;
using System.Windows.Input;
using Gtk;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.WeakSubscription;

namespace MvvmCross.Platforms.Gtk.Binding.Target
{
    public class MvxButtonTargetBinding : MvxConvertingTargetBinding<Button, ICommand>
    {
        private ICommand? _command;
        private IDisposable? _clickSubscription;
        private IDisposable? _canExecuteChangedSubscription;
        private readonly EventHandler<EventArgs> _canExecuteEventHandler;

        public MvxButtonTargetBinding(Button target) : base(target)
        {
            _canExecuteEventHandler = OnCanExecuteChanged;
            _clickSubscription = Target.WeakSubscribe(nameof(Button.Clicked), OnClicked);
        }

        protected override void SetValueImpl(Button target, ICommand value)
        {
            if (_canExecuteChangedSubscription is not null)
            {
                _canExecuteChangedSubscription?.Dispose();
                _canExecuteChangedSubscription = null;
            }

            _command = value;

            if (_command is not null)
            {
                _canExecuteChangedSubscription = _command.WeakSubscribe(_canExecuteEventHandler);
            }

            RefreshEnabledState();
        }

        private void OnCanExecuteChanged(object? sender, EventArgs e) => RefreshEnabledState();

        private void OnClicked(object? sender, EventArgs e)
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
            if (Target is null)
            {
                return;
            }

            Target.Sensitive = _command switch
            {
                null => false,
                _ => _command.CanExecute(null)
            };
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
