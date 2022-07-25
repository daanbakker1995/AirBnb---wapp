using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Repository
{
    public class ListingsRepository : BaseRepository<Listing>, IListingsRepository
    {
        public ListingsRepository(AirbnbV2Context context) : base(context)
        {
        }

        public async Task<List<GeoData>> GetListingsGeoData(ListingsFilterOptions filterOptions = null)
        {
            var listings = _set.AsNoTracking(); // TODO: remove take

            if (filterOptions != null)
            {
                if (filterOptions.Neighbourhood != null)
                {
                    listings = listings.Where(x => x.Neighbourhood == filterOptions.Neighbourhood);
                }
                if (filterOptions.RoomType != null)
                {
                    listings = listings.Where(x => x.RoomType == filterOptions.RoomType);
                }
                if (filterOptions.MinPrice > 0)
                {
                    listings = listings.Where(x => x.Price > filterOptions.MinPrice);
                }
                if (filterOptions.MaxPrice > 0)
                {
                    listings = listings.Where(x => x.Price < filterOptions.MaxPrice);
                }
                if (filterOptions.MinReviews > 0)
                {
                    listings = listings.Where(x => x.NumberOfReviews > filterOptions.MinReviews);
                }
                if (filterOptions.MaxReviews > 0)
                {
                    listings = listings.Where(x => x.NumberOfReviews < filterOptions.MaxReviews);
                }
            }

            var geoDatas = new List<GeoData>();
            foreach (var item in await listings.Take(100)
                .Select(l => new { l.Id, l.Name, l.ListingUrl, l.HostId, l.HostName, l.Neighbourhood, l.RoomType, l.NumberOfReviews, l.MinimumNights, l.Price, l.License, l.Latitude,l.Longitude })
                .ToListAsync())
            {
                var geoData = new GeoData
                {
                    Properties = new Properties
                    {
                        ListingId = item.Id,
                        Listing_url = item.ListingUrl,
                        Name = item.Name,
                        HostId = item.HostId,
                        HostName = item.HostName,
                        Neighbourhood = item.Neighbourhood,
                        RoomType = item.RoomType,
                        NumberOfReviews = item.NumberOfReviews ?? default,
                        MinimunNights = item.MinimumNights ?? default,
                        Price = item.Price ?? default,
                        HasLicense = item.License is not null,
                    }
                };
                geoData.Geometry.Coordinates.Add(item.Longitude.ToString().Replace(",", "."));
                geoData.Geometry.Coordinates.Add(item.Latitude.ToString().Replace(",", "."));
                geoDatas.Add(geoData);
            }
            return geoDatas;
        }
    }
}
