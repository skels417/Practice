using System.Collections.Generic;
using System.Data;
using System;
using System.IO;
using System.Linq;

namespace Task5.Classes
{
    public class InterpreterData
    {
        private Dictionary<string, DeviceData> _configuration;

        public InterpreterData(Dictionary<string, DeviceData> configuration)
        {
            _configuration = configuration;
        }

        public object Values { get; internal set; }

        public DataTable InterpretData(string csvFilePath)
        {
            var dataTable = new DataTable();
            //  колонка "Время" в DataTable
            dataTable.Columns.Add("Время", typeof(TimeSpan)); // Используем TimeSpan для хранения времени

            // Используем HashSet для отслеживания имен столбцов
            var columnNames = new HashSet<string> { "Время" };

            // Чтение CSV файла и интерпретация данных
            var lines = File.ReadAllLines(csvFilePath);
            foreach (var line in lines.Skip(1)) // Пропускаем заголовок
            {
                var values = line.Split(';');

                // Удаляем пробелы и проверяем формат времени
                if (TimeSpan.TryParse(values[0].Trim(), out TimeSpan time))
                {
                    var row = dataTable.NewRow();
                    row["Время"] = time;

                    // Добавляем данные из других столбцов
                    for (int i = 1; i < values.Length; i++)
                    {
                        string columnName = $"Столбец_{i}"; // Создаем уникальное имя столбца
                        if (!columnNames.Contains(columnName))
                        {
                            dataTable.Columns.Add(columnName, typeof(double));
                            columnNames.Add(columnName);
                        }

                        // Преобразуем значение и добавляем в строку
                        if (double.TryParse(values[i], out double value))
                        {
                            row[columnName] = value;
                        }
                        else
                        {
                            row[columnName] = DBNull.Value; 
                        }
                    }

                    dataTable.Rows.Add(row);
                }
                else
                {
                    Console.WriteLine($"Неверный формат времени: {values[0]}");
                }
            }

            return dataTable;
        }
    }
}