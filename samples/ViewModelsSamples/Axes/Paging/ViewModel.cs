using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace ViewModelsSamples.Axes.Paging;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

[ObservableObject]
public partial class ViewModel
{
    private readonly Random _random = new();

    private ObservableCollection<double> _values { get; set; } = new();
    private LiveChartsCore.Defaults.ObservablePoint _PointValues { get; set; } = new();

    public ViewModel()
    {
        var trend = 100;
        var values = new List<int>();

        for (var i = 0; i < 100; i++)
        {
            trend += _random.Next(-30, 50);
            values.Add(trend);
            _values.Add(trend);
        }

        Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = _values,
                Stroke = new SolidColorPaint(SKColors.Red,1){StrokeThickness = 1f},// 控制线条粗细
                GeometrySize = 0,
                Fill = null,//new SolidColorPaint( SKColors.Red,0.1f),
            }
        };

        XAxes = new[] { new Axis() };
        GoToPage3Command = new AsyncRelayCommand(GoToPage3);
    }

    private void AutomaticDisplayNewest100Data()
    {
        _values.Add(_random.Next(0, 100));
        var axis = XAxes[0];
        axis.MaxLimit = _values.Count;
        axis.MinLimit = _values.Count - 100;

    }

    public ISeries[] Series { get; }

    public Axis[] XAxes { get; }

    [RelayCommand]
    public void GoToPage1()
    {
        var axis = XAxes[0];
        axis.MinLimit = -0.5;
        axis.MaxLimit = 10.5;
    }

    [RelayCommand]
    public void GoToPage2()
    {
        var axis = XAxes[0];
        axis.MinLimit = 9.5;
        axis.MaxLimit = 20.5;
    }

    [RelayCommand]
    public void GoToPage31()
    {
        var axis = XAxes[0];
        axis.MinLimit = 19.5;
        axis.MaxLimit = 30.5;
    }

    // [RelayCommand]
    public IAsyncRelayCommand GoToPage3Command { get; set; }

    public async Task GoToPage3()
    {
        await Task.Run(async () =>
        {
            for (var i = 0; i < 1000; i++)
            {
                await Task.Delay(10);
                AutomaticDisplayNewest100Data();
            }
        }
        );
    }

    [RelayCommand]
    public void SeeAll()
    {
        var axis = XAxes[0];
        axis.MinLimit = null;
        axis.MaxLimit = null;
    }
}
