using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;

namespace DevTest
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        bool HasLast = true;
        bool HasLastID = true;
        string Manager_ID = "";
        int U_ID = 0;
        T_EmployessPw emplPw = null;
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            var ep = from empp in hm.T_EmployessPw select empp;
            comboBox1.DataSource = ep;
            comboBox1.DisplayMember = "员工姓名";
            comboBox1.ValueMember = "密码";
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["LastLogin"] == null)
            {
                HasLast = false;
            }
            else 
            {
                Manager_ID = config.AppSettings.Settings["LastLogin"].Value;
                if (Manager_ID == "")
                {
                    HasLastID = false;
                }
                else 
                {
                    try
                    {
                        U_ID = Convert.ToInt32(Manager_ID);
                    }
                    catch 
                    {
                        U_ID = 0;
                    }
                    if (U_ID == 0)
                    {
                        HasLastID = false;
                    }
                    else 
                    {
                        emplPw = ep.Single(o => o.员工编号 == U_ID);
                        if (emplPw == null)
                        {
                            HasLastID = false;
                        }
                        else 
                        {
                            comboBox1.Text = emplPw.员工姓名;
                        }
                    }
                }
            }
            
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (HmHelper.Logined)
            {
                this.Close();
            }
            else 
            {
                Application.Exit();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HMMangeDataContext hm =new HMMangeDataContext();
            if (emplPw == null)
            {
                if (textEdit1.Text == comboBox1.SelectedValue.ToString())
                {
                    HmHelper.Logined = true;
                    HmHelper.Emp = (from em in hm.T_Employees where em.员工编号 == ((T_EmployessPw)comboBox1.SelectedItem).员工编号 select em).SingleOrDefault();
                    Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    if (!HasLast)
                    {

                        config.AppSettings.Settings.Add("LastLogin", HmHelper.Emp.员工编号.ToString());
                    }
                    config.AppSettings.Settings["LastLogin"].Value = HmHelper.Emp.员工编号.ToString();
                    config.Save();
                    this.Close();
                }
            }
            else
            {
                if (comboBox1.Text == emplPw.员工姓名)
                {
                    if (textEdit1.Text == emplPw.密码)
                    {
                        HmHelper.Logined = true;
                        HmHelper.Emp = (from em in hm.T_Employees where em.员工编号 == ((T_EmployessPw)comboBox1.SelectedItem).员工编号 select em).SingleOrDefault();
                        Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        if (!HasLast)
                        {

                            config.AppSettings.Settings.Add("LastLogin", HmHelper.Emp.员工编号.ToString());
                        }
                        config.AppSettings.Settings["LastLogin"].Value = HmHelper.Emp.员工编号.ToString();
                        config.Save();
                        this.Close();
                    }
                }
                else
                {
                    if (textEdit1.Text == comboBox1.SelectedValue.ToString())
                    {
                        HmHelper.Logined = true;
                        HmHelper.Emp = (from em in hm.T_Employees where em.员工编号 == ((T_EmployessPw)comboBox1.SelectedItem).员工编号 select em).SingleOrDefault();
                        Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        if (!HasLast)
                        {

                            config.AppSettings.Settings.Add("LastLogin", HmHelper.Emp.员工编号.ToString());
                        }
                        config.AppSettings.Settings["LastLogin"].Value = HmHelper.Emp.员工编号.ToString();
                        config.Save();
                        this.Close();
                    }

                }
            }
        }
    }
}