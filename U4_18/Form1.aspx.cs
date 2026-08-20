using System;
using System.Collections.Generic;
//using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using U4_18.Classes;

namespace U4_18
{
    public partial class Form1 : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the click event of the execution button.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event data</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FileUpload1.HasFiles)
                {
                    LabelError.Text = "Nepasirinkti failai.";
                    LabelError.CssClass = "error-text";
                    return;
                }

                List<Agency> agencies = ReadAgenciesData(Request.Files);

                SetElementsVisibility(true);
                ShowInitialData(agencies);
                ShowPopularTypes(agencies);

                List<RealEstate> repeated = TaskUtils.Repeated(agencies);
                List<string> districts = TaskUtils.Districts(agencies);
                List<RealEstate> largeObjects = TaskUtils.LargeObjects(agencies);

                SortLargeObjects(largeObjects);

                SaveResults(agencies, repeated, districts, largeObjects);

                LabelError.Text = "Apdorojimas baigtas sėkmingai.";
                LabelError.CssClass = "success-text";
            }
            catch (Exception ex)
            {
                LabelError.Text = "Klaida: " + ex.Message;
                LabelError.CssClass = "error-text";
            }
        }

        /// <summary>
        /// Iterates through uploaded files and populates a list of Agency objects
        /// </summary>
        /// <param name="uploadedFiles">Collection of files uploaded via the FileUpload control</param>
        /// <returns>A list of Agency objects containing real estate data</returns>
        private List<Agency> ReadAgenciesData(HttpFileCollection uploadedFiles)
        {
            List<Agency> agencies = new List<Agency>();

            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                HttpPostedFile file = uploadedFiles[i];
                if (file.ContentLength > 0)
                {
                    using (StreamReader reader = new StreamReader(file.InputStream))
                    {
                        string name = reader.ReadLine();
                        string address = reader.ReadLine();
                        string phone = reader.ReadLine();
                        Agency agency = new Agency(name, address, phone);

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            RealEstate item = ParseLine(line);
                            if (item != null) agency.AddItem(item);
                        }
                        agencies.Add(agency);
                    }
                }
            }
            return agencies;
        }

        //// <summary>
        /// Controls the visibility of result labels, tables, and lists on the page
        /// </summary>
        /// <param name="visible">True to show elements, false to hide them</param>
        private void SetElementsVisibility(bool visible)
        {
            LabelInitial.Visible = visible;
            Table1.Visible = visible;
            LabelResult1.Visible = visible;
            Table2.Visible = visible;
            LabelResult2.Visible = visible;
            BulletedList1.Visible = visible;
        }

        /// <summary>
        /// Parses a single line of text into a Flat or House object
        /// </summary>
        /// <param name="line">The semicolon-separated string from the data file</param>
        /// <returns>A RealEstate object (House or Flat), or null if parsing fails</returns>
        private RealEstate ParseLine(string line)
        {
            string[] parts = line.Split(';');

            try
            {
                string category = parts[0].Trim();
                string city = parts[1].Trim();
                string district = parts[2].Trim();
                string street = parts[3].Trim();
                string number = parts[4].Trim();
                string type = parts[5].Trim();
                int year = int.Parse(parts[6]);
                double area = double.Parse(parts[7].Replace(',', '.'), CultureInfo.InvariantCulture);
                int rooms = int.Parse(parts[8]);

                if (category == "Flat")
                {
                    return new Flat(city, district, street, number, type, year, area, rooms, int.Parse(parts[9]));
                }
                else if (category == "House")
                {
                    return new House(city, district, street, number, type, year, area, rooms, parts[9].Trim());
                }
            }
            catch { return null; }
            return null;
        }

        /// <summary>
        /// Displays the initial data from all agencies in a formatted Table
        /// </summary>
        /// <param name="agencies">The list of agencies to display</param>
        private void ShowInitialData(List<Agency> agencies)
        {
            Table1.Rows.Clear();
            foreach (Agency agency in agencies)
            {
                TableRow agencyHeader = new TableRow { CssClass = "agency-header" };
                agencyHeader.Cells.Add(new TableCell { Text = $"{agency.Name} | {agency.Address}", ColumnSpan = 9 });
                Table1.Rows.Add(agencyHeader);

                TableHeaderRow header = new TableHeaderRow();
                string[] colNames = { "Miestas", "Rajonas", "Gatvė", "Nr.", "Tipas", "Metai", "Plotas", "Kamb.", "Papildoma" };
                foreach (string name in colNames) header.Cells.Add(new TableHeaderCell { Text = name });
                Table1.Rows.Add(header);

                for (int i = 0; i < agency.Count(); i++)
                {
                    RealEstate item = agency.GetItem(i);
                    TableRow row = new TableRow();
                    row.Cells.Add(new TableCell { Text = item.City });
                    row.Cells.Add(new TableCell { Text = item.District });
                    row.Cells.Add(new TableCell { Text = item.Street });

                    row.Cells.Add(new TableCell { Text = item.Number, CssClass = "numeric-cell" });

                    row.Cells.Add(new TableCell { Text = item.Type });
                    row.Cells.Add(new TableCell { Text = item.Year.ToString(), CssClass = "numeric-cell" });
                    row.Cells.Add(new TableCell { Text = item.Area.ToString("F2"), CssClass = "numeric-cell" });
                    row.Cells.Add(new TableCell { Text = item.Rooms.ToString(), CssClass = "numeric-cell" });

                    string extra = (item is Flat f) ? "Aukštas: " + f.Floor : "Šildymas: " + ((House)item).Heating;
                    row.Cells.Add(new TableCell { Text = extra });
                    Table1.Rows.Add(row);
                }
            }
        }

        /// <summary>
        /// Displays the most frequent property types for each agency in a summary table
        /// </summary>
        /// <param name="agencies">The list of agencies to analyze</param>
        private void ShowPopularTypes(List<Agency> agencies)
        {
            Table2.Rows.Clear();
            TableHeaderRow header = new TableHeaderRow();
            header.Cells.Add(new TableHeaderCell { Text = "Agentūra" });
            header.Cells.Add(new TableHeaderCell { Text = "Populiariausias tipas" });
            header.Cells.Add(new TableHeaderCell { Text = "Kiekis" });
            Table2.Rows.Add(header);

            foreach (Agency a in agencies)
            {
                int count;
                string type = TaskUtils.MostPopularType(a, out count);
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = a.Name });
                row.Cells.Add(new TableCell { Text = type });
                row.Cells.Add(new TableCell { Text = count.ToString(), CssClass = "numeric-cell" });
                Table2.Rows.Add(row);
            }
        }

        /// <summary>
        /// orts the list of large real estate objects primarily by area (descending) and secondarily by specific attributes
        /// </summary>
        /// <param name="list">The list of large RealEstate objects to sort</param>
        private void SortLargeObjects(List<RealEstate> list)
        {
            list.Sort((x, y) => {
                int areaCompare = y.Area.CompareTo(x.Area);
                if (areaCompare != 0) return areaCompare;
                if (x is House hX && y is House hY) return string.Compare(hX.Heating, hY.Heating, StringComparison.OrdinalIgnoreCase);
                if (x is Flat fX && y is Flat fY) return fX.Floor.CompareTo(fY.Floor);
                return 0;
            });
        }

        /// <summary>
        /// Saves the processed results into CSV files and updates the download links in the UI
        /// </summary>
        /// <param name="repeated">List of repeating objects</param>
        /// <param name="districts">List of unique districts</param>
        /// <param name="large">Sorted list of large objects</param>
        private void SaveResults(List<Agency> agencies, List<RealEstate> repeated, List<string> districts, List<RealEstate> large)
        {
            string folder = Server.MapPath("~/Data/");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string txtReportPath = folder + "Results.txt";


            InOutUtils.WriteSimpleText(txtReportPath, "NT AGENTŪRŲ APDOROJIMO REZULTATAI\n Generated: " + DateTime.Now.ToString());

            foreach (Agency agency in agencies)
            {
                InOutUtils.PrintAgencyData(txtReportPath, agency);
            }

            List<string> popularResults = new List<string>();
            foreach (Agency a in agencies)
            {
                int count;
                string type = TaskUtils.MostPopularType(a, out count);
                popularResults.Add($"{a.Name}: {type} ({count})");
            }
            InOutUtils.PrintPopularTypes(txtReportPath, popularResults, "POPULIARIAUSI NT TIPAI:");

            if (repeated.Count > 0)
                InOutUtils.WriteToCsv(folder + "Kartojasi.csv", repeated, "Agentūrose pasikartojantys objektai;");
            else
                InOutUtils.WriteSimpleText(folder + "Kartojasi.csv", "Atitinkamų pasikartojančių objektų nerasta.");

            if (districts.Count > 0)
                InOutUtils.WriteStringsToCsv(folder + "Mikrorajonai.csv", districts, "Visi mikrorajonai;");
            else
                InOutUtils.WriteSimpleText(folder + "Mikrorajonai.csv", "Sistemoje mikrorajonų duomenų nėra.");

            if (large.Count > 0)
                InOutUtils.WriteToCsv(folder + "Dideli.csv", large, "Surikiuoti dideli objektai;");
            else
                InOutUtils.WriteSimpleText(folder + "Dideli.csv", "Didelių objektų pagal nurodytus kriterijus nerasta.");

            BulletedList1.Items.Clear();
            string[] files = { "Results.txt", "Kartojasi.csv", "Mikrorajonai.csv", "Dideli.csv" };
            foreach (string f in files) BulletedList1.Items.Add(new ListItem(f, "~/Data/" + f));
        }
    }
}