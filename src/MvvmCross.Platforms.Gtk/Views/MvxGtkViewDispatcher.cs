using System;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MvvmCross.Platforms.Gtk.Views
{
    public class MvxGtkViewDispatcher
        : MvxMainThreadAsyncDispatcher
        , IMvxViewDispatcher
    {
        private readonly IMvxViewPresenter _presenter;

        public MvxGtkViewDispatcher(IMvxViewPresenter presenter)
        {
            _presenter = presenter;
        }

        public override bool IsOnMainThread => SynchronizationContext.Current is not null;

        public async Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            await ExecuteOnMainThreadAsync(() => _presenter.ChangePresentation(hint));
            return true;
        }

        public override bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            global::Gtk.Application.Invoke((s, e) => ExceptionMaskedAction(action, maskExceptions));
            return true;
        }

        public async Task<bool> ShowViewModel(MvxViewModelRequest request)
        {
            if (IsOnMainThread)
            {
                await _presenter.Show(request);
            }
            else
            {
                await ExecuteOnMainThreadAsync(() => _presenter.Show(request));
            };

            return true;
        }
    }
}
