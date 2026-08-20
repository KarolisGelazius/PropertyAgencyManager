using System;

namespace U4_18.Classes {
    /// <summary>
    /// Base abstract class for real estate objects
    /// </summary>
    public abstract class RealEstate : IComparable<RealEstate>, IEquatable<RealEstate> {
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }

        /// <summary>
        /// Initializes a new instance of the RealEstate class
        /// </summary>
        /// <param name="city">City of the object</param>
        /// <param name="district">District of the object</param>
        /// <param name="street">Street name</param>
        /// <param name="number">Building or flat number</param>
        /// <param name="type">Property type</param>
        /// <param name="year">Construction year</param>
        /// <param name="area">Total area in square meters</param>
        /// <param name="rooms">Number of rooms</param>
        protected RealEstate(string city, string district, string street,
            string number, string type, int year, double area, int rooms) {
            City = city;
            District = district;
            Street = street;
            Number = number;
            Type = type;
            Year = year;
            Area = area;
            Rooms = rooms;
        }

        /// <summary>
        /// Determines if the real estate object is considered large based on criteria
        /// </summary>
        /// <returns>True if large, otherwise false</returns>
        public abstract bool IsLarge();

        /// <summary>
        /// Compares two real estate objects based on their total area
        /// </summary>
        /// <param name="other">The other real estate object to compare with</param>
        /// <returns>A value indicating the relative order based on area</returns>
        public int CompareTo(RealEstate other) {
            if (other == null) return 1;
            return Area.CompareTo(other.Area);
        }

        /// <summary>
        /// Checks if this real estate object is equal to another based on address details
        /// </summary>
        /// <param name="other">The other real estate object to check</param>
        /// <returns>True if address details match, false otherwise</returns>
        public bool Equals(RealEstate other) {
            if (other == null) return false;
            return City == other.City &&
                   District == other.District &&
                   Street == other.Street &&
                   Number == other.Number;
        }

        /// <summary>
        /// Returns a formatted CSV string representation of the object data
        /// </summary>
        /// <returns>Semicolon-separated string of properties</returns>
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6:f2};{7}",
                City, District, Street, Number, Type, Year, Area, Rooms);
        }
    }
}