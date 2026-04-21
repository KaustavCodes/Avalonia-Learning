using System;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTestApplication.ViewModels;

public class StatItem
{
    public string Icon { get; set; } = "";
    public string Label { get; set; } = "";
    public string Value { get; set; } = "";
    public double Progress { get; set; }
    public SolidColorBrush BarBrush { get; set; } = new(Color.Parse("#7C3AED"));
    public string Trend { get; set; } = "";
    public string TrendColor { get; set; } = "#10B981";
}

public partial class StatsViewModel : ViewModelBase
{
    private readonly Action _navigateBack;

    public ObservableCollection<StatItem> Stats { get; } = new()
    {
        new StatItem
        {
            Icon = "💰", Label = "Monthly Revenue", Value = "$48,320",
            Progress = 0.74, BarBrush = new(Color.Parse("#7C3AED")),
            Trend = "↑ 8.2%", TrendColor = "#A78BFA"
        },
        new StatItem
        {
            Icon = "👥", Label = "Active Users", Value = "2,847",
            Progress = 0.58, BarBrush = new(Color.Parse("#06B6D4")),
            Trend = "↑ 3.1%", TrendColor = "#67E8F9"
        },
        new StatItem
        {
            Icon = "📦", Label = "Orders Completed", Value = "1,239",
            Progress = 0.83, BarBrush = new(Color.Parse("#EC4899")),
            Trend = "↑ 12.4%", TrendColor = "#F9A8D4"
        },
        new StatItem
        {
            Icon = "📈", Label = "Growth Rate", Value = "+12.4%",
            Progress = 0.45, BarBrush = new(Color.Parse("#F59E0B")),
            Trend = "↑ 2.0%", TrendColor = "#FCD34D"
        },
        new StatItem
        {
            Icon = "⭐", Label = "Satisfaction Score", Value = "4.8 / 5.0",
            Progress = 0.96, BarBrush = new(Color.Parse("#10B981")),
            Trend = "↑ 0.3", TrendColor = "#6EE7B7"
        },
        new StatItem
        {
            Icon = "🔄", Label = "User Retention", Value = "87%",
            Progress = 0.87, BarBrush = new(Color.Parse("#A855F7")),
            Trend = "→ stable", TrendColor = "#D8B4FE"
        },
    };

    public StatsViewModel(Action navigateBack)
    {
        _navigateBack = navigateBack;
    }

    [RelayCommand]
    private void GoBack() => _navigateBack();
}
