using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandsArgDemo
{
    internal class DBWork
    {
        static private string dbname = "Hobbits.db";
        static private string path = $"Data Source={dbname}";

        static public bool IfDBExists() // Метод проверки существования БД
        {            
            if (File.Exists(dbname)) return true;
            return false;
        }
        static public void MakeBD() // Метод создания БД
        {             
            MakeQuery("CREATE TABLE IF NOT EXISTS " +
                " Names (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                " number_arg VARCHAR, " +
                " name VARCHAR, " +
                "date_time TEXT);"); // Подключаемся к БД и выполняем запрос
        }
        static public void InitDB(string arg, string name) // Метод заполнения БД
        {            
            MakeQuery("INSERT INTO Names(number_arg, name, date_time) " +
                "VALUES " +
                $"('{arg}', '{name}', '{DateTime.Now.ToString("yyyyMMdd-HH:mm")}');"); // Подключаемся к БД и выполняем запрос
        }
        static public void DropTable() // Метод очистки содержимого таблицы
        {            
            MakeQuery("DROP TABLE Names;"); // Подключаемся к БД и выполняем запрос
        }
        static public string GetData(int id, string value) // Метод получения данных из БД
        {               
            string result = string.Empty;
            using (SQLiteConnection conn = new SQLiteConnection(path)) // Ресурс для подключения к БД
            {
                SQLiteCommand cmd = conn.CreateCommand();  // Создаём в ресурсе команду на выполнение запроса
                cmd.CommandText = $"SELECT {value} FROM Names WHERE id = {id};"; // Формируем конкретную команду
                conn.Open(); // Открываем соединение
                var reader = cmd.ExecuteReader(); // Переменная для считывания данных
                if (reader.HasRows) // Если reader имеет поля              
                    while (reader.Read()) // Читает значения и переходит на следующие  
                        result += reader.GetString(0); // Добавляет по строчке                
            }
            return result;
        }
        static public int Count_Rows_DB()
        {
            string result = string.Empty;
            using (SQLiteConnection conn = new SQLiteConnection(path)) // Ресурс для подключения к БД
            {
                SQLiteCommand cmd = conn.CreateCommand();  // Создаём в ресурсе команду на выполнение запроса
                cmd.CommandText = $"SELECT COUNT(name) FROM Names;"; // Формируем конкретную команду
                conn.Open(); // Открываем соединение
                var reader = cmd.ExecuteReader(); // Переменная для считывания данных
                if (reader.HasRows) // Если reader имеет поля              
                    while (reader.Read()) // Читает значения и переходит на следующие  
                        result = reader.GetValue(0).ToString(); // Добавляет по строчке                
            }
            return Int32.Parse(result);
        }
        // Общий метод для подключения к БД и выполнения запросов
        static private void MakeQuery(string query) 
        {
            SQLiteConnection conn = new SQLiteConnection(path); // Подключаемся к БД
            SQLiteCommand cmd = conn.CreateCommand(); // Создаём в подключении команду на выполнение запроса
            cmd.CommandText = query; // Формируем конкретную команду
            conn.Open(); // Открываем соединение
            cmd.ExecuteNonQuery(); // Выполянем команду
            conn.Close(); // Закрываем соединение
        }        
    }
}
