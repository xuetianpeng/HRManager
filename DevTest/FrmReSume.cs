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
    public partial class FrmReSume : DevExpress.XtraEditors.XtraForm
    {
        public FrmReSume()
        {
            InitializeComponent();
            
        }

        private void FrmReSume_Load(object sender, EventArgs e)
        {
            //if (HmHelper.Emp.职位编号 > 3)
            //{
            //    simpleButton4.Enabled = false;
            //}
            LoadGrid();
            SetState();
            LoadCom();
        }

        void LoadCom() 
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            var sign = from si in hm.T_Sign select si;
            var state = from st in hm.T_State select st;
            comboxsignstate.DataSource = sign;
            combostate.DataSource = state;
            comboxsignstate.DisplayMember = "签约情况编号";
            comboxsignstate.ValueMember = "签约情况";
            combostate.DisplayMember = "状态编号";
            combostate.ValueMember = "状态名称";
        }

        void LoadGrid() 
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            if (HmHelper.Emp.职位编号 == 1 || HmHelper.Emp.备注.Contains("会计"))
            {
                var list = from re in hm.V_ReSume
                           orderby re.应聘日期 descending
                           select new
                           {
                               编号 = re.简历编号,
                               部门 = re.部门,
                               姓名 = re.姓名,
                               性别 = re.男 == true ? "男" : "女",
                               年龄 = re.年龄 == null ? "" : Convert.ToString(DateTime.Now.Year - re.年龄.Value.Year),
                               手机 = re.手机,
                               身份证 = re.身份证,
                               应聘日期 = re.应聘日期 == null ? "" : re.应聘日期.ToString(),
                               预约日期 = re.预约日期 == null ? "" : re.预约日期.ToString(),
                               处理日期 = re.处理日期 == null ? "" : re.处理日期.ToString()
                           };
                gridControl1.DataSource = list;
            }
            else
            {
                var list = from re in hm.V_ReSume
                           where re.部门 == (from red in hm.T_Department
                                           where red.部门编号 == HmHelper.Emp.部门编号
                                           select red).SingleOrDefault().部门名称
                           orderby re.应聘日期 descending
                           select new
                           {
                               编号 = re.简历编号,
                               部门 = re.部门,
                               姓名 = re.姓名,
                               性别 = re.男 == true ? "男" : "女",
                               年龄 = re.年龄 == null ? "" : Convert.ToString(DateTime.Now.Year - re.年龄.Value.Year),
                               手机 = re.手机,
                               身份证 = re.身份证,
                               应聘日期 = re.应聘日期 == null ? "" : re.应聘日期.ToString(),
                               预约日期 = re.预约日期 == null ? "" : re.预约日期.ToString(),
                               处理日期 = re.处理日期 == null ? "" : re.处理日期.ToString()
                           };
                gridControl1.DataSource = list;

            }
        }

        void SetState() 
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            GETUNTREATED_RESUMEResult gs =
                hm.GETUNTREATED_RESUME(HmHelper.GetStartTime(datepistate.Value), HmHelper.GetEndTime(datepistate.Value)).FirstOrDefault();
            linkLabel1.Text = "未处理总数:" + gs.未处理.ToString();
            linkLabel2.Text = "拨打总数:" + gs.已拨打.ToString();
            linkLabel3.Text = "无意者总数:" + gs.无意上门.ToString();
            linkLabel4.Text = "预约者总数:" + gs.预约上门.ToString();
            linkLabel5.Text = "未上门总数:" + gs.未上门.ToString();
            linkLabel6.Text = "未成交总数:" + gs.上门未签约.ToString();
            linkLabel7.Text = "成交者总数:" + gs.已签约.ToString();
        }

        void SetControl() 
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            string bh = advBandedGridView1.GetRowCellDisplayText(advBandedGridView1.GetSelectedRows()[0], "编号");
            var resume = from r in hm.T_ReSume
                         where r.编号 == Convert.ToInt32(bh)
                         select r;
            textID.Text = resume.FirstOrDefault<T_ReSume>().编号.ToString();
            textRe_Name.Text = resume.FirstOrDefault<T_ReSume>().姓名;
            textSex.Text = resume.FirstOrDefault<T_ReSume>().男 == null ? "" :
                ((bool)resume.FirstOrDefault<T_ReSume>().男) ?
                textSex.Properties.Items[0].ToString() :
                textSex.Properties.Items[1].ToString();
            comboxRType.Text = resume.FirstOrDefault<T_ReSume>().简历类型 == null ? "" :
                ((int)resume.FirstOrDefault<T_ReSume>().简历类型) == 0 ?
                comboxRType.Items[0].ToString() :
                comboxRType.Items[1].ToString();
            textPost.Text = resume.FirstOrDefault<T_ReSume>().应聘职位 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().应聘职位;
            textHopewages.Text = resume.FirstOrDefault<T_ReSume>().期待工资 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().期待工资;
            textdom.Text = resume.FirstOrDefault<T_ReSume>().居住地 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().居住地;
            texthob.Text = resume.FirstOrDefault<T_ReSume>().工作经验 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().工作经验;
            textnowhob.Text = resume.FirstOrDefault<T_ReSume>().现在职位 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().现在职位;
            textnowcomp.Text = resume.FirstOrDefault<T_ReSume>().现在公司 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().现在公司;
            textedu.Text = resume.FirstOrDefault<T_ReSume>().学历 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().学历;
            textmj.Text = resume.FirstOrDefault<T_ReSume>().专业 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().专业;
            textschool.Text = resume.FirstOrDefault<T_ReSume>().学校 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().学校;
            textage.Text = resume.FirstOrDefault<T_ReSume>().年龄 == null ? "" :
                (DateTime.Now.Year - ((DateTime)resume.FirstOrDefault<T_ReSume>().年龄).Year).ToString();
            textphone.Text = resume.FirstOrDefault<T_ReSume>().手机 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().手机;
            textemail.Text = resume.FirstOrDefault<T_ReSume>().电子邮箱 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().电子邮箱;
            textAdate.Text = resume.FirstOrDefault<T_ReSume>().应聘日期 == null ? "" :
                Convert.ToDateTime(resume.FirstOrDefault<T_ReSume>().应聘日期).ToShortDateString();
            textsigndate.Text = resume.FirstOrDefault<T_ReSume>().缴费日期 == null ? "" :
                Convert.ToDateTime(resume.FirstOrDefault<T_ReSume>().缴费日期).ToShortDateString();
            textbillno.Text = resume.FirstOrDefault<T_ReSume>().订单号 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().订单号;
            textsermon.Text = resume.FirstOrDefault<T_ReSume>().服务费 == null ? "" :
                Convert.ToDecimal(resume.FirstOrDefault<T_ReSume>().服务费).ToString();
            textdeposit.Text = resume.FirstOrDefault<T_ReSume>().押金 == null ? "" :
                Convert.ToDecimal(resume.FirstOrDefault<T_ReSume>().押金).ToString();
            textspemon.Text = resume.FirstOrDefault<T_ReSume>().服务费 == null ? "" :
                Convert.ToDecimal(resume.FirstOrDefault<T_ReSume>().服务费).ToString();
            if (resume.FirstOrDefault<T_ReSume>().状态编号 == null)
            {
                combostate.Text = "";
            }
            else
            {
                switch (resume.FirstOrDefault<T_ReSume>().状态编号)
                {
                    case 0:
                        combostate.Text = combostate.Items[0].ToString();
                        break;
                    case 1:
                        combostate.Text = combostate.Items[1].ToString();
                        break;
                    case 2:
                        combostate.Text = combostate.Items[2].ToString();
                        break;
                    case 3:
                        combostate.Text = combostate.Items[3].ToString();
                        break;
                    case 4:
                        combostate.Text = combostate.Items[4].ToString();
                        break;
                    case 5:
                        combostate.Text = combostate.Items[5].ToString();
                        break;
                    case 6:
                        combostate.Text = combostate.Items[6].ToString();
                        break;
                }
            }
            textidcard.Text = resume.FirstOrDefault<T_ReSume>().身份证号 == null ? "" :
                resume.FirstOrDefault<T_ReSume>().身份证号;
            if (resume.FirstOrDefault<T_ReSume>().签约情况编号 == null)
            {
                comboxsignstate.Text = "未签约";
            }
            else
            {
                switch (Convert.ToInt32(resume.FirstOrDefault<T_ReSume>().签约情况编号))
                {
                    case 0:
                        comboxsignstate.Text = comboxsignstate.Items[0].ToString();
                        break;
                    case 1:
                        comboxsignstate.Text = comboxsignstate.Items[1].ToString();
                        break;
                    case 2:
                        comboxsignstate.Text = comboxsignstate.Items[2].ToString();
                        break;
                    case 3:
                        comboxsignstate.Text = comboxsignstate.Items[3].ToString();
                        break;
                    case 4:
                        comboxsignstate.Text = comboxsignstate.Items[4].ToString();
                        break;
                }
            }
        }

        void ClearControl() 
        {
            textidcard.Text = "";
            textRe_Name.Text = "";
            textSex.Text = "";
            textPost.Text = "";
            textHopewages.Text = "";
            textdom.Text = "";
            textnowcomp.Text = "";
            textnowhob.Text = "";
            textedu.Text = "";
            textmj.Text = "";
            textschool.Text = "";
            textage.Text = "";
            textphone.Text = "";
            textemail.Text = "";
            textAdate.Text = "";
            textbillno.Text = "";
            textsigndate.Text = "";
            textsermon.Text = "";
            textspemon.Text = "";
            textdeposit.Text = "";
            textidcard.Text = "";

        }

        private void advBandedGridView1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(advBandedGridView1.GetRowCellDisplayText(advBandedGridView1.GetSelectedRows()[0], "姓名").ToString());
            SetControl();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
            {
                if (this.MdiParent.MdiChildren[i].Text == "收银台界面")
                {
                    this.MdiParent.MdiChildren[i].Close();
                }
            }
            FrmCashier fm = new FrmCashier(Convert.ToInt32(textID.Text));
            fm.MdiParent = this.MdiParent;
            fm.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            if (textRe_Name.Text == "" && textphone.Text == "")
            {
                return;
            }
            else 
            {
                var hasresume =
                    from re in hm.T_ReSume
                    where re.姓名 == textRe_Name.Text && re.手机 == textphone.Text && re.部门编号 == HmHelper.Emp.部门编号
                    select re;
                if (hasresume.Count() > 0)
                {
                    T_ReSume resume = hasresume.FirstOrDefault();
                    if (textID.Text == resume.编号.ToString())
                    {
                        resume.处理日期 = DateTime.Now;
                        resume.电子邮箱 = textemail.Text;
                        resume.工作经验 = texthob.Text;
                        resume.简历类型 = comboxRType.Text == "全职" ? 0 : 1;
                        resume.居住地 = textdom.Text;
                        resume.男 = textSex.Text == "男" ? true : false;
                        if (textSex.Text == "")
                        {
                            resume.年龄 = null;
                        }
                        else 
                        {
                            resume.年龄= new DateTime(DateTime.Now.Year - Convert.ToInt32(textage.Text), 1, 1);
                        }
                        resume.期待工资 = textHopewages.Text;
                        resume.身份证号 = textidcard.Text;
                        resume.手机 = textphone.Text;
                        resume.现在公司 = textnowcomp.Text;
                        resume.现在职位 = textnowhob.Text;
                        resume.学历 = textedu.Text;
                        resume.学校 = textschool.Text;
                        resume.应聘职位 = textPost.Text;
                        resume.专业 = textmj.Text;
                        hm.SubmitChanges();
                    }
                    else
                    {
                        MessageBox.Show("数据重复");
                    }

                }
                else 
                {
                    T_ReSume newresume = new T_ReSume();
                    newresume.姓名 = textRe_Name.Text;
                    newresume.手机 = textphone.Text;
                    newresume.部门编号 = HmHelper.Emp.部门编号;
                    newresume.处理日期 = DateTime.Now;
                    newresume.电子邮箱 = textemail.Text;
                    newresume.工作经验 = texthob.Text;
                    newresume.简历类型 = comboxRType.Text == "全职" ? 0 : 1;
                    newresume.居住地 = textdom.Text;
                    newresume.男 = textSex.Text == "男" ? true : false;
                    if (textSex.Text == "")
                    {
                        newresume.年龄 = null;
                    }
                    else
                    {
                        newresume.年龄 = new DateTime(DateTime.Now.Year - Convert.ToInt32(textage.Text), 1, 1);
                    }
                    newresume.期待工资 = textHopewages.Text;
                    newresume.身份证号 = textidcard.Text;
                    newresume.手机 = textphone.Text;
                    newresume.现在公司 = textnowcomp.Text;
                    newresume.现在职位 = textnowhob.Text;
                    newresume.学历 = textedu.Text;
                    newresume.学校 = textschool.Text;
                    newresume.应聘职位 = textPost.Text;
                    newresume.专业 = textmj.Text;
                    hm.T_ReSume.InsertOnSubmit(newresume);
                    hm.SubmitChanges();
                }
                LoadGrid();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void textEdit2_Validated(object sender, EventArgs e)
        {
            advBandedGridView1.Columns.Clear();
            HMMangeDataContext hm = new HMMangeDataContext();
            if (textRe_Name.Text == "" && textphone.Text == "" && textidcard.Text == "")
            {
                var list = from re in hm.V_ReSume
                           where re.部门 == (from red in hm.T_Department
                                           where red.部门编号 == HmHelper.Emp.部门编号
                                           select red).SingleOrDefault().部门名称
                           orderby re.应聘日期 descending
                           select new
                           {
                               编号 = re.简历编号,
                               部门 = re.部门,
                               姓名 = re.姓名,
                               性别 = re.男 == true ? "男" : "女",
                               年龄 = re.年龄 == null ? "" : Convert.ToString(DateTime.Now.Year - re.年龄.Value.Year),
                               手机 = re.手机,
                               身份证 = re.身份证,
                               应聘日期 = re.应聘日期 == null ? "" : re.应聘日期.ToString(),
                               预约日期 = re.预约日期 == null ? "" : re.预约日期.ToString(),
                               处理日期 = re.处理日期 == null ? "" : re.处理日期.ToString()
                           };
                gridControl1.DataSource = list;
            }
            else 
            {
                var allre = from r in hm.V_ReSume
                            where (textRe_Name.Text == "" ? true : r.姓名.Contains(textRe_Name.Text)) && 
                            (textphone.Text == "" ? true : (r.手机 == null ? "" : r.手机).Contains(textphone.Text)) &&
                            (textidcard.Text == "" ? true : (r.身份证 == null ? "" : r.身份证).Contains(textidcard.Text))&&
                            r.部门 == (from red in hm.T_Department
                                      where red.部门编号 == HmHelper.Emp.部门编号
                                      select red).SingleOrDefault().部门名称
                            orderby r.应聘日期 descending
                            select new
                            {
                                编号 = r.简历编号,
                                部门 = r.部门,
                                姓名 = r.姓名,
                                性别 = r.男 == true ? "男" : "女",
                                年龄 = r.年龄 == null ? "" : Convert.ToString(DateTime.Now.Year - r.年龄.Value.Year),
                                手机 = r.手机,
                                身份证 = r.身份证,
                                应聘日期 = r.应聘日期 == null ? "" : r.应聘日期.ToString(),
                                预约日期 = r.预约日期 == null ? "" : r.预约日期.ToString(),
                                处理日期 = r.处理日期 == null ? "" : r.处理日期.ToString()
                            };
                gridControl1.DataSource = allre;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            SetState();
        }
    }
}