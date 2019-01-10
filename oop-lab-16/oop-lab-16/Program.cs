using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace oop_lab_16
{
    class Program
    {

        static void Main(string[] args)
        {
            //DoTask1();
            //DoTask2();
            //DoTask3();
            //DoTask4_1();
            //DoTask4_2();
            //DoTask5();
            //DoTask6();
            //DoTask7();
            //DoTask8();


            #region Task 1
            void DoTask1()
            {
                Stopwatch stopWatch = new Stopwatch();

                Task task1 = new Task(Task1);
                Console.WriteLine("ID: " + task1.Id);
                stopWatch.Start();
                task1.Start();
                if (task1.IsCompleted)
                {
                    Console.WriteLine("Задача завершена");
                }
                else
                {
                    Console.WriteLine("Задача не завершена");
                }

                task1.Wait();

                Console.WriteLine("Статус: " + task1.Status);

                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime: " + elapsedTime);
            }
            #endregion

            #region Task 2
            void DoTask2()
            {


                Stopwatch stopWatch = new Stopwatch();
                Task task2 = new Task(Task1);

                stopWatch.Start();
                task2.Start();
                if (!task2.IsCanceled)
                {
                    Console.WriteLine("Введите Y для отмены операции или другой символ для ее продолжения:");
                    string s = Console.ReadLine();
                    if (s == "Y")
                        cancelTokenSource.Cancel();

                }
                task2.Wait();
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("RunTime: " + elapsedTime);
                Console.WriteLine("Кол-во чисел: " + list.Count() + "(должно быть 78498)");

            }
            #endregion

            #region Task 3

            void DoTask3()
            {
                Task<double>[] tasks = new Task<double>[3];
                Func<double> func1 = () => { Console.WriteLine("First"); return Math.Cos(855); };
                Func<double> func2 = () => { Console.WriteLine("Second"); return Math.Exp(5); };
                Func<double> func3 = () => { Console.WriteLine("Third"); return Math.Pow(25, 2); };

                tasks[0] = new Task<double>(func1);
                tasks[1] = new Task<double>(func2);
                tasks[2] = new Task<double>(func3);


                foreach (var t in tasks)
                {
                    t.Start();
                }

                Task.WaitAll(tasks);
                //tasks[0].Result;
                // tasks[1].Result;
                // tasks[2].Result;

                Task task4 = new Task(() => { Console.WriteLine("Результат вычисления: " + (tasks[0].Result + tasks[1].Result + tasks[2].Result)); });
                task4.Start();
                task4.Wait();
            }

            #endregion

            #region Task 4
            void DoTask4_1()
            {
                Task task1 = new Task(firstTask);
                Task task2 = task1.ContinueWith(Display);
                task1.Start();
                task2.Wait();
            }
            void DoTask4_2()
            {
                Task<int> task1 = Task.Run(() => Enumerable.Range(1, 10000).Where(i => i % 11 == 0).Count(i => i > 1000));
                var awaiter = task1.GetAwaiter();
                awaiter.OnCompleted(() => { Console.WriteLine("Задание 1 завершилось со следующим результатом: " + (int)awaiter.GetResult()); });
                Console.ReadKey();
            }

            void firstTask()
            {
                Console.WriteLine("Задача 1 запущена");
                Thread.Sleep(5000);
                Console.WriteLine("Задача 1 завершина");
            }

            void Display(Task task)
            {
                Console.WriteLine("Статус задачи 1: " + task.Status);
                Console.WriteLine("Задача 1 продолжена с помощью Display(начало 2 задачи)");
                Thread.Sleep(5000);
                Console.WriteLine("Задача 2 завершина");
            }


            #endregion

            #region Task 5
            void DoTask5()
            {
                Random random = new Random();

                Stopwatch sw = new Stopwatch();
                int[] data = new int[100000000];
                sw.Start();
                Parallel.For(0, data.Length, i => data[i] = i);
                sw.Stop();
                Console.WriteLine("|| заполнение: " + sw.Elapsed.TotalSeconds + " секунд");
                sw.Reset();

                sw.Start();
                Parallel.ForEach(data, MyTransform);
                sw.Stop();
                Console.WriteLine("|| преобразование: " + sw.Elapsed.TotalSeconds + " секунд");
                sw.Reset();

                sw.Start();
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = i;
                }
                sw.Stop();
                Console.WriteLine("последовательное заполнение: " + sw.Elapsed.TotalSeconds + " секунд");
                sw.Reset();

                sw.Start();
                foreach (int i in data)
                {
                    MyTransform(i);
                }
                sw.Stop();
                Console.WriteLine("последовательное преобразование: " + sw.Elapsed.TotalSeconds + " секунд");

                void MyTransform(int i)
                {
                    data[i] = data[i] / 10;

                    if (data[i] < 10000) data[i] = 0;
                    if (data[i] >= 10000) data[i] = 100;
                    if (data[i] > 20000) data[i] = 200;
                    if (data[i] > 30000) data[i] = 300;
                }
            }
            #endregion

            #region Task 6
            void DoTask6()
            {
                Parallel.Invoke(First, Second, Third);

                Console.ReadLine();
            }
            #endregion

            #region Task 7
            void DoTask7()
            {
                Parallel.Invoke(men1, men2, men3, men4, men5, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10);
                Console.Read();
            }
            #endregion

            #region Task 8
            void DoTask8()
            { 
            counterAsync();

            for (int i = 0; i < 30; i++)
            {
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(i);
                Console.ResetColor();
                Thread.Sleep(1);
            }

            Console.Read();
        }
            #endregion

        }

        static void counter()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(i);
                Console.ResetColor();
            }
            //Thread.Sleep(8000);
        }
        // определение асинхронного метода
        static async void counterAsync()
        {
            Console.WriteLine("Начало метода counterAsync"); // выполняется синхронно
            await Task.Run(() => counter());                            // выполняется асинхронно
            Console.WriteLine("Конец метода counterAsync");  // выполняется синхронно
        }
        #region Task 7
        public static ConcurrentQueue<string> teqneaue = new ConcurrentQueue<string>();

        public static void men1()
        {
            Thread.Sleep(3000);

            teqneaue.Enqueue("наушники");
            print();
        }
        public static void men2()
        {
            Thread.Sleep(7000);

            teqneaue.Enqueue("телефизор");
            print();

        }
        public static void men3()
        {
            Thread.Sleep(3500);

            teqneaue.Enqueue("ПК");
            print();

        }
        public static void men4()
        {
            Thread.Sleep(3000);

            teqneaue.Enqueue("микрофон");
            print();

        }
        public static void men5()
        {
            Thread.Sleep(1000);
            teqneaue.Enqueue("наушники");
            print();
        }

        public static void user1()
        {
            Thread.Sleep(1300);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("user1 извлёк "+result);
            }
            else
            {
                Console.WriteLine("1 Склад пуст");
            }
        }

        public static void user2()
        {
            Thread.Sleep(200);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("2 user2 извлёк " + result);
            }
            else
            {
                Console.WriteLine("2 Склад пуст");
            }
        }
        public static void user3()
        {
            Thread.Sleep(30);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("user3 извлёк " + result);
            }
            else
            {
                Console.WriteLine("3 Склад пуст");
            }
        }
        public static void user4()
        {
            Thread.Sleep(90);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("4 user4 извлёк " + result);
            }
            else
            {
                Console.WriteLine("4 Склад пуст");
            }
        }
        public static void user5()
        {
            Thread.Sleep(2800);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("user5 извлёк " + result);
            }
            else
            {
                Console.WriteLine("5 Склад пуст");
            }
        }
        public static void user6()
        {
            Thread.Sleep(4300);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("user6 извлёк " + result);
            }
            else
            {
                Console.WriteLine("6 Склад пуст");
            }
        }
        public static void user7()
        {
            Thread.Sleep(400);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("user7 извлёк " + result);
            }
            else
            {
                Console.WriteLine("7 Склад пуст");
            }
        }
        public static void user8()
        {
            Thread.Sleep(800);

            string result;
            if (teqneaue.TryDequeue(out result))
            {                
                Console.WriteLine("user8 извлёк " + result);
            }
            else
            {
                Console.WriteLine("8 Склад пуст");
            }
        }
        public static void user9()
        {
            Thread.Sleep(1300);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("user9 извлёк " + result);
            }
            else
            {
                Console.WriteLine("9 Склад пуст");
            }
        }
        public static void user10()
        {
            Thread.Sleep(1500);

            string result;
            if (teqneaue.TryDequeue(out result))
            {
                Console.WriteLine("user10 извлёк " + result);
            }
            else
            {
                Console.WriteLine("10 Склад пуст");
            }
        }

        public static void print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("На складе:");
            foreach(string s in teqneaue)
            {
                Console.WriteLine(s);
            }
            Console.ResetColor();
        }

#endregion
        public static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public static CancellationToken token = cancelTokenSource.Token;
        #region Task 1

        public static List<int> list = new List<int>();

        public static void Task1()
            {
            int MaxValue=1000000;
            //Console.WriteLine("Введите максимальное число");
            // MaxValue = Int32.Parse(Console.ReadLine());
            for(int i=1;i<=MaxValue;i++)
            {
                list.Add(i);
            }
            list = Aratosfen(list.ToArray());
            list.RemoveAll(i => i == 1);

            //Console.WriteLine("Простые числа:");
            //foreach (int i in list)
            //{
            //    Console.WriteLine(i);
            //}
            Console.WriteLine("Задача заверешена");
        }

        public static List<int> Aratosfen(int[] array)
        {
            int element = (int)Math.Sqrt(array.Length);
            for(int i=2;i<=element;i++)
            {
                for(int j=i;j<array.Length;j++)
                {
                    if(array[j]%i==0)
                    {
                        array[j] = 1;
                    }
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Операция прервана");
                        return array.ToList();
                    }
                }
            }
            return array.ToList();
        }

        #endregion

        #region Task 6
        public static void First()
        {
            Console.WriteLine("Началась 1 задача");
            Thread.Sleep(7000);
            Console.WriteLine("Закончилась 1 задача");
        }
        public static void Second()
        {
            Console.WriteLine("Началась 2 задача");
            Thread.Sleep(5000);
            Console.WriteLine("Закончилась 2 задача");
        }
        public static void Third()
        {
            Console.WriteLine("Началась 3 задача");
            Thread.Sleep(10000);
            Console.WriteLine("Закончилась 3 задача");
        }
        #endregion
    }
}
