using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Otomasyon
{
    //Bu sınıfı her sefernde sql bağlantısı açmakla uğraşmamak için açtım ve bağlantı bilgileri girip bağlantıyı çalıştırdım.
    class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source = KILLME\SQLEXPRESS; Initial Catalog = OkulOtomasyonu; Integrated Security = True");
            baglan.Open();
            return baglan;

        }
       

        
    }
}
