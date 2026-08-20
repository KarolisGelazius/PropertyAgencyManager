using System;
using System.Collections.Generic;
using System.IO;

namespace U4_18.Classes
{
    /// <summary>
    /// Utility class for handling input and output operations
    /// </summary>
    public static class InOutUtils
    {
        // <summary>
        /// Reads all lines from an uploaded file and stores them in a list of strings
        /// </summary>
        /// <param name="file">The HttpPostedFile object received from the web form</param>
        /// <returns>A list containing each line of the file as a string</returns>
        public static List<string> ReadRawLines(Stream file)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        /// <summary>
        /// Spausdina agentūros bendrąją informaciją ir visą nekilnojamojo turto sąrašą į tekstinį failą lentelės pavidalu.
        /// </summary>
        /// <param name="filePath">Kelias iki rezultato tekstinio failo.</param>
        /// <param name="agency">Agentūros objektas, kurio duomenys bus spausdinami.</param>
        public static void PrintAgencyData(string filePath, Agency agency)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
            {
                string sep = new string('-', 105);

                writer.WriteLine(sep);
                writer.WriteLine("| {0,-30} | {1,-44} | {2,-21} |",
                    agency.Name, agency.Address, agency.Phone);
                writer.WriteLine(sep);
                writer.WriteLine("| {0,-9} | {1,-12} | {2,-14} | {3,-3} | {4,-8} | {5,-5} | {6,-7} | {7,-22} |",
                    "Miestas", "Rajonas", "Gatvė", "Nr.", "Tipas", "Metai", "Plotas", "Papildoma");
                writer.WriteLine(sep);

                for (int i = 0; i < agency.Count(); i++)
                {
                    RealEstate item = agency.GetItem(i);
                    string extra = (item is Flat f) ? $"Aukštas: {f.Floor}" : $"Šildymas: {((House)item).Heating}";

                    string printExtra = extra.Length > 18 ? extra.Substring(0, 18) : extra;

                    writer.WriteLine("| {0,-9} | {1,-12} | {2,-14} | {3,3} | {4,-8} | {5,5} | {6,7} | {7,-22} |",
                        item.City, item.District, item.Street, item.Number, item.Type, item.Year,
                        item.Area.ToString("F1"), printExtra);
                }
                writer.WriteLine(sep);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Spausdina analizės rezultatus apie populiariausius nekilnojamojo turto tipus kiekvienoje agentūroje.
        /// </summary>
        /// <param name="filePath">Kelias iki rezultato tekstinio failo.</param>
        /// <param name="results">Sąrašas su suformuotomis eilutėmis (Agentūra: Tipas (Kiekis)).</param>
        /// <param name="header">Lentelės antraštės tekstas.</param>
        public static void PrintPopularTypes(string filePath, List<string> results, string header)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
            {
                string line = new string('=', 55);
                writer.WriteLine(line);
                writer.WriteLine(header);
                writer.WriteLine(line);
                writer.WriteLine("| {0,-28} | {1,-20} |", "Agentūra", "Tipas (Kiekis)");
                writer.WriteLine(new string('-', 55));

                foreach (var res in results)
                {
                    var parts = res.Split(':');
                    if (parts.Length == 2)
                        writer.WriteLine("| {0,-28} | {1,-20} |", parts[0].Trim(), parts[1].Trim());
                }
                writer.WriteLine(line);
            }
        }

        /// <summary>
        /// Writes a collection of RealEstate objects to a CSV file with a custom header
        /// </summary>
        /// <param name="filePath">The physical path where the file will be saved</param>
        /// <param name="data">The list of RealEstate objects to export</param>
        /// <param name="header">A descriptive header line for the beginning of the file</param>
        public static void WriteToCsv(string filePath, List<RealEstate> data, string header)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(header);
                writer.WriteLine("Miestas;Rajonas;Gatvė;Nr;Tipas;Metai;Plotas;Kambariai;Papildoma");
                foreach (var item in data)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        /// <summary>
        /// Writes a list of strings to a CSV file
        /// </summary>
        /// <param name="filePath">The physical path where the file will be saved</param>
        /// <param name="data">The list of strings to export</param>
        /// <param name="header">A descriptive header line for the beginning of the file</param>
        public static void WriteStringsToCsv(string filePath, List<string> data, string header)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(header);
                foreach (string item in data)
                {
                    writer.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// Creates a simple text file with a single message or piece of information
        /// </summary>
        /// <param name="filePath">The physical path where the file will be saved</param>
        /// <param name="message">The text content to be written to the file</param>
        public static void WriteSimpleText(string filePath, string message)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(message);
            }
        }
    }
}