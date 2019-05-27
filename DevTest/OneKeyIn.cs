using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace DevTest
{
    public partial class OneKeyIn : DevExpress.XtraEditors.XtraForm
    {
        public OneKeyIn()
        {
            InitializeComponent();
        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            openFileDialog1.Filter = "Excel表格 97~2003(*.xls)|*.xls|Excel表格 2007(*.xlsx)|.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                buttonEdit1.Text = openFileDialog1.FileName;
            }
            string fileName = openFileDialog1.FileName;
            IWorkbook workbook = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                comboBoxEdit1.Properties.Items.Clear();
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    comboBoxEdit1.Properties.Items.Add(workbook.GetSheetName(i));
                }
            }
            catch 
            {
                MessageBox.Show("错误！", "文件正被另一进程占用，请关闭后重新");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int add, err, has;
            add = err = has = 0;
            if (buttonEdit1.Text != "")
            {
                string fileName = openFileDialog1.FileName;
                IWorkbook workbook = null;
                FileStream fs = null;
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheet(comboBoxEdit1.SelectedText);
                IRow firstRow = sheet.GetRow(1);
                int cellCount = firstRow.LastCellNum;
                int cellRow = sheet.LastRowNum;
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.Properties.Maximum = cellRow;
                progressBarControl1.Position = 0;
                progressBarControl1.Properties.ShowTitle = true;
                progressBarControl1.Properties.PercentView = true;  
                switch (radioGroup1.SelectedIndex)
                {
                    case 0:
                        for (int l = 1; l <= cellRow; l++)
                        {
                            try
                            {
                                IRow Row = sheet.GetRow(l);
                                HMMangeDataContext hm = new HMMangeDataContext();
                                T_ReSume Resume = new T_ReSume();
                                Resume.姓名 = ExcelHelper.GetCellString(Row.GetCell(0));
                                Resume.男 = ExcelHelper.GetCellString(Row.GetCell(1)) == "1" ? true : false;
                                Resume.年龄 = ExcelHelper.GetCellString(Row.GetCell(2)) == "" ? new DateTime(1900, 1, 1) :
                                    new DateTime((DateTime.Now.Year - Convert.ToInt32(Row.GetCell(2).StringCellValue)), 1, 1);
                                Resume.简历类型 = Convert.ToInt32(ExcelHelper.GetCellString(Row.GetCell(3)));
                                Resume.应聘职位 = ExcelHelper.GetCellString(Row.GetCell(4));
                                Resume.期待工资 = ExcelHelper.GetCellString(Row.GetCell(5));
                                Resume.现在职位 = ExcelHelper.GetCellString(Row.GetCell(6));
                                Resume.工作经验 = ExcelHelper.GetCellString(Row.GetCell(7));
                                Resume.手机 = ExcelHelper.GetCellString(Row.GetCell(8));
                                Resume.电子邮箱 = ExcelHelper.GetCellString(Row.GetCell(9));
                                Resume.居住地 = ExcelHelper.GetCellString(Row.GetCell(10));
                                Resume.现在公司 = ExcelHelper.GetCellString(Row.GetCell(11));
                                Resume.学历 = ExcelHelper.GetCellString(Row.GetCell(12));
                                Resume.学校 = ExcelHelper.GetCellString(Row.GetCell(13));
                                Resume.专业 = ExcelHelper.GetCellString(Row.GetCell(14));
                                Resume.应聘日期 = Convert.ToDateTime(ExcelHelper.GetCellString(Row.GetCell(15)));
                                var res = (from re in hm.T_ReSume
                                           where re.姓名 == Resume.姓名 && re.手机 == Resume.手机 && re.部门编号 == HmHelper.Emp.部门编号
                                           select re).Count();
                                if (res > 0)
                                {
                                    has++;
                                }
                                else
                                {
                                    hm.T_ReSume.InsertOnSubmit(Resume);
                                    hm.SubmitChanges();
                                    add++;
                                }
                            }
                            catch
                            {
                                err++;
                            }
                            Application.DoEvents();
                            progressBarControl1.Position += 1;
                        }
                        
                        labelControl1.Text = "操作结果：" + "\r\n" + add.ToString() + "行被添加" + "\r\n" +
                            err.ToString() + "行内容错误无法导入" + "\r\n" + has.ToString() + "行重复，无需导入";
                        break;
                    case 1:

                        break;
                    case 2:

                        break;
                }
            }
            else 
            {
                MessageBox.Show("提示!", "请选择对应的简历文件。", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OneKeyIn_Load(object sender, EventArgs e)
        {
            
        }

        void LoadExcel() 
        {

        }
        void InsertSql() 
        {

        }
    }
}