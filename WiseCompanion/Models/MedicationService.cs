using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using CsvHelper;
using System.Globalization;
using WiseCompanion.Models;

public static class MedicationService
{
    private static ObservableCollection<Medication> _medications = new();

    public static ObservableCollection<Medication> Medications => _medications;

    static MedicationService()
    {
        LoadMedicationsFromCsv();
    }

    public static ObservableCollection<Medication> LoadMedicationsFromCsv()
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MedicationService)).Assembly;
        using var stream = assembly.GetManifestResourceStream("WiseCompanion.Assets.medications.csv");
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // Directly return the collection filled with records from the CSV
        return new ObservableCollection<Medication>(csv.GetRecords<Medication>());
    }



    public static void AddMedication(Medication medication)
    {
        _medications.Add(medication);
    }

    public static void RemoveMedication(Medication medication)
    {
        _medications.Remove(medication);
    }
}
