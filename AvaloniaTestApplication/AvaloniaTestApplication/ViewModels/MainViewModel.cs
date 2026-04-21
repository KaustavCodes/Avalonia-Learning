using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaTestApplication.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentPage = null!;

    private readonly DashboardViewModel _dashboardVm;
    private readonly StatsViewModel _statsVm;

    public MainViewModel()
    {
        _dashboardVm = new DashboardViewModel(NavigateToStats);
        _statsVm = new StatsViewModel(NavigateBack);
        CurrentPage = _dashboardVm;
    }

    private void NavigateToStats() => CurrentPage = _statsVm;
    private void NavigateBack() => CurrentPage = _dashboardVm;
}
