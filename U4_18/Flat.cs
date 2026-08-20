namespace U4_18.Classes
{
    /// <summary>
    /// Represents a flat (apartment) real estate object
    /// </summary>
    public class Flat : RealEstate {
        /// <summary>
        /// The floor number where the flat is located
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Initializes a new instance of the Flat class
        /// </summary>
        /// <param name="city">City of the flat</param>
        /// <param name="district">District of the flat</param>
        /// <param name="street">Street name</param>
        /// <param name="number">Flat number</param>
        /// <param name="type">Property type (e.g., Brick, Block)</param>
        /// <param name="year">Construction year</param>
        /// <param name="area">Total area in square meters</param>
        /// <param name="rooms">Number of rooms</param>
        /// <param name="floor">Floor level</param>
        public Flat(string city, string district, string street,
            string number, string type, int year, double area,
            int rooms, int floor)
            : base(city, district, street, number, type, year, area, rooms) {
            Floor = floor;
        }

        /// <summary>
        /// Checks if the flat is considered large (area greater than 90 square meters)
        /// </summary>
        /// <returns>True if area > 90, otherwise false</returns>
        public override bool IsLarge() => Area > 90;

        /// <summary>
        /// Returns a formatted CSV string representation of the flat data
        /// </summary>
        /// <returns>Semicolon-separated string including base data and floor</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format(";{0}", Floor);
        }
    }
}