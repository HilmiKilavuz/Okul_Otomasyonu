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
    public partial class FrmNufusCuzdani : Form
    {
        //Bu formda kaydı yapılan öğrencinin üzerine çift tıklandığı zaman bilgilerini bir kimlik kartı üzerinde yazacak şekilde simüle ettim.
        public FrmNufusCuzdani()
        {
            InitializeComponent();
        }

        public string ad, soyad, tc, cinsiyet, dogtarihi, uzanti;

        private void FrmNufusCuzdani_Load(object sender, EventArgs e)
        {
            lblAd.Text = ad;
            lblSoyad.Text = soyad;
            lblTC.Text = tc;
            lblCiniyet.Text = cinsiyet;
            lblDogumTrh.Text = dogtarihi;
            pictureEdit1.Image = Image.FromFile(uzanti);


        }
    }
}
