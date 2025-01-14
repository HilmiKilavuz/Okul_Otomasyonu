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
using System.IO;

namespace Otomasyon
{
    //Bu formda yöneticinin öğretmen kaydı yapabilmesini sağladım.

    public partial class FrmOgretmenler : Form
    {
        public FrmOgretmenler()
        {
            InitializeComponent();
        }
        //Bağlantı Sınıfının bir nesnesini oluşturdum ve gerekli yerlerde daha rahat kullanmayı sağladım.

        sqlBaglantisi bgl = new sqlBaglantisi();
        //Bu metod sayesinde öğretmen tablosundaki öğretmen bilgilerini grid controle yasıttım. 
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_OGRETMENLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        //bu fonksiyonda veri tabanından şehirleri bir liste halinde aldım.

        void ilekle()
        {
            SqlCommand komut = new SqlCommand("select * from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[1]);
            }
            bgl.baglanti().Close();
        }
        //bu fonksiyonda veri tabanından branşları bir liste halinde aldım.

        void bransgetir()
        {
            SqlCommand komut = new SqlCommand("select * from TBL_BRANSLAR",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Properties.Items.Add(dr[1]);


            }
            bgl.baglanti().Close();


        }
        //Form yüklendiği zaman yazdığım metodların çalışması sağladım.
        private void FrmOgretmenler_Load(object sender, EventArgs e)
        {
            listele();
            ilekle();
            bransgetir();
            temizle();
        }
        //bu metod sayesinde seçilen il hangisi ise ilçeler bölümünde o şehirin ilçelerin gelmesini sağladım.

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select * from TBL_ILCELER where sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbil.SelectedIndex+1);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbilce.Properties.Items.Add(oku[1]);
            }
            bgl.baglanti().Close();
        }
        //Kaydet butonuna basıldığı zaman girilen öğretemen bilgilerini öğretmen tablosuna kaydettim.

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_OGRETMENLER (OGRTAD,OGRTSOYAD,OGRTTC,OGRTTEL,OGRTMAIL,OGRTIL,OGRTILCE,OGRTADRES,OGRTBRANS,OGRTFOTO) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@P10)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskTxtTc.Text);
            komut.Parameters.AddWithValue("@p4",mskTxtTel.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6",cmbil.Text);
            komut.Parameters.AddWithValue("@p7", cmbilce.Text);
            komut.Parameters.AddWithValue("@p8",richTxtAdres.Text);
            komut.Parameters.AddWithValue("@p9",cmbBrans.Text);
            komut.Parameters.AddWithValue("@p10", Path.GetFileName(yeniyol));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Eklendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        }
        //Gridview Üzerinden tıklanan öğretmen  bilgilerini  boşluklarına yerleştirdim.

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                txtId.Text = dr["OGRTID"].ToString();
                txtAd.Text = dr["OGRTAD"].ToString();
                txtSoyad.Text = dr["OGRTSOYAD"].ToString();
                mskTxtTc.Text = dr["OGRTTC"].ToString();
                mskTxtTel.Text = dr["OGRTTEL"].ToString();
                txtMail.Text = dr["OGRTMAIL"].ToString();
                cmbil.Text = dr["OGRTIL"].ToString();
                cmbilce.Text = dr["OGRTILCE"].ToString();
                richTxtAdres.Text = dr["OGRTADRES"].ToString();
                cmbBrans.Text = dr["OGRTBRANS"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRTFOTO"].ToString();
                pctrBoxOgretmen.ImageLocation = yeniyol;

            }
        }
        public string yeniyol;
        //Resim seçmek için butona baasıldığı zaman istenilen fotoğrafı seçmelerine izin verdim.
        //Seçilen fotoğrafın dosya yolunu daha sonrasında projenin olduğu klasörun içindeki resimler klasör içine kaydettim.
        private void btnResimSec_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dosya = new OpenFileDialog();
                dosya.Filter = "Resim Dosyası |*.jpg;*png;*nef|Tüm Dosyalar|*.*";
                dosya.ShowDialog();
                string dosyayolu = dosya.FileName;
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + Guid.NewGuid().ToString() + ".jpg";
                File.Copy(dosyayolu, yeniyol);
                pctrBoxOgretmen.ImageLocation = yeniyol;

            }
            catch
            {
                MessageBox.Show("Fotoğraf Seçilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            



        }
        //temizle metodu sayesinde formu temizlemeyi sağladım.

        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTxtTc.Text = "";
            mskTxtTel.Text = "";
            txtMail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            richTxtAdres.Text = "";
            cmbBrans.Text = "";
            pctrBoxOgretmen.ImageLocation ="";
        }
        //Güncelle butonu sayesinde seçilen öğretmen bilgilerini öğretmen id sine göre güncellemeyi sağladım. 

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_OGRETMENLER SET OGRTAD=@p1,OGRTSOYAD=@p2,OGRTTC=@p3,OGRTTEL=@p4,OGRTMAIL=@p5,OGRTIL=@p6,OGRTILCE=@p7, OGRTADRES=@p8,OGRTBRANS=@p9,OGRTFOTO=@p10 where OGRTID=@p11",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtTc.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtTel.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbil.Text);
            komut.Parameters.AddWithValue("@p7", cmbilce.Text);
            komut.Parameters.AddWithValue("@p8", richTxtAdres.Text);
            komut.Parameters.AddWithValue("@p9", cmbBrans.Text);
            komut.Parameters.AddWithValue("@p10", Path.GetFileName(yeniyol));
            komut.Parameters.AddWithValue("@p11",txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }
        //Silme butonu sayesinde öğretmen kaydını silmeyi sağladım.

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_OGRETMENLER where OGRTID=@p1" ,bgl.baglanti() );
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();




        }
        //temizle butonuna basıldığı zaman teizleme metodunun çalışmasını sağladım.

        private void btnMetinTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }
    }
}
