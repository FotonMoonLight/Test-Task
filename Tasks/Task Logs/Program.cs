using System.Text.RegularExpressions;


namespace Task_Logs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: LogStandardizer <log.txt> <goodlog.txt>");
                return;
            }

            string inputFile = "log.txt";
            string outputFile = "goodlog.txt";
            string problemsFile = "problems.txt";

            try
            {
                using (StreamReader reader = new StreamReader(inputFile))
                using (StreamWriter writer = new StreamWriter(outputFile))
                using (StreamWriter problemsWriter = new StreamWriter(problemsFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (TryParseLine(line, out var parsedLine))
                        {
                            writer.WriteLine(parsedLine);
                        }
                        else
                        {
                            problemsWriter.WriteLine(line);
                        }
                    }
                }

                Console.WriteLine("Processing completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static bool TryParseLine(string line, out string parsedLine)
        {
            parsedLine = null;

            // Попробуем разобрать первый формат
            var format1Match = Regex.Match(line, @"^(\d{2})\.(\d{2})\.(\d{4})\s(\d{2}:\d{2}:\d{2}\.\d+)\s+(\w+)\s+(.*)$");
            if (format1Match.Success)
            {
                string day = format1Match.Groups[1].Value;
                string month = format1Match.Groups[2].Value;
                string year = format1Match.Groups[3].Value;
                string time = format1Match.Groups[4].Value;
                string level = NormalizeLogLevel(format1Match.Groups[5].Value);
                string message = format1Match.Groups[6].Value.Trim();

                parsedLine = $"{day}-{month}-{year}\t{time}\t{level}\tDEFAULT\t{message}";
                return true;
            }

            // Попробуем разобрать второй формат
            var format2Match = Regex.Match(line, @"^(\d{4})-(\d{2})-(\d{2})\s(\d{2}:\d{2}:\d{2}\.\d+)\|\s*(\w+)\|.*?\|\s*([^|]*)\|\s*(.*)$");
            if (format2Match.Success)
            {
                string year = format2Match.Groups[1].Value;
                string month = format2Match.Groups[2].Value;
                string day = format2Match.Groups[3].Value;
                string time = format2Match.Groups[4].Value;
                string level = NormalizeLogLevel(format2Match.Groups[5].Value);
                string method = format2Match.Groups[6].Value.Trim();
                string message = format2Match.Groups[7].Value.Trim();

                if (string.IsNullOrWhiteSpace(method))
                {
                    method = "DEFAULT";
                }

                parsedLine = $"{day}-{month}-{year}\t{time}\t{level}\t{method}\t{message}";
                return true;
            }

            return false;
        }

        static string NormalizeLogLevel(string level)
        {
            return level.ToUpper() switch
            {
                "INFORMATION" => "INFO",
                "WARNING" => "WARN",
                _ => level.ToUpper()
            };
        }
    }
}
