using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon
{
    //Bu form bizim işlemlerimizi içeren formları açmamızı sağlayan ana formdur.
    public partial class FrmAna : Form  
    {
        public FrmAna()
        {
            InitializeComponent();
        }
        public string Bilgi;
        //Kullanıcının T.C. kimlik numarasını bilgilerin yansıtmak için giriş ekranından aldım.
        public FrmAna(string bilgi)
        {
            InitializeComponent();
            Bilgi = bilgi;
            
        }
        //Kullanılan formların örneklerini yazdım.
        FrmOgretmenler frmOgretmen;
        FrmOgrenciler frmOgrencıler;
        FrmVeliler frmVeliler;
        FrmAyarlar frmAyarlar;
        FrmNotGirisi frmNotGirisi;
        FrmKarne frmKarne;
        FrmAnasayfa frmAnasayfa;
        //Hangi butona basılırsa o formun açılmasını sağladım ve yaptığım karar yapısı sayesinde bir form kapandığı zaman bir daha açılmasını sağladım
        //ve açık olan formun tekrar açılmasını engelledim

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmVeliler == null || frmVeliler.IsDisposed)
            {
                frmVeliler = new FrmVeliler();
                frmVeliler.MdiParent = this;
                frmVeliler.Show();

            }

        }

        private void btnOgretmen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmOgretmen==null||frmOgretmen.IsDisposed)
            {
                frmOgretmen = new FrmOgretmenler();
            frmOgretmen.MdiParent = this;
            frmOgretmen.Show();

            }
            


        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (frmOgrencıler == null || frmOgrencıler.IsDisposed)
            {
                frmOgrencıler= new FrmOgrenciler();
                frmOgrencıler.MdiParent = this;
                frmOgrencıler.Show();

            }

        }

        private void FrmAna_Load(object sender, EventArgs e)
        {
            if (frmAnasayfa == null || frmAnasayfa.IsDisposed)
            {
                frmAnasayfa = new FrmAnasayfa();
                frmAnasayfa.MdiParent = this;
                frmAnasayfa.Show();

            }

        }

        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmAyarlar == null || frmAyarlar.IsDisposed)
            {
                frmAyarlar  = new FrmAyarlar();
                frmAyarlar.MdiParent = this;
                frmAyarlar.Show();

            }
        }

        private void btnNot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmNotGirisi == null || frmNotGirisi.IsDisposed)
            {
                frmNotGirisi = new FrmNotGirisi(Bilgi);
                frmNotGirisi.MdiParent = this;
                frmNotGirisi.Show();

            }

        }

        private void btnKarne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmKarne== null || frmKarne.IsDisposed)
            {
                frmKarne = new FrmKarne();
                frmKarne.MdiParent = this;
                frmKarne.Show();

            }


        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmAnasayfa == null || frmAnasayfa.IsDisposed)
            {
                frmAnasayfa = new FrmAnasayfa();
                frmAnasayfa.MdiParent = this;
                frmAnasayfa.Show();

            }

        }
    }
}
