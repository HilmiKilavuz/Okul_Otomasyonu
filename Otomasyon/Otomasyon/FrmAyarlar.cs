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
    public partial class FrmAyarlar : Form
    {
        //Bu formda öğretmen ve öğrencilere şifre ataması değiştirme işlemlerinin yapılmasını sağladım.
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        //formdaki öğrenci  bölümündeki textboxları temizleyen metodu tanımladım.
        void temizleOgrenci()
        {
            txtOgrId.Text = "";
            lookUpEdit2.Text = "";
            txtOgrSinif.Text = "";
            mskOgrTc.Text = "";
            txtOgrSifre.Text = "";
            pictureEdit2.Text = "";
        }
        //formdaki öğretmen bölümündeki textboxları temizleyen metodu tanımladım.

        void temizleOgretmen()
        {
            txtOgrtId.Text = "";
            lookUpEdit1.Properties.NullText = "Öğretmen Seçiniz";
            txtBrans.Text = "";
            mskOgrtTc.Text = "";
            txtOgrtSifre.Text = "";
            pictureEdit1.Text = "";

        }
        //Bağlantı Sınıfının bir nesnesini oluşturdum ve gerekli yerlerde daha rahat kullanmayı sağladım.
        sqlBaglantisi bgl = new sqlBaglantisi();
        //yazdığım prosedürler sayesinde öğretmen ve öğrenci şifre bilgilerini tabloya yansıttım. 
        void listele()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("execute AyarlarOgretmenler",bgl.baglanti());
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("execute AyarlarOgrenci",bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
            bgl.baglanti().Close();


        }
        //Öğretmen şifre atamasınıda kullanmak için öğretmen bilgilerini çeken metodu tanımladım.
        //ve lookup edit içine bu sanal tabloyu yansıttım.
        void ogretmenListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select OGRTID, (OGRTAD +' '+ OGRTSOYAD)as 'OGRTADSOYAD' , OGRTBRANS from TBL_OGRETMENLER ",bgl.baglanti());

            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "OGRTID";
            lookUpEdit1.Properties.DisplayMember = "OGRTADSOYAD";
            lookUpEdit1.Properties.NullText = "Öğretmen Seçiniz";
            lookUpEdit1.Properties.DataSource = dt;
            
        }
        //Öğrenci şifre atamasınıda kullanmak için öğrenci bilgilerini çeken metodu tanımladım.
        //ve lookup edit içine bu sanal tabloyu yansıttım.
        void ogrenciListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select OGRID, (OGRAD+' '+OGRSOYAD) as OGRADSOYAD , OGRSINIF from TBL_OGRENCILER",bgl.baglanti());
            da.Fill(dt);
            lookUpEdit2.Properties.ValueMember = "OGRID";
            lookUpEdit2.Properties.DisplayMember = "OGRADSOYAD";
            lookUpEdit2.Properties.DataSource = dt;
        }
        //form yüklendiği zaman öğretmen , öğrenci bilgilerini yansıtmak için yazdığım metodları çağırdım ve text boxları temizledim.
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            ogretmenListele();
            temizleOgretmen();
            ogrenciListele();
            temizleOgrenci();
        }
       

        //Gridview Üzerinden tıklanan kişinin bilgilerini bilgi boşluklarına ve fotoğrafı göstermesini sağladım
        public string yeniyol;
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                txtOgrtId.Text = dr["AYARLARID"].ToString();
                lookUpEdit1.Text = dr["OGRTADSOYAD"].ToString();
                txtBrans.Text = dr["OGRTBRANS"].ToString();
                mskOgrtTc.Text = dr["OGRTTC"].ToString();
                txtOgrtSifre.Text = dr["OGRTSIFRE"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRTFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);


            }
        }

    //Look up edit üzerrinde farklı bir öğretmen seçildiği zaman seçilen öğretmen bilgilerini boşluklara yazmasını sağladım.
        private void lookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            temizleOgretmen();
            SqlCommand komut = new SqlCommand("select * from TBL_OGRETMENLER where OGRTID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lookUpEdit1.EditValue);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtOgrtId.Text = dr["OGRTID"].ToString();
                
                txtBrans.Text = dr["OGRTBRANS"].ToString();
                mskOgrtTc.Text = dr["OGRTTC"].ToString();
              
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRTFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);
            }
        }
        //Kaydet butonuna basıldığı zaman öğretmen şifresini öğretmenlerin şifre bilgilerinin olduğu tabloya kaydettim.
        //eğer bir hata yaşanırsa try-catch ile hata mesajı verdirmeyi sağladım.
        private void btnOgrtKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into TBL_AYARLAR (AYARLARID,OGRTSIFRE) values(@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtOgrtId.Text);
                komut.Parameters.AddWithValue("@p2", txtOgrtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Şifre Başarılı Bir Şekilde Kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizleOgretmen();

            }
            catch
            {
                MessageBox.Show("Girlen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        //Güncelle butonuna basıldığı zaman öğretmen şifresini güncellemeyi sağladım. 
        private void btnOgrtGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_AYARLAR set OGRTSIFRE=@p1 where AYARLARID=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtOgrtSifre.Text);
            komut.Parameters.AddWithValue("@p2",txtOgrtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Şifre Başarılı Bir Şekilde Güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizleOgretmen();
        }
        //tanımladığım temizle metodunu temizle butonuna basıldığında da çalışması için fonksiyonu çağırdım.
        private void btnOgrtTemizle_Click(object sender, EventArgs e)
        {
            temizleOgretmen();
        }

        //Gridview Üzerinden tıklanan kişinin bilgilerini bilgi boşluklarına ve fotoğrafı göstermesini sağladım
        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            temizleOgrenci();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                txtOgrId.Text = dr["AYARLAROGRID"].ToString();
                lookUpEdit2.Text = dr["OGRADSOYAD"].ToString();
                txtOgrSinif.Text = dr["OGRSINIF"].ToString();
                mskOgrTc.Text = dr["OGRTC"].ToString();
                txtOgrSifre.Text = dr["OGRSIFRE"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit2.Image = Image.FromFile(yeniyol);


            }

        }

      //Öğrencikaydet tarafındaki butona basıldığı zaman öğrenci şifresini öğrenci şifresinin bulunduğu tabloya kaydettim.
        private void btnOgrKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into TBL_OGRAYARLAR (AYARLAROGRID,OGRSIFRE) values(@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtOgrId.Text);
                komut.Parameters.AddWithValue("@p2", txtOgrSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Şifre Başarılı Bir Şekilde Kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizleOgrenci();

            }
            catch
            {
                MessageBox.Show("Girlen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        //Güncelle butonuna basıldığı zaman öğrenci şifresini güncellemeyi sağladım. 

        private void btnOgrGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_OGRAYARLAR set OGRSIFRE=@p1 where AYARLAROGRID=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtOgrSifre.Text);
            komut.Parameters.AddWithValue("@p2", txtOgrId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Şifre Başarılı Bir Şekilde Güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizleOgrenci();
        }
        //tanımladığım temizle metodunu temizle butonuna basıldığında da çalışması için fonksiyonu çağırdım.

        private void btnOgrTemizle_Click(object sender, EventArgs e)
        {
            temizleOgrenci();
        }
        //look up edit üzerinden tıklana öğrenci bilgilerinin ve fotoğrafını getirmeyi sağladım.
        private void lookUpEdit2_Properties_EditValueChanged(object sender, EventArgs e)
        {
            temizleOgrenci();
            SqlCommand komut = new SqlCommand("select * from TBL_OGRENCILER where OGRID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit2.EditValue);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtOgrId.Text = dr["OGRID"].ToString();

                txtOgrSinif.Text = dr["OGRSINIF"].ToString();
                mskOgrTc.Text = dr["OGRTC"].ToString();

                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit2.Image = Image.FromFile(yeniyol);
            }

        }

      
    }
}
