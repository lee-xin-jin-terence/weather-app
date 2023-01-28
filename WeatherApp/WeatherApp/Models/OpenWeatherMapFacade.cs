using OpenWeatherMap.Standard.Models;
using OpenWeatherMap.Standard;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    /// <summary>
    /// A class that wraps up the third-party OpenWeatherMap API
    /// classes (Facade design pattern) and expose only methods 
    /// used my this program
    /// 
    /// Example usage:
    ///     
    ///     const string OpenWeatherMapApiKey = "[some-key]";
    ///     
    ///     Weather weather = await 
    ///         OpenWeatherMapFacade.GetCurrentWeather(OpenWeatherMapApiKey,
    ///                                                 Location.Singapore);
    ///                                                 
    ///     Console.WriteLine("The current temperature is " + weather.Temperature);
    ///     ....
    ///     .....
    ///                                                 
    /// </summary>
    internal class OpenWeatherMapFacade
    {
        /// <summary>
        /// An enum containing all the locations supported in the app
        /// </summary>
        public enum Location
        {
            Singapore, KualaLumpur, Bangkok, Jakarta, Hanoi
        }


        /// <summary>
        /// Private constructor as no need for constructor
        /// </summary>
        private OpenWeatherMapFacade()
        {

        }



        /// <summary>
        /// Retrieves the current weather of a particular location using the
        /// OpenWeatherMap API
        /// 
        /// </summary>
        /// <param name="openWeatherMapApiKey">a string representing a 
        ///             a key required for making API cals to OpenWeatherMap servers</param>
        /// <param name="location">a enum representing the location of interest</param>
        /// <returns></returns>
        public static async Task<Weather> GetCurrentWeather(string openWeatherMapApiKey,
                                                                    Location location)
        {

            Current currentWeather = new Current(openWeatherMapApiKey);

            string[] cityNameAndCountryArray = ConvertLocationToCityNameAndCountryArray(location);

            string cityName = cityNameAndCountryArray[0];
            string countryName = cityNameAndCountryArray[1];

            WeatherData weatherData = await currentWeather.
                            GetWeatherDataByCityNameAsync(cityName, countryName);

            Weather weather = ConvertWeatherDataToWeather(weatherData);

            return weather;

        }


        /// <summary>
        /// Convert a Location enum (defined by this class) into a 
        /// a string arrow containing the city name and country name.
        /// 
        /// This city name and country name will be used as parameters
        /// for when making calls using the external library API 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private static string[] ConvertLocationToCityNameAndCountryArray(Location location) 
        {
            string[] arrayToReturn = null;

            switch (location) 
            { 
                case Location.Singapore:
                    arrayToReturn = new string[] { "Singapore", "SG" };
                    break;


                case Location.KualaLumpur:
                    arrayToReturn = new string[] { "Kuala Lumpur", "MY" };
                    break;


                case Location.Bangkok:
                    arrayToReturn = new string[] { "Bangkok", "TH" };
                    break;


                case Location.Jakarta:
                    arrayToReturn = new string[] { "Jakarta", "ID" };   
                    break;


                case Location.Hanoi:
                    arrayToReturn = new string[] { "Ha Noi", "VN" };
                    break;


            }

            return arrayToReturn;
        }



        /// <summary>
        /// Converts an instance of WeatherData (external library API 
        /// defined class) to an instance of Weather 
        /// (defined by this program)
        /// </summary>
        /// <param name="weatherData"></param>
        /// <returns></returns>
        private static Weather ConvertWeatherDataToWeather(WeatherData weatherData)
        {
            return new Weather() 
            { 
                TemperatureCelsius = weatherData.WeatherDayInfo.Temperature,
                HumidityPercentage = weatherData.WeatherDayInfo.Humidity,
                WindSpeedMetersPerSecond = weatherData.Wind.Speed,
                VisibilityMeters = weatherData.Visibility,
                AtmosphericPressureHectopascal = weatherData.WeatherDayInfo.Pressure

            };
        }
       


    }
}
