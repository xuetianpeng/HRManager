using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace DevTest
{
    public static class ExcelHelper
    {
        public static string GetCellString(ICell cell) 
        {
            if (cell == null)
                return "";
            switch (cell.CellType) 
            {
                case CellType.String:
                    return cell.StringCellValue == "" ? "" : cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString() == "" ? "" :
                        DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue.ToString() : cell.NumericCellValue.ToString();
                case CellType.Blank:
                    return "";
                case CellType.Boolean:
                    return cell.BooleanCellValue ? "1" : "0";
                default:
                    return "";
            }
        }
    }
}
