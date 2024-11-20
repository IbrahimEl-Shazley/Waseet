using GeoCoordinatePortable;
using System.Globalization;

namespace Wasit.Core.Helpers.General
{
    public static class GeneralHelper
    {
        public static double GetDistance(string sLatitude, string sLongitude, string eLatitude, string eLongitude)
        {
            try
            {
                var sLat = Convert.ToDouble(sLatitude, CultureInfo.InvariantCulture);
                var sLng = Convert.ToDouble(sLongitude, CultureInfo.InvariantCulture);
                var eLat = Convert.ToDouble(eLatitude, CultureInfo.InvariantCulture);
                var eLng = Convert.ToDouble(eLongitude, CultureInfo.InvariantCulture);

                var startCoordinate = new GeoCoordinate(sLat, sLng);
                var endCoordinate = new GeoCoordinate(eLat, eLng);

                double distanceInKm = startCoordinate.GetDistanceTo(endCoordinate) / 1000.0;
                return Math.Round(distanceInKm, 1, MidpointRounding.ToEven);
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

    }
}
