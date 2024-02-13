namespace WiseCompanion;

using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using WiseCompanion.Models;
using CsvHelper;

public partial class MedicationSearch : ContentPage
{
    private ObservableCollection<Medication> allMedications;
    public ObservableCollection<Medication> FilteredMedications { get; } = new ObservableCollection<Medication>();

    public MedicationSearch()
    {
        InitializeComponent();
        allMedications = MedicationService.LoadMedicationsFromCsv(); 
        searchResultsListView.ItemsSource = FilteredMedications;
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.ToLower() ?? string.Empty;
        var filtered = allMedications.Where(m => m.Name.ToLower().Contains(searchText)).ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            FilteredMedications.Clear();
            foreach (var medication in filtered)
            {
                FilteredMedications.Add(medication);
            }
        });
    }


    private async void OnSearchResultSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Medication selectedMedication)
        {
            ((ListView)sender).SelectedItem = null; 

            TimeSpan? selectedTime = await DisplayTimePicker();
            if (selectedTime.HasValue)
            {
                
                selectedMedication.Time = selectedTime.Value.ToString(@"hh\:mm"); 

                
                MessagingCenter.Send(this, "AddMedication", selectedMedication);
            }

            
            await Shell.Current.GoToAsync("..");
        }
    }



    private async Task<TimeSpan?> DisplayTimePicker()
    {
        var timePickerPage = new TimePickerPage(); 
        await Shell.Current.Navigation.PushModalAsync(timePickerPage);

        var completionSource = new TaskCompletionSource<TimeSpan?>();
        MessagingCenter.Subscribe<TimePickerPage, TimeSpan>(this, "TimeSelected", (sender, arg) =>
        {
            completionSource.SetResult(arg);
            MessagingCenter.Unsubscribe<TimePickerPage, TimeSpan>(this, "TimeSelected");
        });

        var selectedTime = await completionSource.Task;
        await Shell.Current.Navigation.PopModalAsync(); 
        return selectedTime;
    }


}
