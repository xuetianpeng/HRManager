using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DevTest
{
    public partial class FrmDepart : DevExpress.XtraEditors.XtraForm
    {
        public FrmDepart()
        {
            InitializeComponent();
        }

        private void FrmDepart_Load(object sender, EventArgs e)
        {
            LoadDepart();
            LoadManager();
        }
        void LoadDepart() 
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            var depts = from d in hm.V_Department select d;
            gridControl1.DataSource = depts;
        }
        void LoadManager() 
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            var manager = from m in hm.V_Employees where m.职务 == "经理" select m;
            var a_manager = from m in hm.V_Employees where m.职务 == "经理" select m;
            comboBox1.DataSource = manager;
            comboBox2.DataSource = a_manager;
            comboBox1.DisplayMember = "姓名";
            comboBox1.ValueMember = "员工编号";
            comboBox2.DisplayMember = "姓名";
            comboBox2.ValueMember="员工编号";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        void LoadControl() 
        {
            HMMangeDataContext hm=new HMMangeDataContext();
            string bm = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "部门编号");
            var bms = from b in hm.V_Department where b.部门编号 == Convert.ToInt32(bm) select b;
            textEdit1.Text = bm;
            textEdit2.Text = bms.FirstOrDefault().部门名称;
            comboBox1.Text = bms.FirstOrDefault().经理 == null ? "" : bms.FirstOrDefault().经理;
            comboBox2.Text = bms.FirstOrDefault().辅助经理 == null ? "" : bms.FirstOrDefault().辅助经理;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            if (textEdit1.Text == "")
            {
                var dept = new T_Department { };
                if(textEdit2.Text=="")
                {
                    toolStripStatusLabel1.Text = "操作提示:名称空";
                    return;
                }
                else
                {
                    var dep = from de in hm.T_Department where de.部门名称 == textEdit2.Text select de;
                    if (dep.Count() > 0) //如果有重复名称的部门 则不可加入新的部门
                    {
                        toolStripStatusLabel1.Text = "操作提示:名称重复";
                        return;
                    }
                    else
                    {
                        dept.部门名称 = textEdit2.Text;
                        if (comboBox1.Text != "")
                        {
                            if (HmHelper.FindItem(comboBox1, comboBox1.Text))
                            {
                                dept.部门经理 = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                            }
                            else 
                            {
                                toolStripStatusLabel1.Text = "操作提示:无此经理";
                                return;
                            }                            
                        }
                        if (comboBox2.Text != "")
                        {
                            if (HmHelper.FindItem(comboBox2, comboBox2.Text))
                            {
                                dept.部门经理 = Convert.ToInt32(comboBox2.SelectedValue.ToString());
                            }
                            else
                            {
                                toolStripStatusLabel1.Text = "操作提示:无此经理";
                                return;
                            }  
                            
                        }
                        hm.T_Department.InsertOnSubmit(dept);
                        hm.SubmitChanges();
                        //修改经理归属部门
                        if (comboBox1.Text != "")
                        {
                            var emp = from em in hm.T_Employees where em.员工编号 == comboBox1.SelectedIndex select em;
                            var newdept = from nd in hm.T_Department where nd.部门名称 == textEdit2.Text select nd;
                            if (emp != null)
                            {
                                emp.FirstOrDefault().部门编号 = newdept.FirstOrDefault().部门编号;
                                hm.SubmitChanges();
                            }
                        }
                    }
                }
            }
            else 
            {
                if (textEdit2.Text == "")
                {
                    toolStripStatusLabel1.Text = "操作提示:名称空";
                    return;
                }
                else
                {
                    var dept = from dep in hm.T_Department where dep.部门名称 == textEdit2.Text && dep.部门编号 !=Convert.ToInt32( textEdit1.Text) select dep;
                    if (dept.Count() > 0)
                    {
                        toolStripStatusLabel1.Text = "操作提示:名称重复";
                        return;
                    }
                    else 
                    {
                        var de = from d in hm.T_Department select d;
                        var udept = de.FirstOrDefault(s => s.部门编号 == Convert.ToInt32( textEdit1.Text));
                        udept.部门名称 = textEdit2.Text;
                        if (comboBox1.Text == "")
                        {
                            udept.部门经理 = null;
                        }
                        else 
                        {
                            if (HmHelper.FindItem(comboBox1, comboBox1.Text))
                            {
                                udept.部门经理 = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                            }
                            else
                            {
                                toolStripStatusLabel1.Text = "操作提示:无此经理";
                            }
                        }
                        if (comboBox2.Text == "")
                        {
                            udept.辅助经理 = null;
                        }
                        else 
                        {
                            if (HmHelper.FindItem(comboBox2, comboBox2.Text))
                            {
                                udept.部门经理 = Convert.ToInt32(comboBox2.SelectedValue.ToString());
                            }
                            else
                            {
                                toolStripStatusLabel1.Text = "操作提示:无此经理";
                            }
                        }
                        hm.SubmitChanges();
                        if (comboBox1.Text != "")
                        {
                            var emp = from em in hm.T_Employees where em.姓名 == comboBox1.Text select em;
                            var newdept = from nd in hm.T_Department where nd.部门名称 == textEdit2.Text select nd;
                            if (emp != null)
                            {
                                emp.FirstOrDefault().部门编号 = newdept.FirstOrDefault().部门编号;
                                hm.SubmitChanges();
                            }
                        }
                    }
                }
            }
            var depts = from d in hm.V_Department select d;
            gridControl1.DataSource = depts;
            toolStripStatusLabel1.Text = "操作提示:成功";
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            LoadControl();
        }
    }
}