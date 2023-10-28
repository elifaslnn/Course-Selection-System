using Npgsql;
using Npgsql.Internal.TypeMapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DersSecim1._1
{
    public partial class LogIn : Form
    {
        public string userType;

        public ogrenciPanel ogrenciPanel = new ogrenciPanel();

        public LogIn()
        {
            InitializeComponent();
        }

        public NpgsqlConnection conn= new NpgsqlConnection("server=localhost; port=5432; Database=dbDersSecim ; Username =postgres; Password=123 ");
 

        private void yöneticiGiriş_Load(object sender, EventArgs e)
        {
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdiTextBox = textBox1.Text.Trim();
            string sifreTextBox = textBox2.Text.Trim();
            string sorgu="";
            //kullanıcı tipine göre giriş
            if (userType == "yonetici")
            {
                sorgu = "select * from yonetici where kullaniciadi='" + kullaniciAdiTextBox + "' and sifre= '" + sifreTextBox + "'";
            }
            else if(userType=="hoca")
            {
                sorgu = "select * from hoca where kullaniciadi='" + kullaniciAdiTextBox + "' and sifre= '" + sifreTextBox + "'";
            }
            else if (userType == "ogrenci")
            {
                sorgu = "select * from ogrenci where kullaniciadi='" + kullaniciAdiTextBox + "' and sifre= '" + sifreTextBox + "'";
            }

            //message boxlar
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (kullaniciAdiTextBox.Length == 0)
            {
                MessageBox.Show("kullanıcı adı boş gecilemez");
   
            }
            else if (sifreTextBox.Length == 0)
            {
                MessageBox.Show("sifre boş gecilemez");
            }
            else if (ds.Tables[0].Rows.Count != 0)
            {
                MessageBox.Show("kullanıcı girişi başarılı");
                if (userType == "ogrenci")
                {
                    ogrenciPanel.ogrenciId = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    this.Hide();
                    ogrenciPanel.Show();
                }
                
            }
            else
            {
                MessageBox.Show("kullanıcı bulunamadı");
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.ForeColor = Color.White;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            textBox2.ForeColor = Color.White;
        }
    }
}
