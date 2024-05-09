#nullable disable
using BigBazar.ViewModels;

namespace BigBazar.Services;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Create<T>() where T : BaseViewModel
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
