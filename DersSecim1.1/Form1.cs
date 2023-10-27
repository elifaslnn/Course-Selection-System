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
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Form1()
        {
            InitializeComponent();
            instance = this;
        }

        public LogIn girisEkrani = new LogIn();
        private void button1_Click(object sender, EventArgs e)
        {
            girisEkrani.userType = "yonetici";
            this.Hide();
            girisEkrani.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            girisEkrani.userType = "hoca";
            this.Hide();
            girisEkrani.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            girisEkrani.userType = "ogrenci";
            this.Hide();
            girisEkrani.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
