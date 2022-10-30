﻿using AirBnb.Data;
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
            var listings = _set.AsNoTracking();

            if (filterOptions != null)
            {
                if (filterOptions.Neighbourhood != null)
                {
                    listings = listings.Where(x => x.NeighbourhoodCleansed == filterOptions.Neighbourhood);
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
                if (filterOptions.Limit > 0)
                {
                    listings = listings.Take(filterOptions.Limit);
                }
            }

            var geoDatas = new List<GeoData>();
            foreach (var item in listings
                .Select(l => new { l.Id, l.RoomType, l.Latitude, l.Longitude }))
            {
                var geoData = new GeoData
                {
                    Properties = new Properties
                    {
                        RoomType = item.RoomType,
                        ListingId = item.Id,
                    }
                };
                geoData.Geometry.Coordinates.Add(item.Longitude.ToString().Replace(",", "."));
                geoData.Geometry.Coordinates.Add(item.Latitude.ToString().Replace(",", "."));
                geoDatas.Add(geoData);
            }
            return geoDatas;
        }

        public async Task<Properties?> GetListingGeoDataById(int id)
        {
            return await _set.AsNoTracking().Where(l => l.Id == id).Select(l => new Properties
            {
                ListingId = l.Id,
                RoomType = l.RoomType,
                Listing_url = l.ListingUrl,
                Name = l.Name,
                HostId = l.HostId,
                HostName = l.HostName,
                Neighbourhood = l.NeighbourhoodCleansed,
                NumberOfReviews = l.NumberOfReviews ?? default,
                MinimunNights = l.MinimumNights ?? default,
                Price = l.Price ?? default,
            }).FirstOrDefaultAsync();
        }

        public async Task<PaginatedList<ListingViewModel>> GetListingViewModels(int? pageIndex)
        {
            return await PaginatedList<ListingViewModel>.CreateAsync(_set.Select(x => new ListingViewModel
            {
                Id = x.Id,
                Name = x.Name,
                HostId = x.HostId,
                HostName = x.HostName,
                NeighbourhoodCleansed = x.NeighbourhoodCleansed,
                PropertyType = x.PropertyType,
                RoomType = x.RoomType,
                Price = x.Price,
                MinimumNights = x.MinimumNights,
                MaximumNights = x.MaximumNights
            }).AsNoTracking(), pageIndex ?? 1, 500);
        }
    }
}
