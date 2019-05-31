

namespace WeatherSysTray0 {
    public static class Extensions {
        public const double absoluteZeroDelta = 273.15;
        public static double KelvinToCelsius(this double kelvin) {
            return kelvin - absoluteZeroDelta;
        }

        public static double KelvinToFahrenheit(this double kelvin) {
            return (kelvin - absoluteZeroDelta) * (9 / 5) + 32;
        }

        public static double CelsiusToFahrenheit(this double celsius) {
            return celsius * (9 / 5) + 32; 
        }

        public static double FahrenheitToCelsius(this double fahrenheit) {
            return (fahrenheit - 32) * (5 / 9);
        }
    }
}
