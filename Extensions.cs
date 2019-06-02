using System;

namespace WeatherSysTray0 {
    public static class Extensions {
        public const double absoluteZeroDelta = 273.15;
        public const string kelvinOutOfRangeMessage = "Kelvin is negative";
        public const string celsiusOutOfRangeMessage = "Celsius is out of range";
        public const string fahrenheitOutOfRangeMessage = "Fahrenheit out of range";

        public static double KelvinToCelsius(this double kelvin, int? rounding = null) {
            if (kelvin < 0)
                throw new ArgumentOutOfRangeException("Kelvin", kelvin, kelvinOutOfRangeMessage);
            return kelvin - absoluteZeroDelta;
        }

        public static double KelvinToFahrenheit(this double kelvin) {
            return (kelvin - absoluteZeroDelta) * (9.0 / 5.0) + 32.0;
        }

        public static double CelsiusToFahrenheit(this double celsius) {
            return celsius * (9.0 / 5.0) + 32.0; 
        }

        public static double CelsiusToKelvin(this double celsius) {
            return celsius + absoluteZeroDelta;
        }

        public static double FahrenheitToCelsius(this double fahrenheit) {
            return (fahrenheit - 32.0) * (5.0 / 9.0);
        }

        public static double FahrenheitToKelvin(this double fahrenheit) {
            return fahrenheit.FahrenheitToCelsius() + absoluteZeroDelta;
        }        
    }
}
