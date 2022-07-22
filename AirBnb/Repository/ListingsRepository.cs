using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AirBnb.Repository
{
    public class ListingsRepository : BaseRepository<Listing>, IListingsRepository
    {
        public ListingsRepository(Airbnb2022Context context) : base(context)
        {
        }

        public async Task<List<Listing>> GetOverViewListingsDataAsync(ListingsFilterOptions options = null)
        {
            var listings = _set.Select(l => new Listing
            {
                Id = l.Id,
                Name = l.Name,
                HostName = l.HostName,
                Neighbourhood = l.Neighbourhood,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                RoomType = l.RoomType,
                Price = l.Price,
                NumberOfReviews = l.NumberOfReviews,
                Availability365 = l.Availability365
            });
            if (options != null)
            {
                listings = await FilterListings(listings, options);
            }
            return await listings.ToListAsync();
        }

        public async Task<List<GeoData>> GetListingsGeoData(ListingsFilterOptions filterOptions = null)
        {
            var listings = await FilterListings(_set.AsNoTracking(), filterOptions);

            var geoDatas = new List<GeoData>();
            foreach (var item in await listings.ToListAsync())
            {
                var geoData = new GeoData
                {
                    Properties = new Properties
                    {
                        ListingId = item.Id,
                        Name = item.Name,
                        HostId = item.HostId,
                        HostName = item.HostName,
                        Neighbourhood = item.Neighbourhood,
                        RoomType = item.RoomType,
                        NumberOfReviews = item.NumberOfReviews,
                        MinimunNights = item.MinimumNights,
                        Price = item.Price
                    }
                };
                if (item.License != null)
                {
                    geoData.Properties.HasLicense = true;
                }
                geoData.Geometry.Coordinates.Add(GetLongitudeCorrectValue(item.Longitude.ToString()));
                geoData.Geometry.Coordinates.Add(GetLatitudeCorrectValue(item.Latitude.ToString()));
                geoDatas.Add(geoData);
            }
            return geoDatas;
        }

        public async Task<IQueryable<Listing>> FilterListings(IQueryable<Listing> listings, ListingsFilterOptions filterOptions)
        {
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
            return listings;
        }

        private static string GetLatitudeCorrectValue(string lat)
        {
            return AddDotToString(lat, 1);
        }

        private static string GetLongitudeCorrectValue(string longitude)
        {
            return AddDotToString(longitude, 0);
        }

        private static string AddDotToString(string value, int afterPosition)
        {
            var stringbuilder = new StringBuilder();
            var characters = value.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                stringbuilder.Append(characters[i]);
                if (i == afterPosition)
                {
                    stringbuilder.Append('.');
                }
            }
            return stringbuilder.ToString();
        }
    }
}
