using System.Text.RegularExpressions;


namespace Task_Logs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "C:\\Users\\ghpla\\OneDrive\\Documentos\\GitHub\\Test-Task\\Tasks\\Task Logs\\input.txt"; // Задаем пути к файлам
            string outputFile = "C:\\Users\\ghpla\\OneDrive\\Documentos\\GitHub\\Test-Task\\Tasks\\Task Logs\\output.txt";
            string problemFile = "C:\\Users\\ghpla\\OneDrive\\Documentos\\GitHub\\Test-Task\\Tasks\\Task Logs\\problems.txt";

            using (var reader = new StreamReader(inputFile))
            using (var writer = new StreamWriter(outputFile))
            using (var problemWriter = new StreamWriter(problemFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (TryParseFormat1(line, out var record))
                    {
                        writer.WriteLine(record); //Пишем в исходный файл, по первому формату логов
                    }
                    else if (TryParseFormat2(line, out record))
                    {
                        writer.WriteLine(record);//Пишем в исходный файл, по второму формату логов
                    }
                    else
                    {
                        problemWriter.WriteLine(line);//В случае ошибки пишем строку в проблемы
                    }
                }
            }

            Console.WriteLine("Обработка завершена");
        }

        static bool TryParseFormat1(string line, out string output)
        {
            // Пример: 10.03.2025 15:14:49.523 INFORMATION  Версия программы: '3.4.0.48729'
            var regex = new Regex(@"^(\d{2})\.(\d{2})\.(\d{4}) (\d{2}:\d{2}:\d{2}\.\d{3}) ([A-Z]+)\s+(.+)$");
            var match = regex.Match(line);

            if (!match.Success)//В случае ошибки регекса, возвращаем null
            {
                output = null;
                return false;
            }

            string date = $"{match.Groups[1].Value}-{match.Groups[2].Value}-{match.Groups[3].Value}";
            string time = match.Groups[4].Value;
            string level = NormalizeLevel(match.Groups[5].Value);
            string message = match.Groups[6].Value.Trim();

            output = $"{date}\t{time}\t{level}\tDEFAULT\t{message}";//Собираем ответ в нужном формате
            return true;
        }

        static bool TryParseFormat2(string line, out string output)
        {
            // Пример: 2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'
            var regex = new Regex(@"^(\d{4}-\d{2}-\d{2}) (\d{2}:\d{2}:\d{2}\.\d{4})\|\s*(INFO|WARN|WARNING|ERROR|DEBUG)\|[^|]+\|([^|]+)\|(.+)$");
            var match = regex.Match(line);

            if (!match.Success)//В случае ошибки регекса, возвращаем null
            {
                output = null;
                return false;
            }

            string date = match.Groups[1].Value;
            string time = match.Groups[2].Value;
            string level = NormalizeLevel(match.Groups[3].Value);
            string method = match.Groups[4].Value.Trim();
            string message = match.Groups[5].Value.Trim();

            output = $"{date}\t{time}\t{level}\t{method}\t{message}";
            return true;
        }

        static string NormalizeLevel(string input)//Изменяем строки под формат
        {
            return input.ToUpper() switch
            {
                "INFORMATION" => "INFO",
                "WARNING" => "WARN",
                _ => input.ToUpper()
            };
        }
    }
}
