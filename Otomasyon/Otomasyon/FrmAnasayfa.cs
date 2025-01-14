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
    //Bu formda yönetici forma girdiği zaman göreceği ekranı ayarladım.
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }
        FrmGiris frmGiris;
        FrmAna frmAna;
        //Çıkış yap butonuna basıldığında giriş ekranına dönmesini sağladım.Ve gizlenen formun çarpıya basıldığı zaman programı kapatmasını sağladım.

        private void btnCıkısYap_Click(object sender, EventArgs e)
        {
            frmGiris = new FrmGiris();
            frmGiris.FormClosed += (s, args) => Application.Exit();

            Form mdiparent = this.MdiParent;

            this.Hide();
            mdiparent?.Hide();
            frmGiris.Show();
            
        }
        //Uygulamayı kapat butonuna basıldığı zaman uygulamayı komple kapanmasını sağladım.

        private void btnUygulamayıKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {

        }
    }
}
