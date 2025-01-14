using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otomasyon
{
    //Bu formda ise kullanıcı tarafında girilen kullanıcı adı (T.C.Kimlik No) ve şifre bilgileri
    //ve girmek istediği panel için kontrolleri sağlayıp girişine izin verdim
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        //Bağlantı Sınıfının bir nesnesini oluşturdum ve gerekli yerlerde daha rahat kullanmayı sağladım.
        sqlBaglantisi bgl = new sqlBaglantisi();

        
        private void FrmGiris_Load(object sender, EventArgs e)
        {

        }
        //Yönetici giriş butonuna basıldığı zaman kullanıcının yönetici olup olmadığını anlamak için veri tabanından yönetici kaydı olup olmadığını kontrol 
        //edip ona göre girmesine izin verdim .Eğer öyle bir kullanıcı yok ise hata mesajı verdim.Eğer var ise yönetici ana formuna yönlendirdim.
        
        private void btnYonetici_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("select OGRTTC ,OGRTSIFRE,OGRTBRANS from TBL_AYARLAR inner join  TBL_OGRETMENLER on TBL_AYARLAR.AYARLARID=TBL_OGRETMENLER.OGRTID where OGRTTC=@p1 and OGRTSIFRE=@p2 and OGRTBRANS=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",mskKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@p2",txtSifre.Text);
            komut.Parameters.AddWithValue("@p3","MÜDÜR");
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                
                FrmAna frmAna = new FrmAna(mskKullaniciAdi.Text);
                frmAna.FormClosed += (s, args) => Application.Exit();
                frmAna.Show();
                this.Hide();
                


            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre","Uyarı!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtSifre.Text = "";
                mskKullaniciAdi.Text = "";
            }
        }
        //öğretmen giriş butonuna basıldığı zaman kullanıcının öğretmen olup olmadığını anlamak için veri tabanından öğretmen kaydı olup olmadığını kontrol 
        //edip ona göre girmesine izin verdim .Eğer öyle bir kullanıcı yok ise hata mesajı verdim.Eğer var ise öğretmen ana formuna yönlendirdim.

        private void btnOgretmen_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("select OGRTTC ,OGRTSIFRE from TBL_AYARLAR inner join  TBL_OGRETMENLER on TBL_AYARLAR.AYARLARID=TBL_OGRETMENLER.OGRTID where OGRTTC=@p1 and OGRTSIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                
                FrmOgretmenlerMenu frmOgretmenlerMenu = new FrmOgretmenlerMenu(mskKullaniciAdi.Text);
                frmOgretmenlerMenu.FormClosed += (s, args) => Application.Exit();
                frmOgretmenlerMenu.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSifre.Text = "";
                mskKullaniciAdi.Text = "";
            }
        }


        //öğrenci giriş butonuna basıldığı zaman kullanıcının öğrenci olup olmadığını anlamak için veri tabanından öğrenci kaydı olup olmadığını kontrol 
        //edip ona göre girmesine izin verdim .Eğer öyle bir kullanıcı yok ise hata mesajı verdim.Eğer var ise öğrenci ana formuna yönlendirdim.
        private void btnOgrenci_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select OGRTC ,OGRSIFRE from TBL_OGRAYARLAR inner join  TBL_OGRENCILER on TBL_OGRAYARLAR.AYARLAROGRID=TBL_OGRENCILER.OGRID where OGRTC=@p1 and OGRSIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);

            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmOgrencilerMenu frmOgrencilerMenu = new FrmOgrencilerMenu(mskKullaniciAdi.Text);
                frmOgrencilerMenu.FormClosed += (s, args) => Application.Exit();
                frmOgrencilerMenu.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSifre.Text = "";
                mskKullaniciAdi.Text = "";
            }
        }
    }
}
