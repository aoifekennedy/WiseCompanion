using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using WiseCompanion.Models;

namespace WiseCompanion;

public partial class MedicationPage : ContentPage
{
    public ObservableCollection<Medication> Medications { get; } = new ObservableCollection<Medication>();

    public MedicationPage()
    {
        InitializeComponent();
        medicationList.ItemsSource = Medications;

        //LoadInitialMedications();

        MessagingCenter.Subscribe<MedicationSearch, Medication>(this, "AddMedication", (sender, arg) =>
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Medications.Add(arg); 
            });
        });
    }

 /*   private void LoadInitialMedications()
    {
        Medications.Add(new Medication { Name = "Aspirin", Dosage = "100mg" });
        Medications.Add(new Medication { Name = "Ibuprofen", Dosage = "200mg" });
    }*/

    private async void OnAddMedicationClicked(object sender, EventArgs e)
    {
        var medicationSearch = new MedicationSearch();
        await Navigation.PushModalAsync(medicationSearch);
    }


    private void OnRemoveMedicationClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var medicationToRemove = button?.BindingContext as Medication;
        if (medicationToRemove != null)
        {
            Medications.Remove(medicationToRemove);
        }
    }
}
