using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevTest
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            if (HmHelper.Emp.职位编号 == 1 || HmHelper.Emp.备注.Contains("行政") || HmHelper.Emp.备注.Contains("会计"))
            {
                HMMangeDataContext hm = new HMMangeDataContext();
                var depts = from dept in hm.V_Department select dept;
                var post = from pos in hm.T_Post select pos;
                comboBox1.DataSource = depts;
                comboBox2.DataSource = post;
                comboBox1.DisplayMember = "部门名称";
                comboBox1.ValueMember = "部门编号";
                comboBox2.DisplayMember = "职位名称";
                comboBox2.ValueMember = "职位编号";
                if (toggleSwitch1.IsOn)
                {
                    var emps = from emp in hm.V_Employees orderby emp.部门 select emp;
                    gridControl1.DataSource = emps;
                }
                else
                {
                    var emps = from emp in hm.V_Employees where emp.离职日期 != null orderby emp.部门 select emp;
                    gridControl1.DataSource = emps;
                }
            }
            else
            {
                
                HMMangeDataContext hm = new HMMangeDataContext();
                string deptname = (from dep in hm.T_Department where dep.部门编号 == HmHelper.Emp.部门编号 select dep).SingleOrDefault().部门名称;
                string postname = (from pst in hm.T_Post where pst.职位编号 == HmHelper.Emp.职位编号 select pst).SingleOrDefault().职位名称;
                comboBox1.Text = deptname;
                comboBox1.SelectedText = deptname;
                comboBox1.SelectedValue = HmHelper.Emp.部门编号;
                comboBox2.Text = postname;
                comboBox2.SelectedText = postname;
                comboBox2.SelectedValue = HmHelper.Emp.职位编号;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                
                if (HmHelper.Emp.职位编号 == 2 || HmHelper.Emp.职位编号 == 3)
                {
                    var emps = from emp in hm.V_Employees where emp.部门==deptname orderby emp.员工编号 select emp;
                    gridControl1.DataSource = emps;
                }
                else 
                {
                    var emps = from emp in hm.V_Employees where emp.员工编号==HmHelper.Emp.员工编号 orderby emp.员工编号 select emp;
                    gridControl1.DataSource = emps;
                }
                
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            string emp_str = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "员工编号");
            HMMangeDataContext hm = new HMMangeDataContext();
            var emps = from emp in hm.V_Employees where emp.员工编号 == Convert.ToInt32(emp_str) select emp;
            var em = emps.FirstOrDefault();
            textEdit1.Text = em.姓名.ToString();
            textEdit3.Text = em.员工编号.ToString();
            textEdit2.Text = em.手机 == null ? "" : em.手机;
            comboBox1.Text = em.部门;
            comboBox2.Text = em.职务;
            textEdit4.Text = em.津贴.ToString();
            textEdit5.EditValue = em.入职日期;
            if (Convert.ToDateTime(textEdit5.EditValue) == Convert.ToDateTime("1900-01-01"))
            {
                textEdit5.Text = "";
            }
            textEdit6.EditValue = em.离职日期 == null ? Convert.ToDateTime("1900-01-01") : em.离职日期;
            if (Convert.ToDateTime(textEdit6.EditValue) == Convert.ToDateTime("1900-01-01")) 
            {
                textEdit6.Text = "";
            }
            textEdit7.Text = em.身份证 == null ? "" : em.身份证;
            textEdit9.Text = em.备注 == null ? "" : em.备注;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (HmHelper.Emp.职位编号 == 1 || HmHelper.Emp.备注.Contains("行政") || HmHelper.Emp.备注.Contains("会计"))
            {
                if (textEdit3.Text == "")
                {
                    //添加
                    HMMangeDataContext hm = new HMMangeDataContext();
                    T_Employees emp = new T_Employees();
                    emp.姓名 = textEdit1.Text;
                    emp.部门编号 = Convert.ToInt32(comboBox1.SelectedValue);
                    emp.职位编号 = Convert.ToInt32(comboBox2.SelectedValue);
                    emp.入职时间 = DateTime.Now;
                    emp.津贴 = Convert.ToDecimal(textEdit4.Text == "" ? "0" : textEdit4.Text);
                    emp.手机 = textEdit2.Text;
                    emp.身份证 = textEdit7.Text;
                    emp.备注 = textEdit9.Text;
                    hm.T_Employees.InsertOnSubmit(emp);
                    hm.SubmitChanges();

                }
                else
                {
                    //修改
                    HMMangeDataContext hm = new HMMangeDataContext();
                    var em = from ed in hm.T_Employees where ed.员工编号 == Convert.ToInt32(textEdit3.Text) select ed;
                    var emp = em.First();
                    emp.姓名 = textEdit1.Text;
                    emp.部门编号 = Convert.ToInt32(comboBox1.SelectedValue);
                    emp.职位编号 = Convert.ToInt32(comboBox2.SelectedValue);
                    emp.入职时间 = DateTime.Now;
                    emp.津贴 = Convert.ToDecimal(textEdit4.Text == "" ? "0" : textEdit4.Text);
                    emp.手机 = textEdit2.Text;
                    emp.身份证 = textEdit7.Text;
                    emp.备注 = textEdit9.Text;
                    //hm.T_Employees.InsertOnSubmit(emp);
                    hm.SubmitChanges();
                }
            }
            else 
            {
                if (textEdit3.Text == "")
                {
                    ////添加
                    //HMMangeDataContext hm = new HMMangeDataContext();
                    //T_Employees emp = new T_Employees();
                    //emp.姓名 = textEdit1.Text;
                    //emp.部门编号 = Convert.ToInt32(comboBox1.SelectedValue);
                    //emp.职位编号 = Convert.ToInt32(comboBox2.SelectedValue);
                    //emp.入职时间 = DateTime.Now;
                    //emp.津贴 = Convert.ToDecimal(textEdit4.Text == "" ? "0" : textEdit4.Text);
                    //emp.手机 = textEdit2.Text;
                    //emp.身份证 = textEdit7.Text;
                    //emp.备注 = textEdit9.Text;
                    //hm.T_Employees.InsertOnSubmit(emp);
                    //hm.SubmitChanges();

                }
                else
                {
                    //修改
                    HMMangeDataContext hm = new HMMangeDataContext();
                    var em = from ed in hm.T_Employees where ed.员工编号 == Convert.ToInt32(textEdit3.Text) select ed;
                    var emp = em.First();
                    if (emp.员工编号 == HmHelper.Emp.员工编号)
                    {
                        emp.姓名 = textEdit1.Text;
                        emp.部门编号 = Convert.ToInt32(comboBox1.SelectedValue);
                        emp.职位编号 = Convert.ToInt32(comboBox2.SelectedValue);
                        emp.入职时间 = DateTime.Now;
                        emp.津贴 = Convert.ToDecimal(textEdit4.Text == "" ? "0" : textEdit4.Text);
                        emp.手机 = textEdit2.Text;
                        emp.身份证 = textEdit7.Text;
                        emp.备注 = textEdit9.Text;
                        //hm.T_Employees.InsertOnSubmit(emp);
                        hm.SubmitChanges();
                    }
                    else 
                    {
                         
                    }
                }
            }
        }

        private void toggleSwitch1_Properties_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn)
            {
                HMMangeDataContext hm = new HMMangeDataContext();
                var emps = from emp in hm.V_Employees orderby emp.部门 select emp;
                gridControl1.DataSource = emps;
            }
            else 
            {
                HMMangeDataContext hm = new HMMangeDataContext();
                var emps = from emp in hm.V_Employees where emp.离职日期!=null orderby emp.部门 select emp;
                gridControl1.DataSource = emps;
            }
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            textEdit1.Tag = "";
        }

    }
}
