using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public static class ExtractConnectorInfoService
    {
        /// <summary>
        /// Extracts information about memory slots on a motherboard from the provided string and returns the quantity of available slots for different connector types.
        /// </summary>
        /// <param name="input">String containing information about slots, e.g., "M.2 slot x2, SATA 3 x4".</param>
        /// <returns>A dictionary where keys represent connector types and values represent the quantity of available slots.</returns>
        /// <remarks>
        /// Example input string: "M.2 slot x2, SATA 3 x4, SATA 2 x1", returns: ["M.2 slot": 2, "SATA 3": 4, "SATA 2": 1]
        /// </remarks>
        static public Dictionary<string, int> ExtractsStorageSlotsFromMotherboard(string input)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            // Splitting into parts by commas
            string[] parts = input.Split(',');

            foreach (var part in parts)
            {
                // Trimming white spaces
                string trimmedPart = part.Trim();

                if (trimmedPart.Contains("x"))
                {
                    // If there is an 'x' in a given part, we expect it to be in the form 'M.2 slot x2
                    string[] subparts = trimmedPart.Split('x');
                    if (subparts.Length == 2 && int.TryParse(subparts[1], out int count))
                    {
                        string connector = subparts[0].Trim();
                        if (result.ContainsKey(connector)) { result[connector] += count; }
                        else { result[connector] = count; }
                    }
                }
                else
                {
                    // Otherwise, we treat the entire part as a single connector
                    if (result.ContainsKey(trimmedPart)) { result[trimmedPart]++; }
                    else { result[trimmedPart] = 1; }
                }
            }

            return result;
        }

        /// <summary>
        /// Extracts information about power connectors from a graphics card based on the provided string.
        /// </summary>
        /// <param name="input">String containing information about power connectors, e.g., "2x 8-pin + 1x 6-pin".</param>
        /// <returns>List of strings representing power connector types, taking into account the quantities defined in the input string.</returns>
        /// <remarks>
        /// Example input string: "2x 8-pin + 1x 6-pin", returns: ["8-pin", "8-pin", "6-pin"]
        /// </remarks>
        static public List<string> ExtractPowerConnectorsFromGraphicCard(string input)
        {
            List<string> connectors = new();

            // Splitting the string into parts by the '+' sign
            string[] parts = input.Split('+'); 

            foreach (var part in parts)
            {
                if (part.Contains('x'))
                {
                    // If there is an 'x' in a given part, we expect it to be in the form 'Nx 8-pin'
                    string[] subparts = part.Trim().Split('x');
                    if (subparts.Length == 2 && int.TryParse(subparts[0], out int count))
                    {
                        string connector = subparts[1].Trim();
                        for (int i = 0; i < count; i++)
                        {
                            connectors.Add(connector);
                        }
                    }
                }
                else
                {
                    // Otherwise, we treat the entire part as a single connector
                    connectors.Add(part.Trim());
                }
            }

            return connectors;
        }

        /// <summary>
        /// Extracts information about CPU sockets compatible with a CPU cooling system from the provided string.
        /// </summary>
        /// <param name="input">String containing information about CPU sockets, e.g., "LGA 1150/1151/1155/1156/1200, LGA 1700, LGA 2011/2011-3, LGA 2066".</param>
        /// <returns>List of strings representing compatible CPU sockets after removing "LGA" and trimming whitespace, e.g., ["1150", "1155", "1156", "1200", "1700", "2011-3"].</returns>
        /// <remarks>
        /// Example input string: "LGA 1150/1151, LGA 1700, LGA 2011/2011-3, LGA 2066", returns: ["1150", "1151", "1700", "2011", "2011-3"]
        /// </remarks>
        static public List<string> ExtractSocketsFromCpuCooling(string input)
        {
            List<string> sockets = new List<string>();

            // Splitting the string into parts by commas and slashes
            string[] separators = { ",", "/" };

            foreach (var part in input.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                // Adding each part of the string, removing LGA, and trimming white spaces
                sockets.Add(part.Replace("LGA ", "").Trim());
            }

            return sockets;
        }

        /// <summary>
        /// Extracts information about fan dimensions and quantities from a case based on the provided string.
        /// </summary>
        /// <param name="input">String containing information about fan dimensions, e.g., .</param>
        /// <returns>Dictionary where keys represent fan dimensions and values represent the maximum quantity of fans of that dimension in the panel of the case.</returns>
        /// <remarks>
        /// Example input string: "120 mm x3/140 mm x2, 120 mm/140 mm x3, 120 mm x1", returns ["120 mm": 3, "140 mm": 3]
        /// </remarks>
        static public Dictionary<string, int> ExtractFanDimensionsFromCase(string input)
        {
            // 120 mm x3/140 mm x2, 120 mm/140 mm x3, 120 mm x1
            Dictionary<string, int> resultMap = new Dictionary<string, int>();

            // Splitting into parts by commas
            string[] parts = input.Split(',');

            foreach (var part in parts)
            {
                // Using a regular expression to find the dimension and quantity
                Match match = Regex.Match(part.Trim(), @"(\d+\s*mm(?:\/\d+\s*mm)?)(?:\s*x(\d+))?");

                if (match.Success)
                {
                    string dimension = match.Groups[1].Value.Trim();
                    int count = 1;

                    if (match.Groups[2].Success)
                    {
                        count = int.Parse(match.Groups[2].Value);
                    }

                    if (dimension.Contains('/'))
                    {
                        string[] dimentsionParts = dimension.Split('/');
                        AddFanDimension(resultMap, dimentsionParts[0], count);
                        AddFanDimension(resultMap, dimentsionParts[1], count);
                    }
                    else
                    {
                        AddFanDimension(resultMap, dimension, count);
                    }
                }
            }

            return resultMap;
        }

        /// <summary>
        /// Adds or updates the fan dimension entry in the result dictionary, considering the maximum quantity.
        /// </summary>
        /// <param name="resultMap">Dictionary representing fan dimensions and quantities.</param>
        /// <param name="key">Fan dimension.</param>
        /// <param name="value">Quantity of fans with the specified dimension.</param>
        static private void AddFanDimension(Dictionary<string, int> resultMap, string key, int value)
        {
            if (resultMap.TryGetValue(key, out int currentValue))
            {
                if (value > currentValue)
                {
                    resultMap[key] = value;
                }
            }
            else
            {
                resultMap.Add(key, value);
            }
        }
    }
}
