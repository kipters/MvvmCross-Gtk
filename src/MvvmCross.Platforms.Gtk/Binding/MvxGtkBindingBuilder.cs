using Gtk;
using MvvmCross.Binding;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Gtk.Binding.Target;

namespace MvvmCross.Platforms.Gtk.Binding
{
    public class MvxGtkBindingBuilder : MvxBindingBuilder
    {
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<Label>(
                nameof(Label.Text),
                view => new MvxLabelTextTargetBinding(view));

            registry.RegisterCustomBindingFactory<Entry>(
                nameof(Entry.Text),
                view => new MvxEntryTextTargetBinding(view));

            registry.RegisterCustomBindingFactory<Button>(
                nameof(Button.Clicked),
                view => new MvxButtonTargetBinding(view));
        }

        protected override void FillDefaultBindingNames(IMvxBindingNameRegistry registry)
        {
            base.FillDefaultBindingNames(registry);

            registry.AddOrOverwrite<Label>(l => l.Text);
            registry.AddOrOverwrite<Entry>(l => l.Text);
            registry.AddOrOverwrite(typeof(Button), nameof(Button.Clicked));
        }
    }
}
