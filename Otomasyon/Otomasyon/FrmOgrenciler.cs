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
    public partial class FrmOgrenciler : Form
    {
        //Bu formda yöneticinin öğrenci kaydı yapabilmesini sağladım.
        public FrmOgrenciler()
        {
            InitializeComponent();
        }
        //Bağlantı Sınıfının bir nesnesini oluşturdum ve gerekli yerlerde daha rahat kullanmayı sağladım.

        sqlBaglantisi bgl = new sqlBaglantisi();
        //Bu fonksiyonda ya yazdığım prosedürler sayesinde hangi sınıfta kim varsa o tabloda gözükmesini sağladım. 

        void listele()
        {
            //5.SINIF
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Execute OgrenciVeli5",bgl.baglanti());
            da1.Fill(dt1);
            GrdCtrl5.DataSource = dt1;
            //6.SINIF
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute OgrenciVeli6", bgl.baglanti());
            da2.Fill(dt2);
            GrdCtrl6.DataSource = dt2;
            //7.SINIF
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Execute OgrenciVeli7", bgl.baglanti());
            da3.Fill(dt3);
            GrdCtrl7.DataSource = dt3;
            //8.SINIF
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("Execute OgrenciVeli8", bgl.baglanti());
            da4.Fill(dt4);
            GrdCtrl8.DataSource = dt4;

        }
        //bu fonksiyonda veri tabanından şehirleri bir liste halinde aldım.
        void sehirekle()
        {
            SqlCommand komut = new SqlCommand("select * from TBL_ILLER ", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[1]);

            }
            bgl.baglanti().Close() ;
        }
       
        //Bu fonksiyon sayesinde veli tablosunda velileri getirdim ve öğrenci ile veliyi bağlamayı sağladım.
        void velilistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select VELIID,(VELIANNE+' | '+VELIBABA) AS 'VELIANNEBABA' FROM TBL_VELILER",bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember="VELIID";
            lookUpEdit1.Properties.DisplayMember = "VELIANNEBABA";
            lookUpEdit1.Properties.DataSource = dt;

        }
        //form yüklendiği zaman öğrencileri, velileri ve şehhirlerin gelmesi sağladım.
        private void FrmOgrencıler_Load(object sender, EventArgs e)
        {
            listele();
            sehirekle();
            temizle();
            velilistesi();
        }
        //temizle metodu sayesinde formu temizlemeyi sağladım.
        void temizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTxtNo.Text = "";
            cmbSinif.Text = "";
            dateEdit1.Text = "";
            rdBtnErkek.Checked = false;
            rdBtKız.Checked = false;
            mskTxtTc.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            richTxtAdres.Text = "";
            txtId.Text = "";
            pictureEdit1.Text = "";
        }
        //bu metod sayesinde seçilen il hangisi ise ilçeler bölümünde o şehirin ilçelerin gelmesini sağladım.
        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            cmbilce.Text = "";
            SqlCommand komut = new SqlCommand("select * from TBL_ILCELER WHERE sehir=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbil.SelectedIndex+1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[1]);
            }
            bgl.baglanti().Close();

        }
        //Gridview Üzerinden tıklanan öğrencinin  bilgilerini  boşluklarına yerleştirdim.
        
        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr!=null)
            {
                txtId.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTxtTc.Text = dr["OGRTC"].ToString();
                mskTxtNo.Text = dr["OGRNO"].ToString();
                cmbSinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString()=="E")
                {
                    rdBtnErkek.Checked= true;
                    rdBtKız.Checked = false;


                }
                else if(dr["OGRCINSIYET"].ToString() == "K")
                {
                    rdBtnErkek.Checked = false;
                    rdBtKız.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                richTxtAdres.Text = dr["OGRADRES"].ToString();
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);





            }
        }


        //Gridview Üzerinden tıklanan öğrencinin  bilgilerini  boşluklarına yerleştirdim.

        private void gridView1_FocusedRowObjectChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTxtTc.Text = dr["OGRTC"].ToString();
                mskTxtNo.Text = dr["OGRNO"].ToString();
                cmbSinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    rdBtnErkek.Checked = true;
                    rdBtKız.Checked = false;



                }
                else if (dr["OGRCINSIYET"].ToString() == "K")
                {
                    rdBtnErkek.Checked = false;
                    rdBtKız.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                richTxtAdres.Text = dr["OGRADRES"].ToString();
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);




            }

        }
        //Gridview Üzerinden tıklanan öğrencinin  bilgilerini  boşluklarına yerleştirdim.

        private void gridView3_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTxtTc.Text = dr["OGRTC"].ToString();
                mskTxtNo.Text = dr["OGRNO"].ToString();
                cmbSinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    rdBtnErkek.Checked = true;
                    rdBtKız.Checked = false;



                }
                else if (dr["OGRCINSIYET"].ToString() == "K")
                {
                    rdBtnErkek.Checked = false;
                    rdBtKız.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                richTxtAdres.Text = dr["OGRADRES"].ToString();
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);




            }

        }
        //Gridview Üzerinden tıklanan öğrencinin  bilgilerini  boşluklarına yerleştirdim.

        private void gridView4_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTxtTc.Text = dr["OGRTC"].ToString();
                mskTxtNo.Text = dr["OGRNO"].ToString();
                cmbSinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    rdBtnErkek.Checked = true;
                    rdBtKız.Checked = false;



                }
                else if (dr["OGRCINSIYET"].ToString() == "K")
                {
                    rdBtnErkek.Checked = false;
                    rdBtKız.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                richTxtAdres.Text = dr["OGRADRES"].ToString();
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);




            }

        }
        public string cinsiyet;
        //Kaydet butonuna basıldığı zaman girilen öğrenci bilgilerini öğrenci tablosuna kaydettim.

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into TBL_OGRENCILER(OGRAD,OGRSOYAD,OGRNO,OGRSINIF,OGRDOGTAR,OGRCINSIYET,OGRTC,OGRIL,OGRILCE,OGRADRES,OGRFOTO,OGRVELIID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", mskTxtNo.Text);
                komut.Parameters.AddWithValue("@p4", cmbSinif.Text);
                komut.Parameters.AddWithValue("@p5", dateEdit1.Text);
                if (rdBtnErkek.Checked == true)
                {
                    komut.Parameters.AddWithValue("@p6", cinsiyet = "E");
                }
                else
                {
                    komut.Parameters.AddWithValue("@p6", cinsiyet = "K");
                }
                komut.Parameters.AddWithValue("@p7", mskTxtTc.Text);
                komut.Parameters.AddWithValue("@p8", cmbil.Text);
                komut.Parameters.AddWithValue("@p9", cmbilce.Text);
                komut.Parameters.AddWithValue("@p10", richTxtAdres.Text);
                komut.Parameters.AddWithValue("@p11", Path.GetFileName(yeniyol));
                komut.Parameters.AddWithValue("p12", lookUpEdit1.EditValue);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Öğrenci Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

            }
            catch
            {
                MessageBox.Show("Girlen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }
        public string yeniyol;
        //Resim seçmek için butona baasıldığı zaman istenilen fotoğrafı seçmelerine izin verdim.
        //Seçilen fotoğrafın dosya yolunu daha sonrasında projenin olduğu klasörun içindeki resimler klasör içine kaydettim.
        private void btnResimSec_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dosya = new OpenFileDialog();
                dosya.Filter = "Resim Dosyası|*.jpg;*.png;*.nef|Tüm Dosyalar|*.*";
                dosya.ShowDialog();
                string dosyayolu = dosya.FileName;
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + Guid.NewGuid().ToString() + ".jpg";
                File.Copy(dosyayolu, yeniyol);
                pictureEdit1.Image = Image.FromFile(yeniyol);

            }
            catch
            {
                MessageBox.Show("Fotoğraf Seçilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }
        //Güncelle butonu sayesinde seçilen öğrenicinin bilgilerini öğrenci numarasına göre güncellemeyi sağladım. 
        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_OGRENCILER set OGRAD=@p1 , OGRSOYAD=@p2,OGRNO=@p3,OGRSINIF=@p4,OGRDOGTAR=@p5,OGRCINSIYET=@p6,OGRTC=@p7,OGRIL=@p8,OGRILCE=@p9,OGRADRES=@p10,OGRFOTO=@p11, OGRVELIID=@p13 where OGRID=@p12" , bgl.baglanti());
           
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtNo.Text);
            komut.Parameters.AddWithValue("@p4", cmbSinif.Text);
            komut.Parameters.AddWithValue("@p5", dateEdit1.Text);
            if (rdBtnErkek.Checked == true)
            {
                komut.Parameters.AddWithValue("@p6", cinsiyet = "E");
            }
            else
            {
                komut.Parameters.AddWithValue("@p6", cinsiyet = "K");
            }
            komut.Parameters.AddWithValue("@p7", mskTxtTc.Text);
            komut.Parameters.AddWithValue("@p8", cmbil.Text);
            komut.Parameters.AddWithValue("@p9", cmbilce.Text);
            komut.Parameters.AddWithValue("@p10", richTxtAdres.Text);
            komut.Parameters.AddWithValue("@p11", Path.GetFileName(yeniyol));
            komut.Parameters.AddWithValue("@p12",txtId.Text);
            komut.Parameters.AddWithValue("@p13",lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgiler Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        //Silme butonu sayesinde öğrencinin kaydını silmeyi sağladım.
        private void btnSil_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from  TBL_OGRENCILER where OGRID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }
        //temizle butonuna basıldığı zaman teizleme metodunun çalışmasını sağladım.
        private void btnMetinTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }
        //öğrencinin üzerine çift tıklandığı zaman nüfuscüzdan formunun açılması sağladım verilen paramere değerleri ile öğrenci bilgisini kimlik üzerinde yansıttım.
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            FrmNufusCuzdani frm = new FrmNufusCuzdani();
            if (dr!= null)
            {
                frm.ad = dr["OGRAD"].ToString();
                frm.soyad = dr["OGRSOYAD"].ToString();
                frm.tc = dr["OGRTC"].ToString();
                frm.cinsiyet = dr["OGRCINSIYET"].ToString();
                frm.dogtarihi = dr["OGRDOGTAR"].ToString();
                frm.uzanti= "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
            }
            frm.Show();
        }
        //öğrencinin üzerine çift tıklandığı zaman nüfuscüzdan formunun açılması sağladım verilen paramere değerleri ile öğrenci bilgisini kimlik üzerinde yansıttım.

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            FrmNufusCuzdani frm = new FrmNufusCuzdani();
            if (dr != null)
            {
                frm.ad = dr["OGRAD"].ToString();
                frm.soyad = dr["OGRSOYAD"].ToString();
                frm.tc = dr["OGRTC"].ToString();
                frm.cinsiyet = dr["OGRCINSIYET"].ToString();
                frm.dogtarihi = dr["OGRDOGTAR"].ToString();
                frm.uzanti = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
            }
            frm.Show();
        }
        //öğrencinin üzerine çift tıklandığı zaman nüfuscüzdan formunun açılması sağladım verilen paramere değerleri ile öğrenci bilgisini kimlik üzerinde yansıttım.

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            FrmNufusCuzdani frm = new FrmNufusCuzdani();
            if (dr != null)
            {
                frm.ad = dr["OGRAD"].ToString();
                frm.soyad = dr["OGRSOYAD"].ToString();
                frm.tc = dr["OGRTC"].ToString();
                frm.cinsiyet = dr["OGRCINSIYET"].ToString();
                frm.dogtarihi = dr["OGRDOGTAR"].ToString();
                frm.uzanti = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
            }
            frm.Show();
        }
        //öğrencinin üzerine çift tıklandığı zaman nüfuscüzdan formunun açılması sağladım verilen paramere değerleri ile öğrenci bilgisini kimlik üzerinde yansıttım.

        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            FrmNufusCuzdani frm = new FrmNufusCuzdani();
            if (dr != null)
            {
                frm.ad = dr["OGRAD"].ToString();
                frm.soyad = dr["OGRSOYAD"].ToString();
                frm.tc = dr["OGRTC"].ToString();
                frm.cinsiyet = dr["OGRCINSIYET"].ToString();
                frm.dogtarihi = dr["OGRDOGTAR"].ToString();
                frm.uzanti = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
            }
            frm.Show();
        }

        private void cmbSinif_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
