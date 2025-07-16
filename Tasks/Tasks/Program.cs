using Task_Compression.Classes;

namespace Tasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Можно запустить для проверки работоспособности, либо прогнать по Unit тестам
            string test = "aaabbfcccdde";
            string goodStr = test.Compression();
            string dsStr = goodStr.Decompressions();
            Console.WriteLine("Входные данные: " + test);
            Console.WriteLine("Данные с компрессией: " + goodStr);
            Console.WriteLine("Данные с декомпрессией: " + dsStr);
        }
    }
}
