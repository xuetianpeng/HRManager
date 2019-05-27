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
    public partial class FrmCashier : DevExpress.XtraEditors.XtraForm
    {
        public int ReSume_ID = 0;
        HMMangeDataContext hm = new HMMangeDataContext();
        public FrmCashier()
        {
            InitializeComponent();
        }

        public FrmCashier(int resumeid) 
        {
            ReSume_ID = resumeid;
            InitializeComponent();
        }

        private void FrmCashier_Load(object sender, EventArgs e)
        {
            LoadDept();
            LoadInCome();
            LoadPayType();
            if (ReSume_ID != 0) 
            {
                var res = from re in hm.T_ReSume where re.编号 == ReSume_ID select re;
                if (res.Count() > 0) 
                {
                    textResumeNo.Text = ReSume_ID.ToString();
                    textAName.Text = res.FirstOrDefault().姓名;
                    textIDCard.Text = res.FirstOrDefault().身份证号;
                    textPhone.Text = res.FirstOrDefault().手机;
                    comboDept.SelectedValue = res.FirstOrDefault().部门编号;
                    comboInCome.Focus();
                }
            }
        }

        void LoadDept() 
        {
            var depts = from dept in hm.T_Department select dept;
            comboDept.DataSource = depts;
            comboDept.DisplayMember = "部门名称";
            comboDept.ValueMember = "部门编号";
        }

        void LoadInCome() 
        {
            var incomes = from inc in hm.T_InCome_Type select inc;
            comboInCome.DataSource = incomes;
            comboInCome.DisplayMember = "收入类型";
            comboInCome.ValueMember = "收入类型编号";
        }

        void LoadPayType() 
        {
            var paytypes = from paytype in hm.T_Pay_Type select paytype;
            comboPayType.DataSource = paytypes;
            comboPayType.DisplayMember = "支付类型名称";
            comboPayType.ValueMember = "支付类型编号";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (listIncome.Items.Count == 0)
            {

            }
            else 
            {

            }
            if (listpaytype.Items.Count == 0)
            {

            }
            else 
            {

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textIncome.Text) < 0) 
            {
                textIncome.Text = "";
                return;
            }
            if (listIncome.Items.Count == 0)
            {
                if (textIncome.Text == "0") 
                {
                    return;
                }
                ListViewItem lvi = new ListViewItem();
                lvi.Text = comboPayType.SelectedValue.ToString();
                lvi.SubItems.Add(comboInCome.Text);
                lvi.SubItems.Add(textIncome.Text);
                listIncome.Items.Add(lvi);
            }
            else 
            {
                bool has = false;
                foreach (ListViewItem lvi in listIncome.Items) 
                {
                    if (lvi.SubItems[1].Text == comboInCome.Text) 
                    {
                        has = true;
                        if (textIncome.Text == "0")
                        {
                            listIncome.Items.Remove(lvi);
                        }
                        else
                        {
                            if (comboInCome.Text == "退费")
                            {
                                lvi.SubItems[2].Text = "-" + textIncome.Text;
                            }
                            else 
                            {
                                lvi.SubItems[2].Text = textIncome.Text;
                            }
                        }
                    }
                }
                if (!has) 
                {
                    if (textIncome.Text == "0")
                    {
                        return;
                    }
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = comboInCome.SelectedValue.ToString();
                    lvi.SubItems.Add(comboInCome.Text);
                    lvi.SubItems.Add(textIncome.Text);
                    listIncome.Items.Add(lvi);
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textpaytype.Text) < 0)
            {
                textpaytype.Text = "";
                return;
            }
            if (listpaytype.Items.Count == 0)
            {
                if (textpaytype.Text == "0")
                {
                    return;
                }
                ListViewItem lvi = new ListViewItem();
                lvi.Text = comboPayType.SelectedValue.ToString();
                lvi.SubItems.Add(comboPayType.Text);
                lvi.SubItems.Add(textpaytype.Text);
                listpaytype.Items.Add(lvi);
            }
            else
            {
                bool has = false;
                foreach (ListViewItem lvi in listpaytype.Items)
                {
                    if (lvi.SubItems[1].Text == comboPayType.Text)
                    {
                        has = true;
                        if (textpaytype.Text == "0")
                        {
                            listpaytype.Items.Remove(lvi);
                        }
                        else
                        {
                            if (comboPayType.Text == "退费")
                            {
                                lvi.SubItems[2].Text = "-" + textpaytype.Text;
                            }
                            else
                            {
                                lvi.SubItems[2].Text = textpaytype.Text;
                            }
                        }
                    }
                }
                if (!has)
                {
                    if (textpaytype.Text == "0")
                    {
                        return;
                    }
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = comboPayType.SelectedValue.ToString();
                    lvi.SubItems.Add(comboPayType.Text);
                    lvi.SubItems.Add(textpaytype.Text);
                    listpaytype.Items.Add(lvi);
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
