using System.Collections.Generic;

namespace U4_18.Classes {
    /// <summary>
    /// Static utility class for data processing tasks related to real estate and agencies
    /// </summary>
    public static class TaskUtils {
        /// <summary>
        /// Finds the most frequently occurring property type within a single agency
        /// </summary>
        /// <param name="agency">The agency object to analyze</param>
        /// <param name="maxCount">Output parameter for the number of occurrences found</param>
        /// <returns>The name of the most popular property type</returns>
        public static string MostPopularType(Agency agency, out int maxCount) {
            maxCount = 0;
            string result = "";

            for (int i = 0; i < agency.Count(); i++) {
                RealEstate first = agency.GetItem(i);
                int count = 0;

                for (int j = 0; j < agency.Count(); j++) {
                    if (first.Type == agency.GetItem(j).Type) {
                        count++;
                    }
                }

                if (count > maxCount) {
                    maxCount = count;
                    result = first.Type;
                }
            }

            return result;
        }

        /// <summary>
        /// Identifies real estate objects that are listed in more than one agency
        /// </summary>
        /// <param name="agencies">List of agencies to search through</param>
        /// <returns>A list of unique real estate objects that appear multiple times</returns>
        public static List<RealEstate> Repeated(List<Agency> agencies) {
            List<RealEstate> result = new List<RealEstate>();

            for (int i = 0; i < agencies.Count; i++) {
                for (int j = 0; j < agencies[i].Count(); j++) {
                    RealEstate item = agencies[i].GetItem(j);
                    int repeats = 0;

                    for (int k = 0; k < agencies.Count; k++) {
                        for (int m = 0; m < agencies[k].Count(); m++) {
                            if (item.Equals(agencies[k].GetItem(m))) {
                                repeats++;
                            }
                        }
                    }

                    if (repeats > 1 && !Contains(result, item)) {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Compiles a list of all unique districts found across all agencies
        /// </summary>
        /// <param name="agencies">The list of agencies to process</param>
        /// <returns>A list of unique district names as strings</returns>
        public static List<string> Districts(List<Agency> agencies) {
            List<string> result = new List<string>();

            for (int i = 0; i < agencies.Count; i++) {
                for (int j = 0; j < agencies[i].Count(); j++) {
                    string district = agencies[i].GetItem(j).District;

                    if (!result.Contains(district)) {
                        result.Add(district);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Filters all properties across all agencies to find those meeting "Large" criteria
        /// </summary>
        /// <param name="agencies">The list of agencies to process</param>
        /// <returns>A list of properties that are considered large</returns>
        public static List<RealEstate> LargeObjects(List<Agency> agencies) {
            List<RealEstate> result = new List<RealEstate>();

            for (int i = 0; i < agencies.Count; i++) {
                for (int j = 0; j < agencies[i].Count(); j++) {
                    RealEstate item = agencies[i].GetItem(j);

                    if (item.IsLarge()) {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Helper method to check if a specific real estate object already exists in a list
        /// </summary>
        /// <param name="list">The list to search in</param>
        /// <param name="item">The real estate object to search for</param>
        /// <returns>True if the object is found, false otherwise</returns>
        private static bool Contains(List<RealEstate> list, RealEstate item) {
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Equals(item)) {
                    return true;
                }
            }

            return false;
        }
    }
}