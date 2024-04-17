
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using WiseCompanion.Models;
using ServiceReference1;
//using Windows.Security.EnterpriseData;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Xml.Linq;
//using static Java.Util.Jar.Attributes;

namespace WiseCompanion;

public partial class MedicationSearch : ContentPage
{
    //private ObservableCollection<Medication> allMedications;
    //public ObservableCollection<Medication> FilteredMedications { get; } = new ObservableCollection<Medication>();

    private bool ListOfMedicationLoaded=false;

    public MedicationSearch()
    {
        InitializeComponent();
        //allMedications = MedicationService.LoadMedicationsFromCsv();
        //searchResultsListView.ItemsSource = FilteredMedications;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (ListOfMedicationLoaded==false)
        {
            await PopulateListOfMedications();
            ListOfMedicationLoaded = true;
        }

        //EntryQuantity.Focus();
    }

    private async Task PopulateListOfMedications()
    {
        try
        {
            using KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
            var xmlResult = await client.GetMedicationListAsync();
            client.Close();

            var doc = System.Xml.Linq.XDocument.Parse(xmlResult.OuterXml);
            string[] ar = new string[doc.Descendants("Medication").Count()];
            int i=1;

            foreach (var element in doc.Descendants("Medication"))
            {
                ar[i-1] = element.Element("MedicationDescription")?.Value.ToString();
                i++;
            }
            PickerMedication.ItemsSource = ar;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to load list of medications: " + ex.Message, "OK");
        }
    }

    //private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    //{
    //    var searchText = e.NewTextValue?.ToLower() ?? string.Empty;
    //    var filtered = allMedications.Where(m => m.Name.ToLower().Contains(searchText)).ToList();

    //    MainThread.BeginInvokeOnMainThread(() =>
    //    {
    //        FilteredMedications.Clear();
    //        foreach (var medication in filtered)
    //        {
    //            FilteredMedications.Add(medication);
    //        }
    //    });
    //}

    //private async void OnSearchResultSelected(object sender, SelectedItemChangedEventArgs e)
    //{
    //    if (e.SelectedItem is Medication selectedMedication)
    //    {
    //        ((ListView)sender).SelectedItem = null;

    //        TimeSpan? selectedTime = await DisplayTimePicker();
    //        if (selectedTime.HasValue)
    //        {
    //            selectedMedication.Time = selectedTime.Value.ToString(@"hh\:mm");

    //            bool result = await AddMedicationToDatabase(selectedMedication);
    //            if (result)
    //            {
    //                MessagingCenter.Send(this, "AddMedication", selectedMedication);
    //                await DisplayAlert("Success", "Medication added to database.", "OK");
    //            }
    //            else
    //            {
    //                await DisplayAlert("Failure", "Failed to add medication to database.", "OK");
    //            }
    //        }

    //        await Shell.Current.GoToAsync("..");
    //    }
    //}

    private async Task<bool> AddMedicationToDatabase()
    {
        var client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
        try
        {
            var result = await client.AddMedicationAsync(WiseCompanion.Global.EmailAddress, PickerMedication.SelectedItem.ToString(), int.Parse(EntryQuantity.Text.ToString()), TimePicker.Time.ToString() );
            client.Close();
            return true;

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error adding medication: " + ex.Message, "OK");
            return false;
        }
    }

    //private async Task<TimeSpan?> DisplayTimePicker()
    //{
    //    var timePickerPage = new TimePickerPage();
    //    await Shell.Current.Navigation.PushModalAsync(timePickerPage);

    //    var completionSource = new TaskCompletionSource<TimeSpan?>();
    //    MessagingCenter.Subscribe<TimePicker, TimeSpan>(this, "TimeSelected", (sender, arg) =>
    //    {
    //        completionSource.SetResult(arg);
    //        MessagingCenter.Unsubscribe<TimePicker, TimeSpan>(this, "TimeSelected");
    //    });

    //    var selectedTime = await completionSource.Task;
    //    await Shell.Current.Navigation.PopModalAsync();
    //    return selectedTime;
    //}


    private async void BackButtonClicked(object sender, EventArgs e)
    {
        var homePage = new MedicationPage();
        await Navigation.PushModalAsync(homePage);
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        if (PickerMedication.SelectedIndex == -1 || string.IsNullOrWhiteSpace(EntryQuantity.Text))
        {
            await DisplayAlert("Error", "Please fill in every entry!", "OK");
            return;
        }

        int entryQuantity;
        if (!int.TryParse(EntryQuantity.Text, out entryQuantity) || entryQuantity <= 0)
        {
            await DisplayAlert("Error", "Please enter a valid quantity!", "OK");
            return;
        }

        var pickerMedication = PickerMedication.SelectedItem.ToString();
        var timePickertext = TimePicker.Time.ToString();

        bool addedSuccessfully = await AddMedicationToDatabase();
        if (addedSuccessfully)
        {
            PickerMedication.SelectedIndex = -1; 
            EntryQuantity.Text = string.Empty;    
            TimePicker.Time = TimeSpan.Zero;

        }
        else
        {
            await DisplayAlert("Error", "Failed to add medication.", "OK");
        }

        //PickerMedication.Focus();
    }


}


