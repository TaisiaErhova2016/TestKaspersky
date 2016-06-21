using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestKasperskyTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало работы");
            ServiceMessage<Int32> serviceMessage = ServiceMessage<Int32>.Instance;
            Thread thread = new Thread(() => Console.WriteLine("Выполнили сообщение:" + serviceMessage.Pop().ToString()));
            thread.IsBackground = true;
            thread.Start();
            serviceMessage.Push(32);
            Console.ReadLine();
        }

        public sealed class ServiceMessage<T>
        {
            private readonly ReaderWriterLockSlim m_lock = new ReaderWriterLockSlim();
            private readonly Queue<T> msgQueue = new Queue<T>();
            //Обработчик сообщений
            public T Pop()
            {
                Console.WriteLine("Мы тут");
                Object message = new Object();
                m_lock.EnterReadLock();
                if (this.msgQueue.Count > 0)
                    message = this.msgQueue.Dequeue();
                m_lock.ExitReadLock();
                return (T)message;
            }
            // Добавление сообщений в очередь
            public void Push(T message)
            {
                m_lock.EnterWriteLock();
                Thread.Sleep(10000); //для тестирования
                this.msgQueue.Enqueue(message);
                Console.WriteLine("Поставили сообщение в очередь: " + message.ToString());
                m_lock.ExitWriteLock();
            }

            //Singleton для объявления очереди
            private static volatile ServiceMessage<T> instance;
            private static object sync_ = new Object();
            private ServiceMessage() { }

            public static ServiceMessage<T> Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (sync_)
                        {
                            if (instance == null)
                                instance = new ServiceMessage<T>();
                        }
                    }
                    return instance;
                }
            }
        }
    }
}
