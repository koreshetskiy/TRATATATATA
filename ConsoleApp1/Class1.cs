using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace GAME
{
    class Class1
    {
        public bool gameOver = false;
        int schet = 0;
        int u;
        int n;
        int celX, celY;
        const int E = 20;
        const int P = 20;
        int[] hvostX = new int[1000];// Массив для хвоста со значением X
        int[] hvostY = new int[1000];// Массив для хвоста со значением У
        int nomerhv;
        Random R = new Random();
        ConsoleKeyInfo knopki;
        public bool Setup(bool gameOver)//Переменные 
        {
            gameOver = false;// Игра = ложь
            u = E / 2 - 1;
            n = P / 2 - 1;
            celX = R.Next(3, E - 2);// Рандомное значения фрукты
            celY = R.Next(3, E - 2);// Рандомное значения фрукты
            Console.WriteLine();
            return gameOver;// Возращение значения игры
        }
        public void Draw()//Карта и персонажи
        {
            if (E == 10 && P == 10)
                Thread.Sleep(200);
            if (E == 15 && P == 15)
                Thread.Sleep(100);
            if (E >= 20 && P >= 20)
                Thread.Sleep(50);
            Console.WriteLine();
            Console.SetCursorPosition(0, 0);// Позиция курсора
            Console.WriteLine("Управление змейкой осуществляется на стрелочки");
            for (int i = 0; i < P + 1; i++)
                Console.Write("%");// Верхняя граница
            Console.WriteLine();

            for (int i = 0; i < E; i++)
            {
                for (int j = 0; j < E; j++)
                {
                    if (j == 0 || j == E - 1)
                        Console.Write("%");// Боковые границы
                    if (i == n && j == u)
                        Console.Write("@");// Голова змейки
                    else if (i == celY && j == celX)
                        Console.Write("O");// Расположение фрукты
                    else
                    {
                        bool print = false;
                        for (int k = 0; k < nomerhv; k++)
                        {
                            if (hvostX[k] == j && hvostY[k] == i)
                            {
                                print = true;
                                Console.Write("o");// Рисование хвоста
                            }
                        }
                        if (!print)
                            Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            for (int j = 0; j < E + 1; j++)
                Console.Write("%");// Нижняя граница
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Баллы: " + schet);
            Console.WriteLine();
            Console.WriteLine("Задача игры: Ты должен съесть как можно много фрукта 'O'");
            Console.WriteLine("Выше отображается количество баллов, за каждый съеденный фрукт +10 балла");
        }
        public void Input_Logic()//ввод логики
        {
            int predznX = hvostX[0];// Предыдущее значение хвоста
            int predznY = hvostY[0];
            int pred2X;
            int pred2Y;
            hvostX[0] = u;// Значение хвоста U = значение U
            hvostY[0] = n;// Значение хвоста N = значение N
            for (int i = 1; i < nomerhv; i++)
            {
                pred2X = hvostX[i];
                pred2Y = hvostY[i];
                hvostX[i] = predznX;
                hvostY[i] = predznY;
                predznX = pred2X;
                predznY = pred2Y;
            }
        }
        public void dvijenie()//движение
        {
            if (Console.KeyAvailable == true)
            { knopki = Console.ReadKey(true); }
            switch (knopki.Key)
            {
                case ConsoleKey.UpArrow:
                    n--;//Вверх
                    break;

                case ConsoleKey.DownArrow:
                    n++;//Вниз
                    break;

                case ConsoleKey.RightArrow:
                    u++;//Вправо
                    break;

                case ConsoleKey.LeftArrow:
                    u--;//Влево
                    break;
            }
        }
        public void result()//результат
        {
            if (u > E)
                u = 0;
            else if (u < 0)
                u = E - 2;// Выход за границы поля и обратное появление с другой стороны
            if (n > P)
                n = 0;
            else if (n < 0)
                n = P - 2;
            for (int g = 0; g < nomerhv; g++)
            {
                if (hvostX[g] == u && hvostY[g] == n)
                {
                    gameOver = true;
                    Console.WriteLine("GAME OVER!!!");
                    Console.WriteLine("Выберите и напишите размер поля N x N");
                    Console.WriteLine("Нажмите ENTER");
                }
            }
            if (gameOver != false)
            {
                Console.Clear();
                {
                    Class1 class1 = new Class1();
                    class1.Setup(class1.gameOver);
                    while (!class1.gameOver)
                    {
                        class1.Draw();//рисование
                        class1.Input_Logic();//ввод логики
                        class1.dvijenie();//движение
                        class1.result();//результат
                        Thread.Sleep(100);// Замедление консоли
                    }
                    Console.ReadKey(true);
                }
            }
            if (u == celX && n == celY)
            {
                schet += 10;// Суммирование баллов
                celX = R.Next(5, 17);
                celY = R.Next(5, 17);
                nomerhv++;
            }
        }
    }
}
