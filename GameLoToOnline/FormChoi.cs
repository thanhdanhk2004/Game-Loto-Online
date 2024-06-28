using System;
using System.Collections;
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
    public partial class FormChoi : Form
    {
        public static int soNguoiChoi = 0;
        public static int demSoBong = 0;
        public static int demSoOChoBangLoTo = 0;
        private FlowLayoutPanel[] flowLayoutPanels = new FlowLayoutPanel[5];
        private String strPath = "12345678910111213141516171819202122232425262728293031323334353637383940414243444546474849";
        private String duongDanBiLoTo = Application.StartupPath + @"\PicBall\";
        private List<PictureBox> listPicBall = new List<PictureBox>();
        private List<int> danhSachBiDaXuatHien = new List<int>();
        private List<Button> mangChuaCacSoCuaCacBangLoTo = new List<Button>();
        private Button[,] soCuaMayThuNhat = new Button[6, 9];
        private Button[,] soCuaMayThuHai = new Button[6, 9];
        private Button[,] soCuaNguoiChoiBan1 = null;
        private Button[,] soCuaNguoiChoiBan2 = null;
        private Button[,] soCuaNguoiChoiBan3 = null;
        private int[] arrayWin = new int[5];
        private Boolean flag = false;
        private Boolean coNhacNen = true;
        private String[] mangChuaThoiGianAmThanh = new String[50];
        private Label[] labels = new Label[5];
        private PictureBox[] pictureBoxes = new PictureBox[5];
        private String pathHuyChuong = Application.StartupPath + @"\Images\HuyChuong.jpg";
        private int[] diemChienThang = new int[3];
        private Boolean flagClose = false;
        public FormChoi()
        {
            FormChonSoNguoiChoi formChonSoNguoiChoi = new FormChonSoNguoiChoi();
            formChonSoNguoiChoi.ShowDialog();
            InitializeComponent();
        }
        //Tạo các khung cho bảng lô tô
        private FlowLayoutPanel taoBangLoTo()
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = 185;
            flowLayoutPanel.Height = 217;
            flowLayoutPanel.BackColor = Color.White;
            return flowLayoutPanel;
        }
        // Tạo các ô cho bảng loto
        private Button taoOChoBanLoTo()
        {
            Button button = new Button();
            button.BackColor = Color.White;
            button.Width = 37;
            button.Height = 54;
            return button;
        }
        //Tạo lable cho người chiến thắng
        private Label taoLableChienThang(int x, int y, int chiSo)
        {
            Label label = new Label();
            label.Width = 80;
            label.Height = 39;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = "Chiến thắng";
            label.ForeColor = Color.Red;
            label.Location = new Point(x, y);
            label.Name = "lbNguoiChoiChienThang" + chiSo.ToString();
            return label;
        }
        private PictureBox taoHinhAnhHuyChuong(int x, int y)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(x, y);
            pictureBox.Width = 40;
            pictureBox.Height = 35;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = Image.FromFile(pathHuyChuong);
            return pictureBox;
        }
        private Boolean kiemTraSoCoTrongBang(ArrayList arrayList, int x)
        {
            foreach (int i in arrayList)
            {
                if (i == x)
                    return false;
            }
            return true;
        }
        private int taoSoTrongBangLoTo(ArrayList arrayList, int i)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int x;
            do
            {
                if (i == 1 || i == 6 || i == 11 || i == 16)
                    x = random.Next(1, 10);
                else if (i == 2 || i == 7 || i == 12 || i == 17)
                    x = random.Next(10, 20);
                else if (i == 3 || i == 8 || i == 13 || i == 18)
                    x = random.Next(20, 30);
                else if (i == 4 || i == 9 || i == 14 || i == 19)
                    x = random.Next(30, 40);
                else
                    x = random.Next(40, 50);
            } while (kiemTraSoCoTrongBang(arrayList, x) == false);
            return x;
        }
        // Tạo bảng loto hoàn chỉnh
        private void taoBangLoTo(int X, int Y, int chiSo)
        {
            FlowLayoutPanel flowLayoutPanel = taoBangLoTo();
            flowLayoutPanel.Location = new Point(X, Y);
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;// Khi không còn đủ không gian sẽ tự động xuống hàng
            ArrayList arrayList = new ArrayList();
            for (int i = 1; i <= 20; i++)
            {
                Button button = taoOChoBanLoTo();
                button.Margin = new Padding(0);// Margin = 0
                int x = taoSoTrongBangLoTo(arrayList, i);
                button.Text = x.ToString();
                arrayList.Add(x);
                flowLayoutPanel.Controls.Add(button);
                this.mangChuaCacSoCuaCacBangLoTo.Add(button);
            }
            flowLayoutPanel.Name = "flowLayoutPanel" + chiSo.ToString();
            this.flowLayoutPanels[chiSo] = flowLayoutPanel;
            this.Controls.Add(flowLayoutPanel);
        }
        // Hàm chuyển từng phần tử qua cho mảng 2 chiều số của người chơi
        //private void chuyenPhanTu(Button[,] buttons1, Button[,] buttons2)
        //{
        //    if (buttons2 != null)
        //    {
        //        for (int i = 0; i < buttons1.GetLength(0); i++)
        //        {
        //            for (int j = 0; j < buttons2.GetLength(1); j++)
        //            {
        //                if (buttons1[i, j] != null)
        //                {
        //                    buttons2[i, j] = buttons1[i, j];
        //                    buttons2[i, j].Name = string.Format("Button{0}{1}", i, j);
        //                }
        //            }
        //        }
        //    }
        //}
        //Hàm thêm số vào mảng hai chiều

        private void Button_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            if (lbSo.Text != "")
            {
                if (int.Parse(lbSo.Text.Trim()) == int.Parse(bt.Text.Trim()))
                    bt.BackColor = Color.Yellow;
            }
        }
        private void themVaoMang(Button[,] soTrongBangLoTo, Button x)
        {
            int soChiaNguyen = int.Parse(x.Text.Trim()) / 10; //Chia để xem đưa số đó vào hàng nào
            int cot = 0;
            while (true)
            {
                if (cot >= soTrongBangLoTo.GetLength(0))
                    break;
                if (soTrongBangLoTo[soChiaNguyen, cot] == null)
                    break;
                cot++;
            }
            soTrongBangLoTo[soChiaNguyen, cot] = x;
            soTrongBangLoTo[soChiaNguyen, cot].Click += Button_Click;
        }
        
        private void chuyenDuLieuTuMangMotChieuVaoMangHaiChieu(Button[,] buttons, int from, int to)
        {
            for (int i = from; i < to; i++)
                themVaoMang(buttons, this.mangChuaCacSoCuaCacBangLoTo[i]);
        }
        private void taoBangLoToChoNguoiChoi()
        {
            int x = 0, y = 350, chiSo = 2;
            x = (this.Width - 185 * FormChoi.soNguoiChoi) / 2;
            int kc = x;
            for (int i = 1; i <= FormChoi.soNguoiChoi; i++)
            {
                Button[,] buttons = new Button[6, 9];
                System.Threading.Thread.Sleep(200);// Tạm dừng luồng thực hiện
                taoBangLoTo(kc, y, chiSo);
                Label label = this.taoLableChienThang(kc + (185 / 2) - 39, 580, chiSo);
                PictureBox pictureBox = this.taoHinhAnhHuyChuong(kc + 13, 580);
                this.Controls.Add(label);
                this.Controls.Add(pictureBox);
                this.labels[chiSo] = label;
                this.pictureBoxes[chiSo++] = pictureBox;
                kc += 185 + 30;
                switch (i)
                {
                    case 1:
                        {
                            this.soCuaNguoiChoiBan1 = new Button[6, 9];
                            this.soCuaNguoiChoiBan1 = buttons; break;
                        }
                    case 2:
                        {
                            this.soCuaNguoiChoiBan2 = new Button[6, 9];
                            this.soCuaNguoiChoiBan2 = buttons; break;
                        }
                    case 3:
                        {
                            this.soCuaNguoiChoiBan3 = new Button[6, 9];
                            this.soCuaNguoiChoiBan3 = buttons; break;
                        }
                }
            }
        }
        private PictureBox PictureBoxBall(String pathPicBall)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = 67;
            pictureBox.Height = 55;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = Image.FromFile(pathPicBall);
            return pictureBox;
        }
        private Boolean kiemTraXemBiDaXuatHienHayChua(List<int> danhSachBiXuatHien, int x)
        {
            foreach (int i in danhSachBiDaXuatHien)
                if (i == x)
                    return false;
            return true;
        }
        private void xemSoRaCoTrongBangMayChoiHayKhong(Button[,] soTrongBangLoTo, int x)
        {
            int soChiaNguyen = int.Parse(lbSo.Text.Trim()) / 10;
            for (int i = 0; i < 9; i++)
            {
                if (soTrongBangLoTo[soChiaNguyen, i] == null)
                    break;
                else
                {
                    if (int.Parse(soTrongBangLoTo[soChiaNguyen, i].Text.Trim()) == x && soTrongBangLoTo[soChiaNguyen, i].BackColor != Color.Red)
                        soTrongBangLoTo[soChiaNguyen, i].BackColor = Color.Green;
                }
            }
        }
        private void rungBiLoTo()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int soBiNgauNhien = 0;
            do
            {
                soBiNgauNhien = random.Next(1, 50);
            } while (kiemTraXemBiDaXuatHienHayChua(this.danhSachBiDaXuatHien, soBiNgauNhien) == false);
            lbSo.ForeColor = Color.Black;
            lbSo.Text = soBiNgauNhien.ToString();
            xemSoRaCoTrongBangMayChoiHayKhong(soCuaMayThuNhat, soBiNgauNhien);
            xemSoRaCoTrongBangMayChoiHayKhong(soCuaMayThuHai, soBiNgauNhien);
            this.danhSachBiDaXuatHien.Add(soBiNgauNhien);
            String path = this.duongDanBiLoTo;
            if (soBiNgauNhien < 10)
                path += this.strPath.Substring(this.strPath.IndexOf(soBiNgauNhien.ToString()), 1);
            else
                path += this.strPath.Substring(this.strPath.IndexOf(soBiNgauNhien.ToString()), 2);
            if (System.IO.File.Exists(path + ".png"))
                path += ".png";
            else
                path += ".jpg";
            PictureBox pictureBoxBall = PictureBoxBall(path);
            this.flowLayoutPanelBall.Controls.Add(pictureBoxBall);
            //this.listPicBall.Add(pictureBoxBall);
        }
        
        //Hàm gắn thời gian đọc lô tô
        private void ganThoiGian()
        {
            for (int i = 2; i < this.flowLayoutPanels.Length; i++)
            {
                if (this.flowLayoutPanels[i] != null)
                    moKhoaControls(this.flowLayoutPanels[i]);
            }
            if (FormChoi.soNguoiChoi >= 1)
            {
                chuyenDuLieuTuMangMotChieuVaoMangHaiChieu(this.soCuaNguoiChoiBan1, 40, 60);
                thoiGianChay.Interval = 4000;
            }
            if (FormChoi.soNguoiChoi >= 2)
            {
                chuyenDuLieuTuMangMotChieuVaoMangHaiChieu(this.soCuaNguoiChoiBan2, 60, 80);
                thoiGianChay.Interval = 6000;
            }
            if (FormChoi.soNguoiChoi >= 3)
            {
                chuyenDuLieuTuMangMotChieuVaoMangHaiChieu(this.soCuaNguoiChoiBan3, 80, 100);
                thoiGianChay.Interval = 8000;
            }
        }
        private void PlayStateChangeHandler(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Kiểm tra nếu trạng thái phát là kết thúc
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                btBatDauChoi.Enabled = true;
                this.ganThoiGian();
                btBatDauChoi.BackColor = Color.FromArgb(192,255,192);
            }
        }
        // Ham cat am thanh dua vao mang
        private void catAmThanhDuaVaoMang()
        {
            String pathAmThanh;
            for (int i = 0; i < 50; i++)
            {
                pathAmThanh = Application.StartupPath + @"\Radio\" + (i + 1).ToString() + ".mp3";
                if (System.IO.File.Exists(pathAmThanh))
                    this.mangChuaThoiGianAmThanh[i] = pathAmThanh;
            }
        }

        private void btBatDauChoi_Click(object sender, EventArgs e)
        {
            quayLai.Enabled = false;
            quayLai.Hide();
            if (btBatDauChoi.Text.Trim() == "Bắt đầu chơi" || btBatDauChoi.Text.Trim() == "Tiếp tục")
            {
                this.flag = true;
                if (btBatDauChoi.Text.Trim() == "Bắt đầu chơi")
                {
                    btBatDauChoi.Enabled = false;
                    thoiGianChay.Interval = 28000;
                    btBatDauChoi.BackColor = Color.Gray;
                    mediaNhacNenSapChoi.URL = Application.StartupPath + @"\Radio\QuyDinhTroChoi.mp3";
                    chuyenDuLieuTuMangMotChieuVaoMangHaiChieu(this.soCuaMayThuNhat, 0, 20);
                    chuyenDuLieuTuMangMotChieuVaoMangHaiChieu(this.soCuaMayThuHai, 20, 40);
                    mediaNhacNenSapChoi.PlayStateChange += PlayStateChangeHandler;
                }
                else
                {
                    for (int i = 2; i < this.flowLayoutPanels.Length; i++)
                    {
                        if (this.flowLayoutPanels[i] != null)
                            moKhoaControls(this.flowLayoutPanels[i]);
                    }
                    ganThoiGian();
                }
                thoiGianChay.Enabled = true;
                btBatDauChoi.Text = "Tạm dừng";
            }
            else if (btBatDauChoi.Text == "Tạm dừng")
            {
                thoiGianChay.Enabled = false;
                btBatDauChoi.Text = "Tiếp tục";
                for (int i = 2; i < this.flowLayoutPanels.Length; i++)
                {
                    if (this.flowLayoutPanels[i] != null)
                        khoaControls(this.flowLayoutPanels[i]);
                }
            }
            else
            {
                flagClose = true;
                Application.Restart();
            }
        }

        private void thoiGianChay_Tick(object sender, EventArgs e)
        {
            if (lbSoBiConLai.Text == "0")
            {
                thoiGianChay.Enabled = false;
                lbSo.Font = new Font(lbSo.Font.FontFamily, 18);
                lbSo.Text = "Trò chơi kết thúc";
                for (int i = 2; i < this.flowLayoutPanels.Length; i++)
                {
                    if (this.flowLayoutPanels[i] != null)
                        khoaControls(this.flowLayoutPanels[i]);
                }
            }
            else
            {
                flowLayoutPanelBall.FlowDirection = FlowDirection.RightToLeft;
                if (85*FormChoi.demSoBong >= flowLayoutPanelBall.Width)
                {
                    flowLayoutPanelBall.Controls.RemoveAt(0);
                    FormChoi.demSoBong--;
                }
                themVaoMangChienThang();
                int win = chienThang();
                if (win == 0)
                {
                    rungBiLoTo();
                    mediaHoXoSo.URL = this.mangChuaThoiGianAmThanh[int.Parse(lbSo.Text.Trim()) - 1];
                    FormChoi.demSoBong++;
                    lbSoBiConLai.Text = (int.Parse(lbSoBiConLai.Text.Trim()) - 1).ToString();
                }
                else
                {
                    btBatDauChoi.Text = "Chơi lại";
                    if (diemChienThang[0] == int.Parse(diemMay1.Text) + 10 && diemChienThang[1] == int.Parse(diemMay2.Text) + 10 && diemChienThang[2] == int.Parse(diemCuaToi.Text) + 10)
                    {
                        diemChienThang[0] -= 6;
                        diemChienThang[1] -= 6;
                        diemChienThang[2] -= 6;
                    }
                    else if (diemChienThang[0] == int.Parse(diemMay1.Text) + 10 && diemChienThang[1] == int.Parse(diemMay2.Text) + 10)
                    {
                        diemChienThang[0] -= 5;
                        diemChienThang[1] -= 5;
                    }
                    else if (diemChienThang[0] == int.Parse(diemMay1.Text) + 10 && diemChienThang[2] == int.Parse(diemCuaToi.Text) + 10)
                    {
                        diemChienThang[0] -= 5;
                        diemChienThang[2] -= 5;
                    }
                    else if (diemChienThang[1] == int.Parse(diemMay1.Text) + 10 && diemChienThang[2] == int.Parse(diemCuaToi.Text) + 10)
                    {
                        diemChienThang[2] -= 5;
                        diemChienThang[1] -= 5;
                    }
                    else
                    {
                        //
                    }
                    for (int i = 0; i < this.diemChienThang.Length; i++)
                    {
                        if (i < 2)
                            diemChienThang[i] -= 3;
                        else
                            diemChienThang[i] -= (3 * FormChoi.soNguoiChoi);
                    }
                    diemMay1.Text = diemChienThang[0].ToString();
                    diemMay2.Text = diemChienThang[1].ToString();
                    diemCuaToi.Text = diemChienThang[2].ToString();
                    this.ghiFileDiem();
                }
            }
        }
        private int kiemTraDuDieuKienDeThang(Button[,] buttons)
        {
            for (int i = 0; i < 5; i++)
            {
                int dem = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (buttons[i, j] != null)
                    {
                        if (buttons[i, j].BackColor != Color.White)
                            dem++;
                        if(dem == 3 && buttons[i, j].BackColor != Color.White)
                        {
                            for(int k = 0;k<4;k++)
                                buttons[i,k].BackColor = Color.Red;
                        }
                    }
                }
                if (dem == 4)
                    return dem;
            }
            return 0;
        }
      
        private void themVaoMangChienThang()
        {
            this.arrayWin[0] = kiemTraDuDieuKienDeThang(this.soCuaMayThuNhat);
            this.arrayWin[1] = kiemTraDuDieuKienDeThang(this.soCuaMayThuHai);
            if (FormChoi.soNguoiChoi >= 1)
                this.arrayWin[2] = kiemTraDuDieuKienDeThang(this.soCuaNguoiChoiBan1);
            if (FormChoi.soNguoiChoi >= 2)
                this.arrayWin[3] = kiemTraDuDieuKienDeThang(this.soCuaNguoiChoiBan2);
            if (FormChoi.soNguoiChoi == 3)
                this.arrayWin[4] = kiemTraDuDieuKienDeThang(this.soCuaNguoiChoiBan3);
        }
        //Hàm khóa các controls không cho thực hiện
        private void khoaControls(FlowLayoutPanel flowLayoutPanel)
        {
            foreach (Control control in flowLayoutPanel.Controls)
                control.Enabled = false;
            flowLayoutPanel.Enabled = false;
        }
        // Hàm mở lại controls 
        private void moKhoaControls(FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.Enabled = true;
            foreach (Control control in flowLayoutPanel.Controls)
                control.Enabled = true;
        }
        //Co bang lo to chien thang
        private int chienThang()
        {
            int dem = 0;
            for (int i = 0; i < arrayWin.Length; i++)
            {
                if (this.arrayWin[i] == 4)
                {
                    thoiGianChay.Enabled = false;
                    //hieuUngMauThangChoCacButton(this.flowLayoutPanels[i]);
                    this.pictureBoxes[i].Show();
                    this.labels[i].Show();
                    diemChienThang[i] += 10;
                    dem++;
                }
            }

            return dem;
        }
        private void hieuUngMauThangChoCacButton(FlowLayoutPanel flowLayoutPanel)
        {
            foreach (Control control in flowLayoutPanel.Controls)
                control.BackColor = Color.Red;
        }
        private void docFileDiem()
        {
            String[] a = File.ReadAllLines(Application.StartupPath + @"\Data.txt");
            diemMay1.Text = a[0];
            diemMay2.Text = a[1];
            diemCuaToi.Text = a[2];
        }
        private void ghiFileDiem()
        {
            String[] a = { diemMay1.Text, diemMay2.Text, diemCuaToi.Text };
            File.WriteAllLines(Application.StartupPath + @"\Data.txt", a);
        }
        private void khoiDong()
        {
            this.labels[0] = lbChienThang1;
            this.labels[1] = lbChienThang2;
            for (int i = 0; i < this.labels.Length; i++)
                if (labels[i] != null)
                    labels[i].Hide();
            this.pictureBoxes[0] = picHuyChuong1;
            this.pictureBoxes[1] = picChienThang2;
            foreach (Control control in this.pictureBoxes)
                if (control != null)
                    control.Hide();
            this.diemChienThang[0] = int.Parse(diemMay1.Text.Trim());
            this.diemChienThang[1] = int.Parse(diemMay2.Text.Trim());
            this.diemChienThang[2] = int.Parse(diemCuaToi.Text.Trim());
        }
        private void caiAnchorChoBangLoTo()
        {
            flowLayoutPanels[0].Anchor = AnchorStyles.Left;
            flowLayoutPanels[1].Anchor = AnchorStyles.Right;
            for (int i = 2; i < this.flowLayoutPanels.Length; i++)
            {
                if (flowLayoutPanels[i] != null)
                    flowLayoutPanels[i].Anchor = AnchorStyles.Bottom;
            }
            //if (FormChoi.soNguoiChoi >= 1)
            //    flowLayoutPanels[2].Anchor = AnchorStyles.Bottom;
        }

        private void FormChoi_Load(object sender, EventArgs e)
        {
            taoBangLoTo(60, 97, 0);
            System.Threading.Thread.Sleep(100);
            taoBangLoTo(680, 97, 1);
            taoBangLoToChoNguoiChoi();
            for (int i = 0; i < this.flowLayoutPanels.Length; i++)
                if (this.flowLayoutPanels[i] != null)
                    khoaControls(this.flowLayoutPanels[i]);
            this.catAmThanhDuaVaoMang();
            mediaNhacNenSapChoi.URL = Application.StartupPath + @"\Radio\amThanhChaoMungDenTroChoi.mp3";
            mediaXoSo.URL = Application.StartupPath + @"\Radio\NhacXoSo.mp3";
            mediaXoSo.settings.volume = 20;
            mediaHoXoSo.Hide();
            mediaXoSo.Hide();
            mediaNhacNenSapChoi.Hide();
            docFileDiem();
            khoiDong();
            caiAnchorChoBangLoTo();
        }
        private void FormChoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mangChuaCacSoCuaCacBangLoTo.Clear();
            this.danhSachBiDaXuatHien.Clear();
            this.listPicBall.Clear();
            if (flagClose == false)
            {
                diemMay1.Text = diemMay2.Text = diemCuaToi.Text = "50";
                this.ghiFileDiem();
            }
            Application.Exit();
        }

        private void quayLai_Click(object sender, EventArgs e)
        {
            flagClose = true;
            Application.Restart();
        }
    }
}
