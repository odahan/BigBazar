#nullable disable
using BigBazar.ViewModels;

namespace BigBazar.Services;

public interface IViewModelFactory
{
    T Create<T>() where T : BaseViewModel;
}