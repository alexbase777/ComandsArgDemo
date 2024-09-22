using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandsArgDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!DBWork.IfDBExists()) // Проверка существования БД
                DBWork.MakeBD(); // Создаём БД
            else // Если БД существует, то пользователь выбирает, что с ней делать
            {                
                Console.Write("База данных существует.\nНажмите '1' для перезаписи существующей базы данных\n" +
                    "Нажмите любую другую клавишу для дополнения базы данных.\nВаш выбор -> ");             
                char symbol = Convert.ToChar(Console.ReadLine());
                if (symbol == '1') // Если выбрана опция для перезаписи
                {
                    DBWork.DropTable();
                    DBWork.MakeBD();
                }
            }
            foreach (string el in args) // Цикл перебора аргументов командной строки
            {                
                Console.Write("Введите имя хобита -> "); // Ввод имён
                DBWork.InitDB(el, Console.ReadLine()); // Заполнение таблицы БД 
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nИтоговая база данных:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Аргумент cmd\tИмя\tДата и время");
            Console.ForegroundColor = ConsoleColor.White;
            int cnt = DBWork.Count_Rows_DB(); // Переменная для хранения кол-ва строк БД (отдельная переменная,
                                              // чтобы в каждой итерации цикла не пересчитывать COUNT)
            for (int i = 1; i <= cnt; ++i) // Начинаем с id = 1
                Console.WriteLine(DBWork.GetData(i, "number_arg") + "\t\t" + DBWork.GetData(i, "name") + "\t" + DBWork.GetData(i, "date_time"));
        }
    }
}
