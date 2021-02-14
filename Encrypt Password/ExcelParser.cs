using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Encrypt_Password
{
    class ExcelParser
    {
        static void Main3(string[] args)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\hp\Downloads\Tally (1).xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            List<Product> products = new List<Product>();

            for (int i = 2; i <= rowCount; i++)
            {
                Product product = new Product();
                product.Name = xlRange.Cells[i, 2].Value2.ToString();
                product.PrintName = xlRange.Cells[i, 3].Value2.ToString();
                product.Barcode = xlRange.Cells[i, 4].Value2.ToString();
                product.Cost = xlRange.Cells[i, 5].Value2.ToString();
                product.MRP = xlRange.Cells[i, 6].Value2.ToString();

                products.Add(product);

                if (product.Barcode.Equals("706"))
                {
                    break;
                }
            }

            using (SQLiteConnection dbConnection = new SQLiteConnection("F://Products.db"))
            {
                try
                {
                    dbConnection.CreateTable<Product>();
                    foreach (Product product in products)
                    {
                        dbConnection.Insert(product);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
