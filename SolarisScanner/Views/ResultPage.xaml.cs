using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using SolarisScanner.Models;
using SolarisScanner.ViewModels;

namespace SolarisScanner.Views;

public partial class ResultPage : ContentPage
{
    private readonly ResultViewModel _viewModel;
    public ResultPage(Reservation reservation)
    {
        InitializeComponent();
        _viewModel = new ResultViewModel(reservation);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // int count = Navigation.NavigationStack.Count;
        // var previousPage = Navigation.NavigationStack[1];
        // Navigation.RemovePage(previousPage);
    }

    protected override bool OnBackButtonPressed()
    {
        // Navigation.InsertPageBefore(new ScanPage(), this);
        return base.OnBackButtonPressed();
        
    }
}