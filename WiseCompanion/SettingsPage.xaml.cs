//namespace WiseCompanion;

//using System.Diagnostics;
//using WiseCompanion.ViewModels;

//public partial class SettingsPage : ContentPage
//{
//    public SettingsPage()
//    {
//        InitializeComponent();
//        BindingContext = new SettingsViewModel(Navigation);
//        LoadColorPalette();
//    }

//    private async void BackButtonClicked(object sender, EventArgs e)
//    {
//        var homePage = new HomePage();
//        await Navigation.PushModalAsync(homePage);
//    }

//    private void LoadColorPalette()
//    {
//        var colors = new string[]
//        {
//            "#FFC0CB", // Pink
//            "#C3D6BD", // Initial color
//            "#FFFF00", // Yellow
//            "#00FFFF"  // Aqua
//        };

//        foreach (var colorHex in colors)
//        {
//            var colorButton = new Button
//            {
//                BackgroundColor = Color.FromHex(colorHex),
//                HeightRequest = 40,
//                WidthRequest = 40,
//                CornerRadius = 20,
//                Margin = 5
//            };
//            colorButton.Clicked += ColorButton_Clicked;
//            colorPaletteStack.Children.Add(colorButton);
//        }
//    }

//    private void ColorButton_Clicked(object sender, EventArgs e)
//    {
//            Debug.WriteLine("ColorButton_Clicked called");
//        if (sender is Button button)
//        {
//            string colorHex = button.BackgroundColor.ToHex();
//            Debug.WriteLine($"Sending color change: {colorHex}");
//            MessagingCenter.Send(this, "BackgroundColorChanged", colorHex);
//        }
//    }




//}

using System;
using Microsoft.Maui.Controls;
using WiseCompanion.ViewModels;

namespace WiseCompanion;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = new SettingsViewModel(Dispatcher);
        LoadColorPalette();
        SubscribeToMessages();
    }


    private void LoadColorPalette()
    {
        var colors = new string[]
        {
        "#FFFFFF", // White
        "#FFB3BA", // Pastel Red
        "#FFDFBA", // Pastel Orange
        "#FFFFBA", // Pastel Yellow
        "#C3D6BD", // Pastel Green
        "#BAE1FF", // Pastel Blue
        "#F5D7DC", // Pastel Pink
        "#D3ECEC", // Pastel Cyan
        "#ECC8EC", // Pastel Purple
        "#ACB9E8", // Pastel Blue
        };

        int row = 0, column = 0;

        foreach (var colorHex in colors)
        {
            var colorButton = new Button
            {
                BackgroundColor = Color.FromArgb(colorHex),
                HeightRequest = 40,
                WidthRequest = 40,
                CornerRadius = 20,
                Margin = 5
            };
            colorButton.Clicked += ColorButton_Clicked;

            Grid.SetRow(colorButton, row);
            Grid.SetColumn(colorButton, column);
            colorPaletteStack.Children.Add(colorButton);

            column++;
            if (column > 4)
            {
                column = 0;
                row++;
            }
        }
    


        foreach (var colorHex in colors)
        {
            var colorButton = new Button
            {
                BackgroundColor = Color.FromArgb(colorHex),
                HeightRequest = 40,
                WidthRequest = 40,
                CornerRadius = 20,
                Margin = 5
            };
            colorButton.Clicked += ColorButton_Clicked;
            colorPaletteStack.Children.Add(colorButton);
        }
    }

    private void ColorButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            var colorHex = button.BackgroundColor.ToHex();
            this.BackgroundColor = Color.FromArgb(colorHex);

            var viewModel = BindingContext as SettingsViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedBackgroundColor = colorHex; 
            }
        }
    }

    private void SubscribeToMessages()
    {
        MessagingCenter.Subscribe<SettingsViewModel>(this, "ResetFontPicker", (sender) =>
        {
            Dispatcher.Dispatch(() =>
            {
                FontPicker.SelectedItem = null;  
            });
        });
    }


    private async void BackButtonClicked(object sender, EventArgs e)
    {

        MessagingCenter.Send(this, "RefreshUI");

        await Navigation.PopModalAsync();
    }
}
