namespace WiseCompanion;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;


public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();


        Location location = new Location(52.6736, -8.5724);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);

        Map map = new Map(mapSpan)
        {
            IsShowingUser = true, // This will show the user's current location on the map
            MapType = MapType.Street
        };


        Content = map;

    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        var homePage = new HomePage();
        await Navigation.PushModalAsync(homePage);
    }

}

