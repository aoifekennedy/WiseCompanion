using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using ServiceReference1;
using Microsoft.Maui.Dispatching;
using System.Xml.Linq;
//using Xamarin.KotlinX.Coroutines;
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
            "Arial", "Calibri", "Open Sans", "Helvetica", "Georgia", "Times New Roman", "Proxima Nova"
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

                   // MessagingCenter.Send(this, "ResetFontPicker");
                }
            }
        }

        public async Task LoadSettings()
        {
            try
            {
                using KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
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

   
