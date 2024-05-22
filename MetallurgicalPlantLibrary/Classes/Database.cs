using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Microsoft.Data.Sqlite;

namespace MetallurgicalPlant
{
    public class Database
    {
        
        private static Database database; 

        private Database() { }

        /// <summary>
        /// Функция для получения объекта базы данных
        /// </summary>
        /// <returns>Объект класса Database</returns>
        public static Database GetDatabase()
        {
            if (database == null)
            {
                database = new Database();
            }
            return database;
        }

        /// <summary>
        /// Функция для получения списка работников из базы данных
        /// </summary>
        /// <returns>Коллекция List с объектами типа Worker</returns>
        public List<Worker> GetListOfWorkers()
        {
            List<Worker> workers = new List<Worker>();
     
            using (var connection = new SqliteConnection("Data Source=MetallurgicalPlantLibrary.db"))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM Workers";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                //Используем класс SqliteDataReader для считывания данных по запросу Select
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    //Если у нас есть хоть какие-то строки, то у нас будет все считываться
                    if (reader.HasRows)
                    {

                        //И пока у нас программа считывает строки, мы с ними взаимодействуем
                        while (reader.Read())   
                        {
                            workers.Add(new Worker() { Id = reader.GetInt32(0), FullName = reader.GetString(1), Department = reader.GetString(2), Post = reader.GetString(3) });
                        }
                    }
                }

            }

            return workers;
        }

        /// <summary>
        /// Функция для получения списка складов из базы данных
        /// </summary>
        /// <returns>Коллекция List с объектами типа Warehouse</returns>
        public List<Warehouse> GetListOfWarehouses()
        {
            List<Warehouse> warehouses = new List<Warehouse>();

            using (var connection = new SqliteConnection("Data Source=MetallurgicalPlantLibrary.db"))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM Warehouses";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                //Используем класс SqliteDataReader для считывания данных по запросу Select
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    //Если у нас есть хоть какие-то строки, то у нас будет все считываться
                    if (reader.HasRows)
                    {

                        //И пока у нас программа считывает строки, мы с ними взаимодействуем
                        while (reader.Read())
                        {
                            warehouses.Add(new Warehouse() { Id = reader.GetInt32(0), ValueKP80 = reader.GetInt32(1), ValueKP90 = reader.GetInt32(2), ValueKP100 = reader.GetInt32(3) });
                        }
                    }
                }

            }

            return warehouses;
        }

        /// <summary>
        /// Метод для удаления записи из базы данных
        /// </summary>
        /// <param name="idRow">Значение ID удаляемой строки</param>
        /// <param name="nameTable">Название таблицы, в которой происходит удаление</param>
        public void DeleteRowFromTable(int idRow, string nameTable)
        {
            //Создание соединения с файлом БД (Указываем источник данных - нашу БД)
            using (var connection = new SqliteConnection("Data Source=MetallurgicalPlantLibrary.db"))
            {
                connection.Open();

                //Создание строки с запросом, который необходимо выполнить нашей БД
                string sqlExpression = $"DELETE FROM {nameTable} WHERE Id = {idRow}";

                //Создаем объект класса SqliteCommand для того, чтобы уже конкретно выполнить запрос и получить результат.
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                command = new SqliteCommand("VACUUM", connection);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Метод для добавления нового сотрудника в базу данных
        /// </summary>
        /// <param name="fullName">ФИО сотрудника</param>
        /// <param name="department">Отдел сотрудника</param>
        /// <param name="post">Должность сотрудника</param>
        public void AddNewWorker(string fullName, string department, string post)
        {
            using (var connection = new SqliteConnection("Data Source=MetallurgicalPlantLibrary.db"))
            {
                connection.Open();
                string sqlExpression = $"INSERT INTO Workers (FullName, Department, Post) VALUES ('{fullName}', '{department}', '{post}')";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Метод для добавления нового склада в базу данных
        /// </summary>
        /// <param name="valueKP80">Количество ресурсов KP80</param>
        /// <param name="valueKP90">Количество ресурсов KP90</param>
        /// <param name="valueKP100">Количество ресурсов KP100</param>
        public void AddNewWarehouse(int valueKP80, int valueKP90, int valueKP100)
        {
            using (var connection = new SqliteConnection("Data Source=MetallurgicalPlantLibrary.db"))
            {
                connection.Open();
                string sqlExpression = $"INSERT INTO Warehouses (ValueKP80, ValueKP90, ValueKP100) VALUES ({valueKP80}, {valueKP90}, {valueKP100})";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

            }
        }

        public void UpdateWarehousesTable(Warehouse warehouse)
        {

            int valueKP80Database = 0;
            int valueKP90Database = 0;
            int valueKP100Database = 0;
            using (var connection = new SqliteConnection("Data Source=MetallurgicalPlantLibrary.db"))
            {
                connection.Open();
                string sqlExpression = $"SELECT * FROM Warehouses WHERE Id = {warehouse.Id}";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                //Используем класс SqliteDataReader для считывания данных по запросу Select
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    //Если у нас есть хоть какие-то строки, то у нас будет все считываться
                    if (reader.HasRows)
                    {

                        //И пока у нас программа считывает строки, мы с ними взаимодействуем
                        while (reader.Read())
                        {
                           valueKP80Database = reader.GetInt32(1);
                            valueKP90Database = reader.GetInt32(2);
                            valueKP100Database = reader.GetInt32(3);
                        }
                    }
                }

                sqlExpression = $"UPDATE Warehouses SET ValueKP80=0, ValueKP90=0, ValueKP100=0 WHERE Id = {warehouse.Id}";
                command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

            }
        }
    }
}
