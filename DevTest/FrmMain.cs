using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace DevTest
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private bool ShowChildrenForm(string p_ChildrenFormText)
        {
            int i;
            //依次检测当前窗体的子窗体
            for (i = 0; i < this.MdiChildren.Length; i++)
            {
                //判断当前子窗体的Text属性值是否与传入的字符串值相同
                if (this.MdiChildren[i].Text == p_ChildrenFormText)
                {
                    //如果值相同则表示此子窗体为想要调用的子窗体，激活此子窗体并返回true值
                    this.MdiChildren[i].Activate();
                    return true;
                }
            }
            //如果没有相同的值则表示要调用的子窗体还没有被打开，返回false值
            return false;
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReSume fm = new FrmReSume();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem48_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCashier fm = new FrmCashier();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCashier fm = new FrmCashier();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        //简历管理界面
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReSume fm = new FrmReSume();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OneKeyIn fm = new OneKeyIn();
            fm.ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OneKeyOut fm = new OneKeyOut();
            fm.ShowDialog();
        }

        private void barButtonItem49_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmSenior fm = new FrmSenior();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem8_ItemDoubleClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmOut fm = new FrmOut();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmOut fm = new FrmOut();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmOut fm = new FrmOut();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmOut fm = new FrmOut();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmOut fm = new FrmOut();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barSubItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCalendar fm = new FrmCalendar();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCalendar fm = new FrmCalendar();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmIndetails fm = new FrmIndetails();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmIndetails fm = new FrmIndetails();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmIndetails fm = new FrmIndetails();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCommission fm = new FrmCommission();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTool fm = new FrmTool();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLinkMe fm = new FrmLinkMe();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AboutBox1 fm = new AboutBox1();
            fm.ShowDialog();
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmDepart fm = new FrmDepart();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmEmployee fm = new FrmEmployee();
            if (!ShowChildrenForm(fm.Text))
            {
                fm.MdiParent = this;
                fm.Show();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ribbonControl1.ShowCustomizationMenu += ribbonControl1_ShowCustomizationMenu;
            string xmlurl;
            if (HmHelper.Emp != null)
            {
                switch (HmHelper.Emp.职位编号)
                {
                    case 1:
                        xmlurl = "http://119.3.1.234/Xml/Wy.xml";
                        break;
                    case 2:
                        xmlurl = "http://119.3.1.234/Xml/Jl.xml";
                        break;
                    case 3:
                        xmlurl = "http://119.3.1.234/Xml/Jl.xml";
                        break;
                    default:
                        xmlurl = "http://119.3.1.234/Xml/Wy.xml";
                        break;
                }

                if (HmHelper.Emp.姓名 == "慎娜")
                {
                    xmlurl = "http://119.3.1.234/Xml/All.xml";
                }
                XmlDocument x = new XmlDocument();
                x.Load(xmlurl);
                x.Save("1.xml");
                ribbonControl1.RestoreLayoutFromXml("1.xml");
                File.Delete("1.xml");
            }
        }

        void ribbonControl1_ShowCustomizationMenu(object sender, DevExpress.XtraBars.Ribbon.RibbonCustomizationMenuEventArgs e)
        {
            e.ShowCustomizationMenu = false;
        }
    }
}
