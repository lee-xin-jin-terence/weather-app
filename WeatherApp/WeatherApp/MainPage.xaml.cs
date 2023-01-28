using System;
using Xamarin.Forms;
using WeatherApp.SecretData;
using WeatherApp.Models;

namespace WeatherApp
{
    /// <summary>
    /// Displays weather data of various cities, based on the
    /// user selection
    /// 
    /// Author: Terence Lee
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Load the page with singapore selected as initial location.
        /// 
        /// Also load and display the weather data for Singapore.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            SetSingaporeAsLocationPickerInitialLocation();


            OpenWeatherMapFacade.Location initialLocation =
                                OpenWeatherMapFacade.Location.Singapore;
            RetrieveAndDisplayWeatherData(initialLocation);

        }


        /// <summary>
        /// Sets singapore as location picker initial location.
        /// 
        /// Have to do this instead of setting it in the XAML as
        /// there is a bug on XAML that prevents me from setting it
        /// using XAML.
        /// </summary>
        private void SetSingaporeAsLocationPickerInitialLocation()
        {
            const int SingaporePickerIndex = 0;

            this.locationPicker.SelectedIndex = SingaporePickerIndex;

        }





        /// <summary>
        /// Retrieves and display the weather data based on a selected location
        /// </summary>
        /// 
        /// <param name="location">The location representing the
        ///     location of interest</param>
        private async void RetrieveAndDisplayWeatherData(OpenWeatherMapFacade.Location location)
        {
            DisplayLoadingText();

            Weather currentWeather =
                await OpenWeatherMapFacade.GetCurrentWeather(Keys.OpenWeatherMapApiKey, location);

            DisplayWeatherData(currentWeather);

            DisplayWeatherDataLastUpdatedDateTime();

        }


        /// <summary>
        /// The event handler for the event when the location picker
        /// selected index is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void locationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveAndDisplayWeatherDataFromLocationPicker();
        }


        /// <summary>
        /// The event handler for the event when the refresh weather 
        /// data button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshButton_Clicked(object sender, System.EventArgs e)
        {
            RetrieveAndDisplayWeatherDataFromLocationPicker();
        }




        /// <summary>
        /// Retrieves the weather location specified by the location picker,
        /// then retrieves and updates the weather data
        /// </summary>
        private void RetrieveAndDisplayWeatherDataFromLocationPicker()
        {

            string selectedLocationString = this.locationPicker.SelectedItem as string;

            OpenWeatherMapFacade.Location location =
                                        ConvertLocationStringToEnum(selectedLocationString);

            RetrieveAndDisplayWeatherData(location);
        }



        /// <summary>
        /// Display the weather data based on the Weather instance
        /// </summary>
        /// 
        /// <param name="weather">instance of Weather contains the weather
        ///             data to be displayed</param>
        void DisplayWeatherData(Weather weather)
        {
            this.temperatureLabel.Text = weather.TemperatureCelsius.ToString() + " °C";

            this.visibilityLabel.Text = string.Format("{0:0.0} km", 
                                            (weather.VisibilityMeters / 1000.0));

            this.humidityLabel.Text = 
                    weather.HumidityPercentage.ToString() + " %";

            this.windSpeedLabel.Text = 
                    weather.WindSpeedMetersPerSecond.ToString() + " m/s";

            this.atmosphericPressureLabel.Text = 
                    weather.AtmosphericPressureHectopascal.ToString() + " hPa";
        
        }


        /// <summary>
        /// Display the text "Loading..." for all various labels that
        /// are supposed to display weather data values
        /// </summary>
        private void DisplayLoadingText()
        {
            const string LoadingText = "Loading...";

            this.temperatureLabel.Text = LoadingText;
            this.visibilityLabel.Text = LoadingText;
            this.humidityLabel.Text = LoadingText;
            this.windSpeedLabel.Text = LoadingText;
            this.atmosphericPressureLabel.Text = LoadingText;

        }



        /// <summary>
        /// Converts a string representing a location into a Location enum
        /// from the OpenWeatherMapFacade class
        /// </summary>
        /// 
        /// <param name="locationString">a string representing a location</param>
        /// <returns></returns>
        private OpenWeatherMapFacade.Location ConvertLocationStringToEnum(
                                                            string locationString) 
        {
            OpenWeatherMapFacade.Location locationEnum = OpenWeatherMapFacade.Location.Singapore;

            switch (locationString)
            {
                case "Singapore":
                    locationEnum = OpenWeatherMapFacade.Location.Singapore;
                    break;

                case "Kuala Lumpur":
                    locationEnum = OpenWeatherMapFacade.Location.KualaLumpur;
                    break;

                case "Bangkok":
                    locationEnum = OpenWeatherMapFacade.Location.Bangkok;
                    break;


                case "Jakarta":
                    locationEnum = OpenWeatherMapFacade.Location.Jakarta;
                    break;


                case "Hanoi":
                    locationEnum = OpenWeatherMapFacade.Location.Hanoi;
                    break;
            }

            return locationEnum;
        }


        /// <summary>
        /// Displays the current date time as the date time when the
        /// weather data is last updated
        /// </summary>
        private void DisplayWeatherDataLastUpdatedDateTime()
        {
            this.dataLastUpdatedOnLabel.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        }



  
    }
}
