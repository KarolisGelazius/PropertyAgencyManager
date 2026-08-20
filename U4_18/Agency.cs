using System.Collections.Generic;

namespace U4_18.Classes {
    /// <summary>
    /// Represents a real estate agency that manages a collection of properties
    /// </summary>
    public class Agency {
        /// <summary>
        /// List of real estate objects belonging to the agency
        /// </summary>
        private List<RealEstate> items;

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// Initializes a new instance of the Agency class
        /// </summary>
        /// <param name="name">Name of the agency</param>
        /// <param name="address">Physical address of the agency</param>
        /// <param name="phone">Contact phone number</param>
        public Agency(string name, string address, string phone) {
            Name = name;
            Address = address;
            Phone = phone;
            items = new List<RealEstate>();
        }

        /// <summary>
        /// Adds a real estate item to the agency's collection
        /// </summary>
        /// <param name="item">The RealEstate object to add</param>
        public void AddItem(RealEstate item) {
            items.Add(item);
        }

        /// <summary>
        /// Returns a specific real estate item by its index
        /// </summary>
        /// <param name="index">The position of the item in the list</param>
        /// <returns>The RealEstate object at the specified index</returns>
        public RealEstate GetItem(int index) {
            return items[index];
        }

        /// <summary>
        /// Gets the total number of real estate items in the agency
        /// </summary>
        /// <returns>The count of items in the list</returns>
        public int Count() {
            return items.Count;
        }
    }
}