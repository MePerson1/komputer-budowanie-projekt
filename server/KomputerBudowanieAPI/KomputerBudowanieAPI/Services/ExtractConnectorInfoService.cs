using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public static class ExtractConnectorInfoService
    {
        static public Dictionary<string, int> ExtractsStorageSlotsFromMotherboard(string input)
        {
            // "M.2 slot x2, SATA 3 x4"
            // "M.2 slot x3, SATA 3 x6"
            // "M.2 slot x1, SATA 3 x4"

            Dictionary<string, int> result = new Dictionary<string, int>();

            // Dzielimy wejściowy string na części po przecinkach
            string[] parts = input.Split(',');

            foreach (var part in parts)
            {
                // Usuwamy białe znaki na początku i końcu każdej części
                string trimmedPart = part.Trim();

                if (trimmedPart.Contains("x"))
                {
                    // Jeśli w danej części występuje "x", to oczekujemy, że jest to w formie "M.2 slot x2"
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
                    // W przeciwnym przypadku traktujemy całą część jako jedno złącze
                    if (result.ContainsKey(trimmedPart)) { result[trimmedPart]++; }
                    else { result[trimmedPart] = 1; }
                }
            }

            return result;
        }

        static public List<string> ExtractPowerConnectorsFromGraphicCard(string input)
        {
            List<string> connectors = new();

            string[] parts = input.Split('+'); // Dzielimy string na części po znaku '+'

            foreach (var part in parts)
            {
                if (part.Contains('x'))
                {
                    // Jeśli w danej części występuje "x", to oczekujemy, że jest to w formie "Nx 8-pin"
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
                    // W przeciwnym przypadku, traktujemy całą część jako jedno złącze
                    connectors.Add(part.Trim());
                }
            }

            return connectors;
        }

        static public List<string> ExtractSocketsFromCpuCooling(string input)
        {
            List<string> sockets = new List<string>();

            // Rozdzielanie stringa na częsci po przecinkach i ukośnikach
            string[] separators = { ",", "/" };

            foreach (var part in input.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                // Dodajemy każdą część stringa, usuwamy LGA i znaki białe
                sockets.Add(part.Replace("LGA ", "").Trim());
            }

            return sockets;
        }

        static public Dictionary<string, int> ExtractFanDimensionsFromCase(string input)
        {
            //120 mm x3/140 mm x2, 120 mm/140 mm x3, 120 mm x1
            Dictionary<string, int> resultMap = new Dictionary<string, int>();

            // Rozdzielenie na części po przecinkach
            string[] parts = input.Split(',');

            foreach (var part in parts)
            {
                // Użycie wyrażenia regularnego do znalezienia wymiaru i ilości
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
