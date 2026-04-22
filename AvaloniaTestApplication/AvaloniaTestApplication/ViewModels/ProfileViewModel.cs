using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaTestApplication.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    public string AvatarInitials => "KH";
    public string Name => "Kaustav Halder";
    public string Role => "Senior Developer";
    public string Email => "kaustav@example.com";
    public string JoinDate => "Member since January 2024";

    // Quick-stat cards shown on the profile page
    public string Projects => "24";
    public string Commits => "1,847";
    public string Stars => "312";
}
