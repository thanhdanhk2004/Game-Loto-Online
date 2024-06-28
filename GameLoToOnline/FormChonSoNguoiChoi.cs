using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace GameLoToOnline
{
    public partial class FormChonSoNguoiChoi : Form
    {
        private String path = Application.StartupPath+ @"\Images\off.jpg";
        public FormChonSoNguoiChoi()
        {
            InitializeComponent();
        }

        private void FormChonSoNguoiChoi_Load(object sender, EventArgs e)
        {
            amNhacNen.URL = Application.StartupPath + @"\Radio\NhacNen.mp3";
            amNhacNen.Hide();
        }

        private void radioAmThanh_Click(object sender, EventArgs e)
        {
            if (path == (Application.StartupPath + @"\Images\off.jpg"))
            {
                amNhacNen.URL = "";
                this.path = Application.StartupPath + @"\Images\on.jpg";
                radioAmThanh.Image = Image.FromFile(this.path);
            }
            else
            {
                amNhacNen.URL = Application.StartupPath + @"\Radio\NhacNen.mp3";
                this.path = Application.StartupPath + @"\Images\off.jpg";
                radioAmThanh.Image = Image.FromFile(this.path);
            }
        }

        private void bt1The_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            String s = button.Text.Trim();
            FormChoi.soNguoiChoi = int.Parse(s.Substring(0, 1));
            amNhacNen.URL = "";
            this.Close();
        }

        private void FormChonSoNguoiChoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormChoi.soNguoiChoi == 0)
                Application.Exit();
        }
    }
}
