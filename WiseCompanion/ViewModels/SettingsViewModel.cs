using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using ServiceReference1;
using Microsoft.Maui.Dispatching;
using System.Xml.Linq;
using Xamarin.KotlinX.Coroutines;
using System.Collections.ObjectModel;

namespace WiseCompanion.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string _selectedBackgroundColor;
        private string _selectedFontFamily;
        private ObservableCollection<string> _fontFamilies;

        public event PropertyChangedEventHandler PropertyChanged;

        private IDispatcher _dispatcher;

        public ObservableCollection<string> FontFamilies
        {
            get => _fontFamilies;
            private set
            {
                _fontFamilies = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            FontFamilies = new ObservableCollection<string>
        {
            "Arial", "Times New Roman", "Caveat"
        };

            LoadSettings();
        }

        public string SelectedBackgroundColor
        {
            get => _selectedBackgroundColor;
            set
            {
                if (_selectedBackgroundColor != value)
                {
                    _selectedBackgroundColor = value;
                    OnPropertyChanged();
                    SaveSettings();  
                }
            }
        }

        public string SelectedFontFamily
        {
            get => _selectedFontFamily;
            set
            {
                if (_selectedFontFamily != value)
                {
                    _selectedFontFamily = value;
                    OnPropertyChanged();
                    SaveSettings();

                    MessagingCenter.Send(this, "ResetFontPicker");
                }
            }
        }

        public async Task LoadSettings()
        {
            try
            {
                var client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
                var emailAddress = WiseCompanion.Global.EmailAddress;

                    Debug.WriteLine($"Attempting to load settings for: {emailAddress}");

                var xmlResult = await client.GetSettingsAsync(emailAddress);
                client.Close();

                Debug.WriteLine($"Received XML: {xmlResult.OuterXml}");

                if (xmlResult != null && !string.IsNullOrEmpty(xmlResult.OuterXml))
                {
                    ParseSettingsFromXml(xmlResult.OuterXml);
                }
                else
                {
                    Debug.WriteLine("No XML result or XML is empty.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading settings: {ex.Message}");
            }
        }


        private void ParseSettingsFromXml(string xmlData)
        {
            try
            {
                var doc = XDocument.Parse(xmlData);
                var backgroundColour = doc.Descendants("BackgroundColour").FirstOrDefault()?.Value;
                var fontFamily = doc.Descendants("FontFamily").FirstOrDefault()?.Value;

                if (!string.IsNullOrEmpty(backgroundColour) && !string.IsNullOrEmpty(fontFamily))
                {
                    _dispatcher.Dispatch(() =>
                    {
                        SelectedBackgroundColor = backgroundColour;
                        SelectedFontFamily = fontFamily;
                    });
                    Debug.WriteLine($"Settings loaded successfully: BackgroundColor={backgroundColour}, FontFamily={fontFamily}");
                }
                else
                {
                    Debug.WriteLine("No settings found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing XML: {ex.Message}");
            }
        }

        public async void SaveSettings()
        {
            try
            {
                using (var client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE))
                {
                    bool success = await client.SaveSettingsAsync(WiseCompanion.Global.EmailAddress, SelectedFontFamily, 12, SelectedBackgroundColor);

                    if (success)
                    {
                        Debug.WriteLine("Settings saved successfully.");
                    }
                    else
                    {
                        Debug.WriteLine("Failed to save settings.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving settings: {ex.Message}");
            }
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

    //public async void LoadSettingsAsync()
    //{
    //    try
    //    {
    //        var client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
    //        var XMLResult = await client.GetSettingsAsync(Global.EmailAddress);
    //        client.Close();

    //        if (XMLResult != null && !string.IsNullOrEmpty(XMLResult.OuterXml))
    //        {
    //            var doc = System.Xml.Linq.XDocument.Parse(XMLResult.OuterXml);
    //            var settingsElement = doc.Descendants("Setting").FirstOrDefault();

    //            if (settingsElement != null)
    //            {
    //                _dispatcher.Dispatch(() =>
    //                {
    //                    SelectedBackgroundColor = settingsElement.Element("BackgroundColour")?.Value;
    //                    SelectedFontFamily = settingsElement.Element("FontFamily")?.Value;

    //                });
    //                Debug.WriteLine("Settings loaded successfully.");
    //            }
    //            else
    //            {
    //                Debug.WriteLine("No settings found.");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine($"Error loading settings: {ex.Message}");
    //    }
    //}

//    private async Task LoadMedications()
//        {
//            try
//            {
//                KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
//                var emailAddress = WiseCompanion.Global.EmailAddress;

//                var xmlResult = await client.GetMedicationAsync(emailAddress);
//                ParseMedicationsFromXml(xmlResult.OuterXml);

//                client.Close();
//            }
//            catch (Exception ex)
//            {
//                await DisplayAlert("Error", "Failed to load medications: " + ex.Message, "OK");
//            }
//        }

//        private void ParseMedicationsFromXml(string xmlData)
//        {
//            try
//            {
//                Medications.Clear();
//                var doc = System.Xml.Linq.XDocument.Parse(xmlData);
//                foreach (var element in doc.Descendants("Medication"))
//                {
//                    var medication = new Medication
//                    {
//                        MedicationDescription = element.Element("MedicationDescription")?.Value,
//                        Dosage = element.Element("Dosage")?.Value,
//                        Time = element.Element("Time")?.Value
//                    };
//                    Medications.Add(medication);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error parsing XML: {ex.Message}");
//            }
//        }

//        private async void SaveSettingsAsync()
//        {
//            try
//            {
//                using (var client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE))
//                {
//                    bool success = await client.SaveSettingsAsync(Global.EmailAddress, SelectedFontFamily, 12, SelectedBackgroundColor);
//                    if (success)
//                    {
//                        Debug.WriteLine("Settings saved successfully.");
//                    }
//                    else
//                    {
//                        Debug.WriteLine("Failed to save settings.");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine($"Error saving settings: {ex.Message}");
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            Debug.WriteLine($"Property changed: {propertyName}");
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}

//using System.ComponentModel;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using ServiceReference1;

//namespace WiseCompanion.ViewModels;

//public class SettingsViewModel : INotifyPropertyChanged
//{
//    private string _selectedBackgroundColor;
//    private string _selectedFontFamily;
//    private INavigation _navigation;

//    public SettingsViewModel(INavigation navigation)
//    {
//        _navigation = navigation;
//        LoadSettingsAsync(); 
//    }

//    public List<string> FontFamilies { get; } = new List<string>
//    {
//        "Arial", "Times New Roman", "Caveat"
//    };

//    public string SelectedBackgroundColor
//    {
//        get => _selectedBackgroundColor;
//        set
//        {
//            if (_selectedBackgroundColor != value)
//            {
//                _selectedBackgroundColor = value;
//                OnPropertyChanged();
//                Global.BackgroundColor = value; 
//                SaveSettingsAsync();
//            }
//        }
//    }

//    public string SelectedFontFamily
//    {
//        get => _selectedFontFamily;
//        set
//        {
//            if (_selectedFontFamily != value)
//            {
//                _selectedFontFamily = value;
//                OnPropertyChanged();
//                Global.FontFamily = value; 
//                SaveSettingsAsync();
//            }
//        }
//    }


//    // Change the access modifier to public
//    public async void LoadSettingsAsync()
//    {
//        try
//        {
//            KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
//            var XMLResult = await client.GetSettingsAsync(Global.EmailAddress);
//            client.Close();

//            if (XMLResult != null && !string.IsNullOrEmpty(XMLResult.OuterXml))
//            {
//                var doc = System.Xml.Linq.XDocument.Parse(XMLResult.OuterXml);
//                var settingsElement = doc.Descendants("Setting").FirstOrDefault(); 

//                if (settingsElement != null)
//                {
//                    SelectedBackgroundColor = settingsElement.Element("BackgroundColour")?.Value;
//                    SelectedFontFamily = settingsElement.Element("FontFamily")?.Value;
//                    Debug.WriteLine("Settings loaded successfully.");
//                }
//                else
//                {
//                    Debug.WriteLine("No settings found.");
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error loading settings: {ex.Message}");
//        }
//    }

//    private async void SaveSettingsAsync()
//    {
//        try
//        {
//            using (var client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE))
//            {
//                bool success = await client.SaveSettingsAsync(Global.EmailAddress, SelectedFontFamily, 12 , SelectedBackgroundColor);

//                if (success)
//                {
//                    Debug.WriteLine("Settings saved successfully.");
//                }
//                else
//                {
//                    Debug.WriteLine("Failed to save settings.");
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error saving settings: {ex.Message}");
//        }
//    }

//    public event PropertyChangedEventHandler PropertyChanged;

//    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}

