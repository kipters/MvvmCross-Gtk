using Gtk;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;

namespace MvvmCross.Platforms.Gtk.Binding.Target
{
    public class MvxLabelTextTargetBinding : MvxConvertingTargetBinding<Label, string>
    {
        public MvxLabelTextTargetBinding(Label target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(Label target, string value) => target.Text = value;
    }
}
