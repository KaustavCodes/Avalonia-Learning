using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTestApplication.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // ── observable state ──────────────────────────────────────────────────────

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HomeTabForeground))]
    [NotifyPropertyChangedFor(nameof(StatsTabForeground))]
    [NotifyPropertyChangedFor(nameof(ProfileTabForeground))]
    [NotifyPropertyChangedFor(nameof(CurrentPageTitle))]
    private int _selectedTabIndex;

    [ObservableProperty] private ViewModelBase _currentPage = null!;

    // ── child view-models ─────────────────────────────────────────────────────

    private readonly DashboardViewModel _dashboardVm;
    private readonly StatsViewModel _statsVm;
    private readonly ProfileViewModel _profileVm;

    // ── derived properties (used by bottom-bar for active colour) ─────────────

    // Returns a purple brush for the active tab, muted grey for inactive ones.
    // Because these are plain computed properties, no converter is needed in XAML.
    public IBrush HomeTabForeground => TabBrush(0);
    public IBrush StatsTabForeground => TabBrush(1);
    public IBrush ProfileTabForeground => TabBrush(2);

    public string CurrentPageTitle => SelectedTabIndex switch
    {
        0 => "Dashboard",
        1 => "Statistics",
        2 => "Profile",
        _ => "App"
    };

    // ── construction ──────────────────────────────────────────────────────────

    public MainViewModel()
    {
        // Pass navigation actions so existing in-page buttons still work.
        _dashboardVm = new DashboardViewModel(SelectStats);
        _statsVm = new StatsViewModel(SelectHome);
        _profileVm = new ProfileViewModel();
        CurrentPage = _dashboardVm;
    }

    // ── tab commands (bound by the bottom bar buttons) ────────────────────────

    [RelayCommand] private void SelectHome() { SelectedTabIndex = 0; CurrentPage = _dashboardVm; }
    [RelayCommand] private void SelectStats() { SelectedTabIndex = 1; CurrentPage = _statsVm; }
    [RelayCommand] private void SelectProfile() { SelectedTabIndex = 2; CurrentPage = _profileVm; }

    // ── helpers ───────────────────────────────────────────────────────────────

    private IBrush TabBrush(int tabIndex) =>
        SelectedTabIndex == tabIndex
            ? new SolidColorBrush(Color.Parse("#A78BFA"))   // active  – lavender
            : new SolidColorBrush(Color.Parse("#4A5568"));  // inactive – muted grey
}
