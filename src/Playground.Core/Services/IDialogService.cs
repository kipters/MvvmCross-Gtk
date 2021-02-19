using System.Threading.Tasks;

namespace Playground.Core.Services
{
    public interface IDialogService
    {
        Task ShowAlert(string title, string text);
    }
}
