using Gtk;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.Extensions;
using MvvmCross.WeakSubscription;
using System;

namespace MvvmCross.Platforms.Gtk.Binding.Target
{
    public class MvxEntryTextTargetBinding : MvxConvertingTargetBinding<Entry, string>, IMvxEditableTextView
    {
        private IDisposable _subscriptionChanged;

        public MvxEntryTextTargetBinding(Entry target) : base(target)
        {
            if (target is null)
            {
                MvxBindingLog.Error($"Entry in {nameof(MvxEntryTextTargetBinding)} is null");
            }
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public override void SubscribeToEvents()
        {
            _subscriptionChanged = Target.WeakSubscribe(nameof(Entry.Changed), HandleChanged);
        }

        private void HandleChanged(object sender, EventArgs e) => FireValueChanged(Target.Text);

        public string CurrentText => Target.Text;

        protected override void SetValueImpl(Entry target, string value) => target.Text = value;

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (!isDisposing)
            {
                return;
            }

            _subscriptionChanged?.Dispose();
            _subscriptionChanged = null;
        }
    }
}
