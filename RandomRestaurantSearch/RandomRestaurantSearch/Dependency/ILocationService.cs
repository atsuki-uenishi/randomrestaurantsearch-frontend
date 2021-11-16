using RandomRestaurantSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomRestaurantSearch.Dependency
{
    public delegate void OnLocationChangedDelegate(GeoCoordinate location);
    public delegate void OnLocationErrorDelegate(string error);
    public interface ILocationService
    {
        void Initialize();

        Task<GeoCoordinate> GetGeoCordinateAsync(int timeout);

        void StartListening(int minTime, double minDistance, bool includeHeading = false);

        event OnLocationChangedDelegate OnLocationChanged;
        event OnLocationErrorDelegate OnLocationError;
    }
}
