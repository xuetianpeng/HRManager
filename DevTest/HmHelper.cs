using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevTest
{
    public static class HmHelper
    {
        public static bool FindItem(ComboBox com ,string item)
        {
            for (int i = 0; i < com.Items.Count; i++) 
            {
                if (((V_Employees)com.Items[i]).姓名 == item) 
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Logined = false;
        public static T_Employees Emp = null;

        public static DateTime GetStartTime(DateTime dt) 
        {
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            return (new DateTime(year, month, day, 0, 0, 0));
        }

        public static DateTime GetEndTime(DateTime dt) 
        {
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            return (new DateTime(year, month, day, 23, 59, 59));
        }

        public static int[] GetFandA(string x) 
        {
            int[] y = new int[2];
            y[0] = Convert.ToInt32(x.Split(',')[0]);
            y[1] = Convert.ToInt32(x.Split(',')[1]);
            return y;
        }

    }
}
