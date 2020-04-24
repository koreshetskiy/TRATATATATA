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
        private int schet = 0, k;
        private int n;
        private int u;
        private int celX, celY;
        public int S;
        public int V;
        private int UR;
        private int[] hvostX = new int[100];// Массив для хвоста с значением Х
        private int[] hvostY = new int[100];// Массив для хвоста с значением У, а как без 2 - х массивов сделать, если у меня значения X и Y разные.......
        private int nomerhv;
        Random R = new Random();
        ConsoleKeyInfo knopki;
        public bool Setup(bool gameOver)// Заданые переменные 
        {
            gameOver = false;// Игра закончена = ложь
            string s = Console.ReadLine();
            do
            {
                if (Int32.TryParse(s, out k))
                {
                    if (k == 1 | k == 2 | k == 3)
                    {
                        break; // Проверка на введенные данные...............
                    }
                    else
                    {
                        Console.WriteLine("От 1 до 3");
                        Setup(gameOver == false);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Не число");
                    Setup(gameOver == false);
                    break;
                }
            }
            while (true);
            UR = k;
            if (UR == 1)
            {
                S = 10;
                V = 10;
            }
            if (UR == 2)
            {
                S = 15;
                V = 15;
            }
            if (UR == 3)
            {
                S = 20;
                V = 20;
            }
            n = S / 2 - 1;
            u = V / 2 - 1;
            celX = R.Next(3, S - 2);// Рандомное значения фрукты
            celY = R.Next(3, S - 2);// Рандомное значения фрукты
            return gameOver;// Возращение значения игры
        }
        public void Draw()// Карта и персонажи
        {
            if (S == 10 && V == 10)
                Thread.Sleep(200);
            if (S == 15 && V == 15)
                Thread.Sleep(100);
            if (S == 20 && V == 20)
                Thread.Sleep(50);
            Console.WriteLine();
            Console.SetCursorPosition(0, 8);// Позиция курсора
            for (int i = 0; i < V + 1; i++)
                Console.Write("#");// Верхняя граница
            Console.WriteLine();

            for (int i = 0; i < S; i++)
            {
                for (int j = 0; j < S; j++)
                {
                    if (j == 0 || j == S - 1)
                        Console.Write((char)47);// Боковые границы
                    if (i == u && j == n)
                        Console.Write((char)79); // Выдаст O// Голова змейки
                    else if (i == celY && j == celX)
                        Console.Write((char)70); // Выдаст F// Расположение фрукты
                    else
                    {
                        bool print = false;
                        for (int k = 0; k < nomerhv; k++)
                        {
                            if (hvostX[k] == j && hvostY[k] == i)
                            {
                                print = true;
                                Console.Write((char)79); // Выдаст O// Рисование хвоста
                            }
                        }
                        if (!print)
                            Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            for (int j = 0; j < S + 1; j++)
                Console.Write("#");// Нижняя граница
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Количество баллов: " + schet);
            Console.WriteLine();
            Console.WriteLine("Задача игры: Вы должны съесть как можно много фрукта 'F'");
            Console.WriteLine("Выше отображается количество баллов, за каждый съеданный фрукт +10 балла");
        }
        public void Input_Logic()
        {
            int predznX = hvostX[0];// Предудыщий значение хвоста по Х
            int predznY = hvostY[0];// Предудыщий значение хвоста по Y
            int pred2X;
            int pred2Y;
            hvostX[0] = n;// Значение хвоста Х = значение Х
            hvostY[0] = u;// Значение хвоста У = значение У
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
        public void dvigenie()
        {
            if (Console.KeyAvailable == true)
            { knopki = Console.ReadKey(true); }
            switch (knopki.Key)
            {
                case ConsoleKey.UpArrow:
                    u--;//Вверх
                    break;

                case ConsoleKey.DownArrow:
                    u++;//Вниз
                    break;

                case ConsoleKey.RightArrow:
                    n++;//Вправо
                    break;

                case ConsoleKey.LeftArrow:
                    n--;//Влево
                    break;
            }
        }
        public void itog()
        {
            if (n > S)//
                n = 0;//
            else if (n < 0)//
                n = S - 2;// Выход за границы поля и обратное появление с другой стороны
            if (u > V)//
                u = 0;//
            else if (u < 0)//
                u = V - 2;//
            for (int g = 0; g < nomerhv; g++)
            {
                if (hvostX[g] == n && hvostY[g] == u)
                {
                    gameOver = true;
                    Console.WriteLine("Вы проиграли!!!");
                    Console.WriteLine("Нажмите ENTER");
                    Thread.Sleep(2000);
                    Console.ReadKey();
                }
            }
            if (gameOver == true)
            {
                Console.Clear();
                {
                    Console.WriteLine("Выберите и введите номер:");
                    Class1 class1 = new Class1();
                    class1.Setup(class1.gameOver);
                    while (!class1.gameOver)
                    {
                        class1.Draw();
                        class1.Input_Logic();
                        class1.dvigenie();
                        class1.itog();
                    }
                    Console.ReadKey(true);
                }
            }
            if (n == celX && u == celY)
            {
                schet += 10;// Суммирование баллов
                celX = R.Next(3, S - 2);
                celY = R.Next(3, S - 2);
                nomerhv++;
            }
        }
    }
}