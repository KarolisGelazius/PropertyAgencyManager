namespace U4_18.Classes
{
    /// <summary>
    /// Represents a house real estate object
    /// </summary>
    public class House : RealEstate {
        /// <summary>
        /// Type of heating system installed in the house
        /// </summary>
        public string Heating { get; set; }

        //// <summary>
        /// Initializes a new instance of the House class
        /// </summary>
        /// <param name="city">City of the house</param>
        /// <param name="district">District of the house</param>
        /// <param name="street">Street name</param>
        /// <param name="number">House number</param>
        /// <param name="type">Property type</param>
        /// <param name="year">Construction year</param>
        /// <param name="area">Total area in square meters</param>
        /// <param name="rooms">Number of rooms</param>
        /// <param name="heating">Heating system type</param>
        public House(string city, string district, string street,
            string number, string type, int year, double area,
            int rooms, string heating)
            : base(city, district, street, number, type, year, area, rooms) {
            Heating = heating;
        }

        /// <summary>
        /// Checks if the house is considered large (area greater than 200 square meters)
        /// </summary>
        /// <returns>True if area > 200, otherwise false</returns>
        public override bool IsLarge() => Area > 200;

        /// <summary>
        /// Returns a formatted CSV string representation of the house data
        /// </summary>
        /// <returns>Semicolon-separated string including base data and heating type</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format(";{0}", Heating);
        }
    }
}