using Gtk;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using System;

namespace MvvmCross.Platforms.Gtk.Binding.Target
{
    public class MvxLabelTextTargetBinding : MvxConvertingTargetBinding
    {
        public MvxLabelTextTargetBinding(object target) : base(target)
        {
            if (target is null)
            {
                MvxBindingLog.Error($"Label in {nameof(MvxLabelTextTargetBinding)} is null");
            }
        }

        public override Type TargetType => typeof(string);
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(object target, object value)
        {
            var view = (Label)target;
            view.Text = (string)value;
        }
    }
}
