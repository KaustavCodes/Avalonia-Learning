using System;
using System.Timers;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTestApplication.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly Timer _clockTimer;
    private Timer? _confettiOffTimer;
    private readonly Action _navigateToStats;

    [ObservableProperty] private string _currentTime = DateTime.Now.ToString("HH:mm:ss");
    [ObservableProperty] private string _currentDate = DateTime.Now.ToString("dddd, MMMM d");
    [ObservableProperty] private bool _isConfettiActive;

    public string Revenue => "$48,320";
    public string Users => "2,847";
    public string Orders => "1,239";
    public string Growth => "+12.4%";

    public DashboardViewModel(Action navigateToStats)
    {
        _navigateToStats = navigateToStats;
        _clockTimer = new Timer(1000) { AutoReset = true };
        _clockTimer.Elapsed += (_, _) => UpdateClock();
        _clockTimer.Start();
    }

    private void UpdateClock()
    {
        Dispatcher.UIThread.Post(() =>
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            CurrentDate = DateTime.Now.ToString("dddd, MMMM d");
        });
    }

    [RelayCommand]
    private void Celebrate()
    {
        IsConfettiActive = true;
        _confettiOffTimer?.Stop();
        _confettiOffTimer?.Dispose();
        _confettiOffTimer = new Timer(5000) { AutoReset = false };
        _confettiOffTimer.Elapsed += (_, _) =>
            Dispatcher.UIThread.Post(() => IsConfettiActive = false);
        _confettiOffTimer.Start();
    }

    [RelayCommand]
    private void GoToStats() => _navigateToStats();

    public void Cleanup()
    {
        _clockTimer.Stop();
        _clockTimer.Dispose();
        _confettiOffTimer?.Stop();
        _confettiOffTimer?.Dispose();
    }
}
