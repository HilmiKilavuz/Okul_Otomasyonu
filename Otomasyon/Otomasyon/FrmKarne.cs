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
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace Otomasyon
{
    public partial class FrmKarne : Form
    {
        //Bu formda yöneticinin istediği öğrencinin girilen bütün notlarını görmesini sağladım.
        public FrmKarne()
        {
            InitializeComponent();
        }
        //Bağlantı Sınıfının bir nesnesini oluşturdum ve gerekli yerlerde daha rahat kullanmayı sağladım.

        sqlBaglantisi bgl = new sqlBaglantisi();


        string yeniyol;
        //ad syadın bulunduğu lookuptan seçilen kişinin notları, kişisel bilgi ve fotoğrafının gelmesini sağladım. 
        private void lookupEditAdSoyad_Properties_EditValueChanged(object sender, EventArgs e)
        {
            var secilensatir = lookupEditAdSoyad.GetSelectedDataRow() as DataRowView;

            if (secilensatir != null)
            {
             
                string ogrid = secilensatir.Row[0].ToString() ;
                txtId.Text = ogrid;
            }
            else
            {
                MessageBox.Show("Hiçbir satır seçilmedi.");
            }

            SqlCommand komut = new SqlCommand("select (OGRAD +' '+OGRSOYAD) as OGRADSOYAD , OGRTC, OGRSINIF ,OGRFOTO from TBL_OGRENCILER where OGRID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtId.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr["OGRADSOYAD"].ToString();
                LblTC.Text = dr["OGRTC"].ToString();
                LblSinif.Text = dr["OGRSINIF"].ToString();
                yeniyol = "C:\\Users\\kilav\\OneDrive\\Masaüstü\\Kodlama\\C#\\Otomasyon\\Otomasyon\\Otomasyon" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = System.Drawing.Image.FromFile(yeniyol);
            }
            ogrNotListele();

        }
        //bu metodda seçilen öğrencinin id sine göre aldığı notları gridcontrol de gözükmesini sağladım.
        void ogrNotListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select DERSAD,SINAV1,SINAV2,SINAV3,ORTALAMA from TBL_NOTLAR inner join TBL_DERSLER on TBL_NOTLAR.NOTDERSID=TBL_DERSLER.DERSID inner join TBL_OGRENCILER on TBL_NOTLAR.NOTOGRID= TBL_OGRENCILER.OGRID where OGRID='" + txtId.Text + "' ", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        
        //Comboboxtan seçilen sınıf değerine göre o sınıfta hangi öğrenciler varsa sadece o öğrencilerini kişisel bilgilerini lookup editte gözükmesini sağladım.
        private void cmbSinif2_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select OGRID ,(OGRAD + ' ' + OGRSOYAD) as OGRADSOYAD,OGRTC,OGRSINIF from TBL_OGRENCILER WHERE OGRSINIF = '" + cmbSinif2.Text + "'", bgl.baglanti());
            da.Fill(dt);
            lookupEditAdSoyad.Properties.ValueMember = "OGRID";
            lookupEditAdSoyad.Properties.DisplayMember = "OGRADSOYAD";
            lookupEditAdSoyad.Properties.DataSource = dt;
        }

        private void FrmKarne_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıya özel bir dosya adı oluştur
                string currentUser = lookupEditAdSoyad.Text; // Bu değişkeni giriş yapılan kullanıcı adıyla değiştirin
                string pdfFileName = $"FormTam_{currentUser}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

                // PDF oluşturulacak yol (Masaüstü)
                string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), pdfFileName);

                // Formun client area kısmını almak için
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;

                using (Bitmap bitmap = new Bitmap(formWidth, formHeight))
                {
                    // Formu bitmap'e çizme işlemi
                    this.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, formWidth, formHeight));

                    // PDF dosyası oluşturuluyor
                    using (FileStream stream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // iTextSharp için yeni bir PDF sayfası oluşturuluyor
                        iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(formWidth, formHeight);
                        var pdfDoc = new Document(pageSize); // iTextSharp.text.Rectangle burada kullanılıyor
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        // Bitmap'i PDF'ye ekleyelim
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitmap.Save(ms, ImageFormat.Png);
                            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                            pdfImage.ScaleToFit(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                            pdfImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                            pdfDoc.Add(pdfImage);
                        }

                        pdfDoc.Close();
                    }

                    MessageBox.Show($"PDF başarıyla kaydedildi: {pdfPath}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // PDF'yi varsayılan PDF görüntüleyicisi ile aç
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = pdfPath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PDF oluşturulurken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
