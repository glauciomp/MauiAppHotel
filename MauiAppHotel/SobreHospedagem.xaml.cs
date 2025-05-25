using Microsoft.Maui.Controls;

namespace MauiAppHotel.Views;

public partial class SobreHospedagem : ContentPage
{
    public SobreHospedagem()
    {
        InitializeComponent();
    }

    private async void OnVoltarButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}