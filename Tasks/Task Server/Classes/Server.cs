using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task_Server.Classes
{
    class Server
    {
        /*Выражаем сервер как Singleton класс, так как сервер у нас 1, а так же для 
         * исключения порождения экземпляров с другим состоянием. Так же я использовал
         * потоко безопасною реализацию для исключения одновременного доступа из нескольких потоков.
         */

        public static Server instance { get; set; }
        public int _Count { get;private set; }
        private static object syncRoot = new Object();


        protected Server()
        {
            
        }
        public int GetCount()
        {
            return this._Count;
        }

        public int AddToCount(int add)
        {
            return _Count += add;
        }

        public static Server getInstance()
        {
            if (instance == null)
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new Server();
                }
            return instance;
        }
        

    }
}
