using Avalonia;
using Avalonia.Controls;

namespace AvaloniaTestApplication.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    // ── Safe Area ────────────────────────────────────────────────────────────
    //
    // Called once the control is attached to the visual tree (i.e. a real
    // TopLevel / Window exists).  We read InsetsManager.SafeAreaPadding and
    // push the values into the AppBar and BottomBar borders so they sit
    // completely outside the OS-reserved areas (iPhone notch, Dynamic Island,
    // Android status bar, iOS home indicator, etc.).
    //
    // On a desktop build SafeAreaPadding is always Thickness(0), so this code
    // is harmless there.
    //
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.InsetsManager is { } insets)
        {
            // Apply the initial value right away.
            ApplySafeArea(insets.SafeAreaPadding);

            // Re-apply whenever the OS changes it (e.g. rotation).
            insets.SafeAreaChanged += (_, args) =>
                ApplySafeArea(args.SafeAreaPadding);
        }
    }

    // Adds the platform's safe-area inset to each bar's padding so content
    // is never hidden by the notch / home indicator.
    private void ApplySafeArea(Thickness safeArea)
    {
        // AppBar   – extra top padding keeps content below the status bar / notch.
        AppBar.Padding = new Thickness(0, safeArea.Top, 0, 0);

        // BottomBar – extra bottom padding lifts content above the home indicator.
        BottomBar.Padding = new Thickness(0, 0, 0, safeArea.Bottom);
    }
}
