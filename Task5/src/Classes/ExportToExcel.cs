using OfficeOpenXml;
using System.Collections.Generic;
using System;
using System.IO;

namespace Task5
{
    public class ExportToExce
    {
        public static void ExportToExcel(List<InterpretedData> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                // Заполнение заголовков
                worksheet.Cells[1, 1].Value = "Time";
                int column = 2;
                foreach (var key in data[0].Values.Keys)
                {
                    worksheet.Cells[1, column++].Value = key;
                }

                // Заполнение данных
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.Time;
                    column = 2;
                    foreach (var value in item.Values.Values)
                    {
                        worksheet.Cells[row, column++].Value = value;
                    }
                    row++;
                }

                var filePath = "output.xlsx";
                File.WriteAllBytes(filePath, package.GetAsByteArray());
                Console.WriteLine($"Данные экспортированы в {filePath}");
            }
        }
    }
}