using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System;
using ServiceReference1;
using WiseCompanion.Models;
using System.Net.Mail;

namespace WiseCompanion;

public partial class MedicationPage : ContentPage
{
    public ObservableCollection<Medication> Medications { get; } = new ObservableCollection<Medication>();

    public MedicationPage()
    {
        InitializeComponent();
        medicationList.ItemsSource = Medications;
        LoadMedications();

        BtnAddMedication.IsVisible = WiseCompanion.Global.AdminYN;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMedications();
    }

    private async Task LoadMedications()
    {
        try
        {
            KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
            var emailAddress = WiseCompanion.Global.EmailAddress;

            var xmlResult = await client.GetMedicationAsync(emailAddress);
            ParseMedicationsFromXml(xmlResult.OuterXml);

            client.Close();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to load medications: " + ex.Message, "OK");
        }
    }

    private void ParseMedicationsFromXml(string xmlData)
    {
        try
        {
            Medications.Clear();
            var doc = System.Xml.Linq.XDocument.Parse(xmlData);
            foreach (var element in doc.Descendants("Medication"))
            {
                var medication = new Medication
                {
                    MedicationDescription = element.Element("MedicationDescription")?.Value,
                    Dosage = element.Element("Dosage")?.Value,
                    Time = element.Element("Time")?.Value
                };
                Medications.Add(medication);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing XML: {ex.Message}");
        }
    }

    private async void OnAddMedicationButtonClicked(object sender, EventArgs e)
    {
        var medicationSearch = new MedicationSearch();
        await Navigation.PushModalAsync(medicationSearch);
    }

    //private void OnRemoveMedicationClicked(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    var medicationToRemove = button?.BindingContext as Medication;
    //    KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);


    //    if (medicationToRemove != null)
    //    {
    //        Medications.Remove(medicationToRemove);
    //    }
    //}

    private async void OnRemoveMedicationClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            dynamic context = button.BindingContext;
            string medicationDescription = context.MedicationDescription;
            int dosage;
            if (!int.TryParse(context.Dosage, out dosage))
            {
                await DisplayAlert("Error", "Invalid dosage format", "OK");
                return;
            }
            string time = context.Time;

            bool success = await RemoveMedicationFromDatabase(WiseCompanion.Global.EmailAddress, medicationDescription, dosage, time);
            if (success)
            {
                // Remove the medication from the ObservableCollection
                Medications.Remove(context);
                await DisplayAlert("Success", "Medication deleted", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete medication", "OK");
            }
        }
    }


    private async Task<bool> RemoveMedicationFromDatabase(string emailAddress, string medicationDescription, int dosage, string time)
    {
        using (var client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE))
        {
            return await client.DeleteMedicationAsync(emailAddress, medicationDescription, dosage, time);
        }
    }


    private async void BackButtonClicked(object sender, EventArgs e)
    {
        var homePage = new HomePage();
        await Navigation.PushModalAsync(homePage);
    }
}

//using Microsoft.Maui.Controls;
//using System.Collections.ObjectModel;
//using System;
//using ServiceReference1;
//using WiseCompanion.Models;

//namespace WiseCompanion;

//public partial class MedicationPage : ContentPage
//{
//    public ObservableCollection<Medication> Medications { get; } = new ObservableCollection<Medication>();

//    public MedicationPage()
//    {
//        InitializeComponent();
//        medicationList.ItemsSource = Medications;
//        LoadInitialMedications();
//        // LoadMedications();
//    }

//    private void LoadInitialMedications()
//    {
//        Medications.Add(new Medication { Name = "Aspirin", Dosage = "100mg" });
//        Medications.Add(new Medication { Name = "Ibuprofen", Dosage = "200mg" });
//    }

//    //private async void LoadMedications()
//    //{
//    //    try
//    //    {
//    //        KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
//    //        var emailAddress = WiseCompanion.Global.EmailAddress;

//    //        var xmlResult = await client.GetMedicationAsync(emailAddress);
//    //        ParseMedicationsFromXml(xmlResult.OuterXml);

//    //        client.Close();
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        await DisplayAlert("Error", "Failed to load medications: " + ex.Message, "OK");
//    //    }
//    //}

//    //private void ParseMedicationsFromXml(string xmlData)
//    //{
//    //    try
//    //    {
//    //        var doc = System.Xml.Linq.XDocument.Parse(xmlData);
//    //        foreach (var element in doc.Descendants("Medication"))
//    //        {
//    //            var medication = new Medication
//    //            {
//    //                Name = element.Element("MedicationDescription")?.Value,
//    //                Dosage = element.Element("Dosage")?.Value,
//    //                Time = element.Element("Time")?.Value
//    //            };
//    //            Medications.Add(medication);
//    //        }
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Console.WriteLine($"Error parsing XML: {ex.Message}");
//    //    }
//    //}

//    private async void OnAddMedicationButtonClicked(object sender, EventArgs e)
//    {
//        var medicationSearch = new MedicationSearch();
//        await Navigation.PushModalAsync(medicationSearch);
//        // LoadMedications();
//    }

//    private void OnRemoveMedicationClicked(object sender, EventArgs e)
//    {
//        var button = sender as Button;
//        var medicationToRemove = button?.BindingContext as Medication;
//        if (medicationToRemove != null)
//        {
//            Medications.Remove(medicationToRemove);
//        }
//    }

//    private async void BackButtonClicked(object sender, EventArgs e)
//    {
//        var homePage = new HomePage();
//        await Navigation.PushModalAsync(homePage);
//    }
//}