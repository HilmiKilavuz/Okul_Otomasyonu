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
    //Bu formda yöneticinin veli kaydı yapmasını,silmesini ve bigilerini güncellemesin sağladım.
    public partial class FrmVeliler : Form
    {
        public FrmVeliler()
        {
            InitializeComponent();
        }
        OkulOtomasyonuEntities1 db = new OkulOtomasyonuEntities1();


        //formdaki textboxları ve lookup editleri temizleyen metodu tanımladım.

        void temizle()
        {
            txtId.Text = "";
            txtAnneAd.Text = "";
            txtBabaAd.Text = "";
            txtMail.Text = "";
            mskTxtTel1.Text = "";
            mskTxtTel2.Text = "";

        }
        //temizle butonuna basıldığı zaman temizleme metodunun çalışmasını sağladım.

        private void btnMetinTemizle_Click(object sender, EventArgs e)
        {
            temizle();

        }
        //Burada entity freamwork kullanarak veli bilgilerini tabloya listelenmesini sağladım.
        void listele()
        {
            var query = from item in db.TBL_VELILER
                        select new { item.VELIID,item.VELIANNE,item.VELIBABA,item.VELITEL1,item.VELITEL2,item.VELIMAIL };
            gridControl1.DataSource = query.ToList();
            
        }

        //Form yüklendiği zaman yazdığım metodların çalışması sağladım.

        private void FrmVeliler_Load(object sender, EventArgs e)
        {
            temizle();
            listele();
           

        }
        //kaydet butonu sayesinde girilen değerleri velilerin olduğu tabloya kaydettim.
        //veli bilgilerini kaydederken entity freamwork kullandım.
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TBL_VELILER veli = new TBL_VELILER();
            veli.VELIANNE = txtAnneAd.Text;
            veli.VELIBABA = txtBabaAd.Text;
            veli.VELITEL1 = mskTxtTel1.Text;
            veli.VELITEL2 = mskTxtTel2.Text;
            veli.VELIMAIL = txtMail.Text;
            db.TBL_VELILER.Add(veli);
            db.SaveChanges();
            MessageBox.Show("Veli Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }
        //tablodan tıklanan veliyi velinin bilgiler bölümündeki boşluklara gelecek şekilde ayarladım.
        //Güncelleme işleminde kolaylık olması açısından.
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            txtId.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"VELIID").ToString();
            txtAnneAd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIANNE").ToString() ;
            txtBabaAd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIBABA").ToString();
            mskTxtTel1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELITEL1").ToString();
            mskTxtTel2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELITEL2").ToString();
            txtMail.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIMAIL").ToString();



        }
        //Entity freamwork yardımıyla seçilen velinin id bilgisi ile girilen veli bilgilerinin güncellenmesini sağladım.
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIID").ToString());

            using (OkulOtomasyonuEntities1 db = new OkulOtomasyonuEntities1())
            {
                var item = db.TBL_VELILER.Find(id); 
                if (item != null)
                {
                    item.VELIANNE = txtAnneAd.Text;
                    item.VELIBABA = txtBabaAd.Text;
                    item.VELIMAIL = txtMail.Text;
                    item.VELITEL1 = mskTxtTel1.Text;
                    item.VELITEL2 = mskTxtTel2.Text;

                    db.SaveChanges();
                    MessageBox.Show("Veli Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
                else
                {
                    MessageBox.Show("Veli bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
        //Silme butonu sayesinde tabloda kayıtlı olan veli bilgileri silmeyi sağladım.
        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIID").ToString());
            using (OkulOtomasyonuEntities1 db = new OkulOtomasyonuEntities1())
            {
                var item = db.TBL_VELILER.Find(id);
                db.TBL_VELILER.Remove(item);
                db.SaveChanges();
                MessageBox.Show("Veli Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();



            }
               
          

        }
    }
}
