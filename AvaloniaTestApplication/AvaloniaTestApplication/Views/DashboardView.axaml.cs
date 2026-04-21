using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaTestApplication.ViewModels;

namespace AvaloniaTestApplication.Views;

public partial class DashboardView : UserControl
{
    private readonly List<ConfettiParticle> _particles = new();
    private DispatcherTimer? _confettiTimer;
    private DashboardViewModel? _vm;

    public DashboardView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (_vm != null)
            _vm.PropertyChanged -= OnViewModelPropertyChanged;

        _vm = DataContext as DashboardViewModel;

        if (_vm != null)
            _vm.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DashboardViewModel.IsConfettiActive))
        {
            if (_vm?.IsConfettiActive == true)
                StartConfetti();
            else
                StopConfetti();
        }
    }

    private void StartConfetti()
    {
        var canvas = this.FindControl<Canvas>("ConfettiCanvas");
        if (canvas == null) return;

        canvas.Children.Clear();
        _particles.Clear();

        var colors = new[]
        {
            "#FF6584", "#7C3AED", "#06B6D4", "#F59E0B",
            "#EC4899", "#10B981", "#A855F7", "#EF4444",
            "#3B82F6", "#F97316"
        };
        var rng = new Random();
        double canvasWidth = canvas.Bounds.Width > 0 ? canvas.Bounds.Width : 430;

        for (int i = 0; i < 100; i++)
        {
            bool isCircle = rng.Next(3) == 0;
            Control shape;

            var fill = new SolidColorBrush(Color.Parse(colors[rng.Next(colors.Length)]));

            if (isCircle)
            {
                shape = new Ellipse
                {
                    Width = rng.Next(6, 13),
                    Height = rng.Next(6, 13),
                    Fill = fill,
                    Opacity = 0.85,
                    IsHitTestVisible = false,
                };
            }
            else
            {
                shape = new Rectangle
                {
                    Width = rng.Next(6, 14),
                    Height = rng.Next(10, 20),
                    Fill = fill,
                    Opacity = 0.9,
                    RenderTransform = new RotateTransform(rng.Next(360)),
                    IsHitTestVisible = false,
                };
            }

            double x = rng.NextDouble() * canvasWidth;
            double y = -rng.Next(20, 350);

            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
            canvas.Children.Add(shape);

            _particles.Add(new ConfettiParticle
            {
                Shape = shape,
                X = x,
                Y = y,
                SpeedY = 1.8 + rng.NextDouble() * 3.5,
                SpeedX = (rng.NextDouble() - 0.5) * 1.8,
                Rotation = rng.Next(360),
                RotationSpeed = (rng.NextDouble() - 0.5) * 9,
                Wobble = rng.NextDouble() * Math.PI * 2,
                WobbleSpeed = 0.04 + rng.NextDouble() * 0.06,
                IsCircle = isCircle,
            });
        }

        _confettiTimer?.Stop();
        _confettiTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
        _confettiTimer.Tick += OnConfettiTick;
        _confettiTimer.Start();
    }

    private void OnConfettiTick(object? sender, EventArgs e)
    {
        var canvas = this.FindControl<Canvas>("ConfettiCanvas");
        if (canvas == null) return;

        double height = canvas.Bounds.Height + 60;
        bool anyVisible = false;

        foreach (var p in _particles)
        {
            p.Y += p.SpeedY;
            p.SpeedY += 0.04; // gravity
            p.Wobble += p.WobbleSpeed;
            p.X += p.SpeedX + Math.Sin(p.Wobble) * 0.6; // gentle horizontal drift
            p.Rotation += p.RotationSpeed;

            Canvas.SetLeft(p.Shape, p.X);
            Canvas.SetTop(p.Shape, p.Y);

            if (!p.IsCircle && p.Shape.RenderTransform is RotateTransform rt)
                rt.Angle = p.Rotation;

            if (p.Y < height)
                anyVisible = true;
        }

        if (!anyVisible)
        {
            _confettiTimer?.Stop();
            canvas.Children.Clear();
            _particles.Clear();
        }
    }

    private void StopConfetti()
    {
        _confettiTimer?.Stop();
        var canvas = this.FindControl<Canvas>("ConfettiCanvas");
        canvas?.Children.Clear();
        _particles.Clear();
    }

    private class ConfettiParticle
    {
        public Control Shape = null!;
        public double X, Y, SpeedY, SpeedX, Rotation, RotationSpeed;
        public double Wobble, WobbleSpeed;
        public bool IsCircle;
    }
}
