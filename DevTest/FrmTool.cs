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
    public partial class FrmTool : DevExpress.XtraEditors.XtraForm
    {
        public FrmTool()
        {
            InitializeComponent();
        }

        HMMangeDataContext hm = new HMMangeDataContext();
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        private void FrmTool_Load(object sender, EventArgs e)
        {
            LoadPayType();
            LoadOutType();
            LoadInCome();
            LoadPost();
        }

        void LoadPayType() 
        {
            var paytypes = from paytype in hm.T_Pay_Type select paytype;
            gridControl1.DataSource = paytypes;
        }

        void LoadOutType() 
        {
            var outtypes = from outtype in hm.T_Spend_Type select outtype;
            gridControl3.DataSource = outtypes;
        }

        void LoadInCome() 
        {
            var incomes = from income in hm.T_InCome_Type select income;
            gridControl4.DataSource = incomes;
        }

        void LoadPost() 
        {
            var posts = from post in hm.T_Post select post;
            gridControl5.DataSource = posts;
        }
    }
}