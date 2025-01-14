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
    //Bu formda yöneticinin istediği öğrencinin istediği ders için not verme yetkisi verdim
    //her sınıftaki çocuklar için ayrı tablo tasarlayıp karışıklıkları gidermeye çalıştım.
    public partial class FrmNotGirisi : Form
    {
        public FrmNotGirisi()
        {
            InitializeComponent();
        }
        string kullanıcıTc;
        //giriş yapan kişinin bilgilerini göstermek için giriş formundan kullanıcı adını aldım.
        public FrmNotGirisi(string bilgi)
        {
            InitializeComponent();
            kullanıcıTc = bilgi;
        }
        //Bağlantı Sınıfının bir nesnesini oluşturdum ve gerekli yerlerde daha rahat kullanmayı sağladım.
        sqlBaglantisi bgl = new sqlBaglantisi();
        //Bu fonksiyonda ya yazdığım prosedürler sayesinde bakılan sınıfın tablosuna göre sadece o sınıftaki öğrencilerin notlarını gösterdim.
        void listele()
        {
            //5.SINIF
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Execute notlarogrenci5", bgl.baglanti());
            da1.Fill(dt1);
            cmbSinif.DataSource = dt1;
            //6.SINIF
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute notlarogrenci6", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
            //7.SINIF
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Execute notlarogrenci7", bgl.baglanti());
            da3.Fill(dt3);
            gridControl3.DataSource = dt3;
            //8.SINIF
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("Execute notlarogrenci8", bgl.baglanti());
            da4.Fill(dt4);
            gridControl4.DataSource = dt4;

        }
        // Form yüklendiği zaman öğrenic not bilgilerinin gelmesinin ve bilgi giriş boşluklarının temizleyen metodu çağırdım.
        private void FrmNotGirisi_Load(object sender, EventArgs e)
        {
           
            listele();
            ogretmenBilgiGoster();
            bransListele();
            ogrenciBilgiGoster();
            temizle();
            
            
            
        }
        //look up edite seçilen sınıfa göre o sınıftaki öğrenci bilgilerinin gelmesini sağladım.
        void ogrenciBilgiGoster()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select OGRID, (OGRAD+' '+OGRSOYAD) as OGRADSOYAD , OGRSINIF , OGRTC from TBL_OGRENCILER WHERE OGRSINIF='"+cmbSinif2.Text+"'", bgl.baglanti());
            da.Fill(dt);
            lookupEditAdSoyad.Properties.ValueMember = "OGRID";
            lookupEditAdSoyad.Properties.DisplayMember = "OGRADSOYAD";
            lookupEditAdSoyad.Properties.DataSource = dt;

        }
        //giriş formundan aldığım kullanıcı adı bilgisi ile öğretmen bilgilerini öğretmen bilgi paneline yansıttım.
        string yeniyol;
       void ogretmenBilgiGoster()
        {
            SqlDataAdapter da = new SqlDataAdapter("select (OGRTAD+' '+OGRTSOYAD) as OGRTADSOYAD, OGRTTC,OGRTBRANS,OGRTFOTO from TBL_OGRETMENLER where OGRTTC='"+ kullanıcıTc+"'", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl5.DataSource = dt;
            DataRow dr = gridView5.GetDataRow(gridView5.FocusedRowHandle);
            lblOgrtadSoyad.Text = dr["OGRTADSOYAD"].ToString();
            lblOgrtBrans.Text = dr["OGRTBRANS"].ToString();
            lblOgrtTc.Text = dr["OGRTTC"].ToString();
            yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRTFOTO"].ToString();
            pictureEdit1.Image = Image.FromFile(yeniyol);

          
        } 
        //Gridview Üzerinden tıklanan öğrencinin not bilgilerini not bilgi boşluklarına yerleştirdim.
        //Ve aynı anda öğrenci bilgilerini öğrenci paneline yansıttım.
        private void gridView4_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (dr != null)
            {
                lblNot.Text = dr["NOTID"].ToString();
                txtId.Text = dr["OGRID"].ToString();
                lookupEditAdSoyad.Text = dr["OGRADSOYAD"].ToString();
                cmbSinif2.Text = dr["OGRSINIF"].ToString();
                lblOgrTc.Text = dr["OGRTC"].ToString();
                cmbBrans.Text = dr["DERSAD"].ToString();
                txtSinav1.Text = dr["SINAV1"].ToString();
                txtSinav2.Text = dr["SINAV2"].ToString();
                txtSozlu.Text = dr["SINAV3"].ToString();
                txtOrtalama.Text = dr["ORTALAMA"].ToString();

            }

        }
        //Branşları veri tabanındaki ders tablosundan branş look up editine doldurdum.
        void bransListele()
        {
            SqlCommand komut = new SqlCommand("select * from TBL_DERSLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Properties.Items.Add(dr[1]);


            }
            bgl.baglanti().Close();
        }
        //formdaki textboxları ve lookup editleri temizleyen metodu tanımladım.
        void temizle()
        {
            txtId.Text = "";
            lookupEditAdSoyad.Properties.NullText = "";
            lookupEditAdSoyad.Text = "";
            cmbSinif.Text="";
            lblOgrTc.Text = "";
            cmbBrans.Text = "";
            txtSinav1.Text ="";
            txtSinav2.Text = "";
            txtSozlu.Text = "";
            txtOrtalama.Text = "";

        }
        //Gridview Üzerinden tıklanan öğrencinin not bilgilerini not bilgi boşluklarına yerleştirdim.
        //Ve aynı anda öğrenci bilgilerini öğrenci paneline yansıttım.
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                lblNot.Text = dr["NOTID"].ToString();
                txtId.Text = dr["OGRID"].ToString();
                lookupEditAdSoyad.Text= dr["OGRADSOYAD"].ToString();
                cmbSinif2.Text = dr["OGRSINIF"].ToString();
                lblOgrTc.Text = dr["OGRTC"].ToString();
                cmbBrans.Text = dr["DERSAD"].ToString();
                txtSinav1.Text = dr["SINAV1"].ToString();
                txtSinav2.Text = dr["SINAV2"].ToString();
                txtSozlu.Text = dr["SINAV3"].ToString();
                txtOrtalama.Text = dr["ORTALAMA"].ToString();

            }
        }
        //temizle butonuna basıldığı zaman temizle metodun çağırdım.
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            temizle();
            btnKaydet.Enabled = false;
           
        }
        //sınıf bilgisi değiştiği zaman öğrenci bilgilerini değiştirerek sadece o sınıftaki öğrenci bilgilerinin gelmesini sağladım.
        private void cmbSinif2_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select OGRID ,(OGRAD + ' ' + OGRSOYAD) as OGRADSOYAD,OGRTC,OGRSINIF from TBL_OGRENCILER WHERE OGRSINIF = '" + cmbSinif2.Text + "'", bgl.baglanti());
            da.Fill(dt);
            lookupEditAdSoyad.Properties.ValueMember = "OGRID";
            lookupEditAdSoyad.Properties.DisplayMember = "OGRADSOYAD";
            lookupEditAdSoyad.Properties.DataSource = dt;
        }
        //bu buton sayesinden girilen notların ortalamasının alınamsın sağladım. eğer eksik ve aralık dışı girilen bir değer olursa hata vermeyi sağladım.
        //Ve hesaplananortalamayı ortalama textbox ına yazdırdım.
        private void btnHesapla_Click(object sender, EventArgs e)
        {
            int not1=0;
            int not2=0;
            int sozlu=0;
            try
            {
                if (Convert.ToInt32(txtSinav1.Text)<=100 && Convert.ToInt32(txtSinav1.Text)>0)
                {
                     not1 = Convert.ToInt32(txtSinav1.Text);
                }
                else
                {
                    MessageBox.Show("Girlen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                if (Convert.ToInt32(txtSinav2.Text) <= 100 && Convert.ToInt32(txtSinav2.Text) > 0)
                {
                     not2 = Convert.ToInt32(txtSinav2.Text);
                }
                else
                {
                    MessageBox.Show("Girlen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                if (Convert.ToInt32(txtSozlu.Text) <= 100 && Convert.ToInt32(txtSozlu.Text) > 0)
                {
                     sozlu = Convert.ToInt32(txtSozlu.Text);
                }
                else
                {
                    MessageBox.Show("Girlen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                int ortalama = (not1+not2+sozlu)/3;
               
                txtOrtalama.Text = ortalama.ToString();
                btnKaydet.Enabled = true;

            }
            catch {

                MessageBox.Show("Girlen değerler uygun değil.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

           
        }
        //kaydet butonu sayesinde girilen değerleri notların olduğu tabloya kaydettim.
        //Bir öğrencinin bir dersten bir giriş yapılması sağladım eğer birden fazla değer geğer girilirse hata mesajı bastırdım.

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            
                string kontrolQuery = "SELECT COUNT(1) FROM TBL_NOTLAR WHERE NOTOGRID = @p1 AND NOTDERSID = @p2";
                using (SqlCommand kontrolCommand = new SqlCommand(kontrolQuery, bgl.baglanti()))
                {
                    kontrolCommand.Parameters.AddWithValue("@P1", txtId.Text);
                    switch (cmbBrans.Text)
                    {
                        case "TÜRKÇE":
                            kontrolCommand.Parameters.AddWithValue("@p2", "1"); break;
                        case "MATEMATİK":
                            kontrolCommand.Parameters.AddWithValue("@p2", "2"); break;
                        case "FEN BİLGİSİ":
                            kontrolCommand.Parameters.AddWithValue("@p2", "3"); break;
                        case "SOSYAL BİLGİSİ":
                            kontrolCommand.Parameters.AddWithValue("@p2", "4"); break;
                        case "BEDEN EĞİTİMİ":
                            kontrolCommand.Parameters.AddWithValue("@p2", "5"); break;
                        case "DİN KÜLTRÜ VE AHLAK BİLGİSİ":
                            kontrolCommand.Parameters.AddWithValue("@p2", "6"); break;
                        case "RESİM":
                            kontrolCommand.Parameters.AddWithValue("@p2", "7"); break;
                        case "MÜZİK":
                            kontrolCommand.Parameters.AddWithValue("@p2", "8"); break;
                        case "TEKNOLOJİ TASARIM":
                            kontrolCommand.Parameters.AddWithValue("@p2", "9"); break;
                        case "İNGİLİZCE":
                            kontrolCommand.Parameters.AddWithValue("@p2", "10"); break;
                        case "ALMANCA":
                            kontrolCommand.Parameters.AddWithValue("@p2", "11"); break;
                        case "BİLGİSAYAR":
                            kontrolCommand.Parameters.AddWithValue("@p2", "12"); break;

                    }
                    try
                    {
                        int count = (int)kontrolCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            try
                            {
                                SqlCommand komut = new SqlCommand("insert into TBL_NOTLAR (NOTOGRID,NOTDERSID,SINAV1,SINAV2,SINAV3,ORTALAMA) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
                                komut.Parameters.AddWithValue("@p1", txtId.Text);

                                switch (cmbBrans.Text)
                                {
                                    case "TÜRKÇE":
                                        komut.Parameters.AddWithValue("@p2", "1"); break;
                                    case "MATEMATİK":
                                        komut.Parameters.AddWithValue("@p2", "2"); break;
                                    case "FEN BİLGİSİ":
                                        komut.Parameters.AddWithValue("@p2", "3"); break;
                                    case "SOSYAL BİLGİSİ":
                                        komut.Parameters.AddWithValue("@p2", "4"); break;
                                    case "BEDEN EĞİTİMİ":
                                        komut.Parameters.AddWithValue("@p2", "5"); break;
                                    case "DİN KÜLTRÜ VE AHLAK BİLGİSİ":
                                        komut.Parameters.AddWithValue("@p2", "6"); break;
                                    case "RESİM":
                                        komut.Parameters.AddWithValue("@p2", "7"); break;
                                    case "MÜZİK":
                                        komut.Parameters.AddWithValue("@p2", "8"); break;
                                    case "TEKNOLOJİ TASARIM":
                                        komut.Parameters.AddWithValue("@p2", "9"); break;
                                    case "İNGİLİZCE":
                                        komut.Parameters.AddWithValue("@p2", "10"); break;
                                    case "ALMANCA":
                                        komut.Parameters.AddWithValue("@p2", "11"); break;
                                    case "BİLGİSAYAR":
                                        komut.Parameters.AddWithValue("@p2", "12"); break;

                                }
                                komut.Parameters.AddWithValue("@p3", txtSinav1.Text);
                                komut.Parameters.AddWithValue("@p4", txtSinav2.Text);
                                komut.Parameters.AddWithValue("@p5", txtSozlu.Text);
                                komut.Parameters.AddWithValue("@p6", txtOrtalama.Text);
                                komut.ExecuteNonQuery();
                                bgl.baglanti().Close();
                                MessageBox.Show("Notlar Başarılı Bir Şekilde Kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnKaydet.Enabled = false;
                                listele();


                            }
                            catch
                            {
                                MessageBox.Show("Girilen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Öğrencinin bu dersten notu zaten girilmiş .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                    catch
                    {
                        MessageBox.Show("Ders seçilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }





        }



        //Gridview Üzerinden tıklanan öğrencinin not bilgilerini not bilgi boşluklarına yerleştirdim.
        //Ve aynı anda öğrenci bilgilerini öğrenci paneline yansıttım.
        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                lblNot.Text = dr["NOTID"].ToString();
                txtId.Text = dr["OGRID"].ToString();
                lookupEditAdSoyad.Text = dr["OGRADSOYAD"].ToString();
                cmbSinif2.Text = dr["OGRSINIF"].ToString();
                lblOgrTc.Text = dr["OGRTC"].ToString();
                cmbBrans.Text = dr["DERSAD"].ToString();
                txtSinav1.Text = dr["SINAV1"].ToString();
                txtSinav2.Text = dr["SINAV2"].ToString();
                txtSozlu.Text = dr["SINAV3"].ToString();
                txtOrtalama.Text = dr["ORTALAMA"].ToString();

            }

        }
        //Gridview Üzerinden tıklanan öğrencinin not bilgilerini not bilgi boşluklarına yerleştirdim.
        //Ve aynı anda öğrenci bilgilerini öğrenci paneline yansıttım.
        private void gridView3_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            if (dr != null)
            {
                lblNot.Text = dr["NOTID"].ToString();
                txtId.Text = dr["OGRID"].ToString();
                lookupEditAdSoyad.Text = dr["OGRADSOYAD"].ToString();
                cmbSinif2.Text = dr["OGRSINIF"].ToString();
                lblOgrTc.Text = dr["OGRTC"].ToString();
                cmbBrans.Text = dr["DERSAD"].ToString();
                txtSinav1.Text = dr["SINAV1"].ToString();
                txtSinav2.Text = dr["SINAV2"].ToString();
                txtSozlu.Text = dr["SINAV3"].ToString();
                txtOrtalama.Text = dr["ORTALAMA"].ToString();

            }

        }
        //Öğrenci ad soy bilgilerinini bulunduğu lookp editte farklı bir öğrenci seçildiği zaman seçilen öğrencinin öğrenci bilgiler bölümünde gözükmesini sağladım.
        private void lookupEditAdSoyad_Properties_EditValueChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from TBL_OGRENCILER where OGRID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookupEditAdSoyad.EditValue);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtId.Text = dr["OGRID"].ToString();
                lblOgrTc.Text = dr["OGRTC"].ToString();

            }

        }
        //Güncelle butonu sayesinde öğrencinin girilmiş olan not bilgilerinde değişiklik yapabilmeye sağladım.Eğer bir hata oluşursa hata mesajı verdim.
        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            try
            {
                SqlCommand komut = new SqlCommand("update TBL_NOTLAR set SINAV1=@p1,SINAV2=@p2,SINAV3=@p3,ORTALAMA=@p4,NOTOGRID=@P5 where NOTID=@p6", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtSinav1.Text);
                komut.Parameters.AddWithValue("@p2", txtSinav2.Text);
                komut.Parameters.AddWithValue("@p3", txtSozlu.Text);
                komut.Parameters.AddWithValue("@p4", txtOrtalama.Text);
                komut.Parameters.AddWithValue("@p5", txtId.Text);
                komut.Parameters.AddWithValue("@p6", lblNot.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Not Başarılı Bir Şekilde Güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();

            }
            catch
            {
                MessageBox.Show("Girilen değerler uygun değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void txtSozlu_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
