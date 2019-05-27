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
    public partial class FrmCommission : DevExpress.XtraEditors.XtraForm
    {
        public FrmCommission()
        {
            InitializeComponent();
        }

        private void FrmCommission_Load(object sender, EventArgs e)
        {
            HMMangeDataContext hm = new HMMangeDataContext();
            var tc = from t in hm.V_Commission select t;
            gridControl1.DataSource = tc;
            
        }

        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "业绩下限") 
            {
                e.Handled = true;
            }
            if (e.Column.FieldName == "业绩上限")
            {
                e.Handled = true;
            }
            if (e.Column.FieldName == "底薪")
            {
                e.Handled = true;
            }
            if (e.Column.FieldName == "奖金")
            {
                e.Handled = true;
            }
            if (e.Column.FieldName == "提成比率")
            {
                e.Handled = true;
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            int row = gridView1.GetSelectedRows()[0];
            if (gridView1.GetRowCellDisplayText(row, "是否高端")!="未选中")
            {
                textEdit1.Text=gridView1.GetRowCellDisplayText(row, "职务") + "高端提成百分比："
                    + (Convert.ToDecimal(gridView1.GetRowCellDisplayText(row, "提成比率")) * 100).ToString()
                    + "%";
            }
            else
            {
                string zw = gridView1.GetRowCellDisplayText(row, "职务");
                string low_limit = gridView1.GetRowCellDisplayText(row, "业绩下限");
                string up_limit = gridView1.GetRowCellDisplayText(row, "业绩上限");
                string dx = gridView1.GetRowCellDisplayText(row, "底薪");
                string jj = gridView1.GetRowCellDisplayText(row, "奖金");
                string bfb = gridView1.GetRowCellDisplayText(row, "提成比率");
                textEdit1.Text =  zw+ "基本工资组成方式：当业绩高于" + low_limit + "低于等于" 
                    + up_limit + "时，底薪为" + dx + "元,奖金为" + jj + "元，业绩提成半分比为" 
                    + (Convert.ToDecimal(bfb) * 100).ToString()+ "%。";
            }
        }
    }
}