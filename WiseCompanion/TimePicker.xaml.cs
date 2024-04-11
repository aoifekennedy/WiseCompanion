using Microsoft.Maui.Controls;

namespace WiseCompanion;

public partial class TimePickerPage : ContentPage
{
    public TimePickerPage()
    {
        InitializeComponent();
    }

    private void OnConfirmClicked(object sender, EventArgs e)
    {
        var selectedTime = timePicker.Time;
        MessagingCenter.Send(this, "TimeSelected", selectedTime);

    }
}
