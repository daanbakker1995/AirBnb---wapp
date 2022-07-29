using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace AirBnb.Models
{
    public enum RoomType
    {
        PrivateRoom,
        EntireHomeApt,
        SharedRoom,
        HotelRoom
    }

    public static class RoomTypeExtensions
    {
        public static string GetRoomTypeDBName(RoomType room)
        {
            switch (room)
            {
                case RoomType.PrivateRoom:
                    return "Private room";
                case RoomType.SharedRoom:
                    return "Shared room";
                case RoomType.HotelRoom:
                    return "Hotel room";
                case RoomType.EntireHomeApt:
                    return "Entire home/apt";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
