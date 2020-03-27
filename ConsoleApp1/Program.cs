using GAME;
using System;
using System.Threading;

namespace GAME
{
    class Program
    {
        static void Main(string[] args)
        {
Console.WriteLine("Выберите и напишите размер поля N x N");
            Console.WriteLine("Легкий уровень [поле10х10, скорость медленная]");
            Console.WriteLine("Средний уровень [поле15х15, скорость нормальная]");
            Console.WriteLine("Сложный уровень [поле20х20, скорость быстрая]");
            Class1 class1 = new Class1();
            class1.Setup(class1.gameOver);
            {
                while (!class1.gameOver)
                class1.Draw();//рисование
                class1.Input_Logic();//ввод логики
                class1.dvijenie();//движение
                class1.result();//результат
                Thread.Sleep(100);// Замедление консоли
            }          
            Console.ReadKey();
        }
    }
}
