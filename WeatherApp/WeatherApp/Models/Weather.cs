
namespace WeatherApp.Models
{
    /// <summary>
    /// A simple model class representing the weather.
    /// Contains simple getters and setters
    /// 
    /// Author: Terence Lee
    /// </summary>
    /// 
    internal class Weather
    {

        
        public float TemperatureCelsius
        {
            get; set;
        }


        public int HumidityPercentage 
        { 
            get; set; 
        }


        public float AtmosphericPressureHectopascal
        {
            get; set;
        }


        public float WindSpeedMetersPerSecond 
        {
            get; set;
        }


        public int VisibilityMeters
        {
            get; set;
        }

      
    }
}
