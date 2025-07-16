using Task_Server.Classes;

namespace Task_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (new Thread(() =>
            {
                Server servCheck = Server.getInstance();
                Console.WriteLine(servCheck.AddToCount(10));
            })).Start();

            Server servCheck = Server.getInstance();
            Console.WriteLine(servCheck.GetCount());
        }
    }
}
