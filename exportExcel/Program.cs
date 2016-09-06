using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExportDataSetToExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            //Create an Emplyee DataTable
            DataTable employeeTable = new DataTable("Employee");
            employeeTable.Columns.Add("Employee ID");
            employeeTable.Columns.Add("Employee Name");
            employeeTable.Rows.Add("1", "ABC");
            employeeTable.Rows.Add("2", "DEF");
            employeeTable.Rows.Add("3", "PQR");
            employeeTable.Rows.Add("4", "XYZ");

            //Create a Department Table
            DataTable departmentTable = new DataTable("Department");
            departmentTable.Columns.Add("Department ID");
            departmentTable.Columns.Add("Department Name");
            departmentTable.Rows.Add("1", "IT");
            departmentTable.Rows.Add("2", "HR");
            departmentTable.Rows.Add("3", "Finance");

            //Create a DataSet with the existing DataTables
            DataSet ds = new DataSet("Organization");
            ds.Tables.Add(employeeTable);
            ds.Tables.Add(departmentTable);

            p.ExportDataSetToExcel(ds);
        }

        /// <summary>
        /// This method takes DataSet as input paramenter and it exports the same to excel
        /// </summary>
        /// <param name="ds"></param>
        private void ExportDataSetToExcel(DataSet ds)
        {
            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"C:\Users\DEMAR\Documents\Visual Studio 2012\Projects\exportExcel\filesTest\Test.xlsx");

            foreach (DataTable table in ds.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();

        }
    }
}