using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaTestApplication.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Click!");
        Debug.WriteLine($"Celsius: {Celsius.Text}");
        Debug.WriteLine($"Fahrenheit: {Fahrenheit.Text}");
    }
}