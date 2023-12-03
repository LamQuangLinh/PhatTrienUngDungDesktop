using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2112998_LamQuangLinh_Lab4
{
    public partial class frmQuanLy : Form
    {
        QuanLySinhVien ql = new QuanLySinhVien();
        public frmQuanLy()
        {
            InitializeComponent();
        }

        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            sv.MSSV = this.mtxtMSSV.Text;
            sv.HVT = this.txtHVT.Text;
            sv.Email = this.txtEmail.Text;
            sv.DiaChi = this.txtDiaChi.Text;
            sv.NgaySinh = this.dtpNgaySinh.Value;
            sv.Lop = this.cbbLop.Text;
            sv.Hinh = this.txtHinh.Text;
            if (rbNu.Checked)
            {
                gt = false;
            }
            sv.Phai = gt;
            return sv;
        }

        private SinhVien GetSinhVienLV(ListViewItem lvitem)
        {
            SinhVien sv = new SinhVien();
            sv.MSSV = lvitem.SubItems[0].Text;
            sv.HVT = lvitem.SubItems[1].Text;
            sv.Email = lvitem.SubItems[2].Text;
            sv.DiaChi = lvitem.SubItems[3].Text;
            sv.NgaySinh = DateTime.Parse(lvitem.SubItems[4].Text);
            sv.Phai = false;
            if (string.Compare(lvitem.SubItems[5].Text, "Nam") == 0)
            {
                sv.Phai = true;
            }
            sv.Lop = lvitem.SubItems[6].Text;
            sv.SDT = lvitem.SubItems[7].Text;
            return sv;
        }

        private void ThietLapThongTin(SinhVien sv)
        {
            this.mtxtMSSV.Text = sv.MSSV;
            this.txtHVT.Text = sv.HVT;
            this.txtEmail.Text = sv.Email;
            this.txtDiaChi.Text = sv.DiaChi;
            this.dtpNgaySinh.Value = sv.NgaySinh;
            this.rbNu.Checked = true;
            if (sv.Phai)
            {
                this.rbNam.Checked = true;
            }
            this.cbbLop.Text = sv.Lop;
            this.mtxtSDT.Text = sv.SDT;
        }

        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MSSV);
            lvitem.SubItems.Add(sv.HVT);
            lvitem.SubItems.Add(sv.Email);
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            string gt = "Nữ";
            if (sv.Phai)
            {
                gt = "Nam";
            }
            lvitem.SubItems.Add(gt);
            lvitem.SubItems.Add(sv.Lop);
            lvitem.SubItems.Add(sv.SDT);
            this.lvSinhVien.Items.Add(lvitem);
        }

        private void LoadListView()
        {
            this.lvSinhVien.Items.Clear();
            foreach (SinhVien sv in ql.DSSV)
                ThemSV(sv);
        }

        private void btnHinh_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbHinh.ImageLocation = openFileDialog1.FileName;
                txtHinh.Text = openFileDialog1.FileName;
            }
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            this.mtxtMSSV.Text = "";
            this.txtHVT.Text = "";
            this.txtEmail.Text = "";
            this.txtDiaChi.Text = "";
            this.dtpNgaySinh.Value = DateTime.Now;
            this.rbNam.Checked = true;
            this.cbbLop.Text = this.cbbLop.Items[0].ToString();
            this.mtxtSDT.Text = "";
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            SinhVien sv = GetSinhVien();
            SinhVien kq = ql.Tim(sv.MSSV, delegate (object a, object b) { return (b as SinhVien).MSSV.CompareTo(a.ToString()); });
            if (kq != null)
            {
                bool kqsua;
                kqsua = ql.Sua(sv, sv.MSSV, SoSanhTheoMa);
                if (kqsua)
                    this.LoadListView();
            }
            else
            {
                this.ql.Them(sv);
                this.LoadListView();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu thay đổi?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                SinhVien sv = GetSinhVien();
                SinhVien kq = ql.Tim(sv.MSSV, delegate (object a, object b) { return (b as SinhVien).MSSV.CompareTo(a.ToString()); });
                if (kq != null)
                {
                    bool kqsua;
                    kqsua = ql.Sua(sv, sv.MSSV, SoSanhTheoMa);
                    if (kqsua)
                        this.LoadListView();
                }
                else
                {
                    this.ql.Them(sv);
                    this.LoadListView();

                }
            }
            else
                Application.Exit();
        }
        private int SoSanhTheoMa(object obj1, object obj2)
        {
            SinhVien sv = obj2 as SinhVien;
            return sv.MSSV.CompareTo(obj1);
        }

        private void frmQuanLy_Load(object sender, EventArgs e)
        {
            ql = new QuanLySinhVien();
            ql.DocTuFile();
            LoadListView();
        }

        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvSinhVien.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvitem = this.lvSinhVien.SelectedItems[0];
                SinhVien sv = GetSinhVienLV(lvitem);
                ThietLapThongTin(sv);
            }
        }

        private void lvSinhVien_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lvSinhVien_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void xoáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count, i;
            ListViewItem lvitem;
            count = this.lvSinhVien.Items.Count - 1;
            for (i = count; i >= 0; i--)
            {
                lvitem = this.lvSinhVien.Items[i];
                if (lvitem.Checked)
                    ql.Xoa(lvitem.SubItems[0].Text, SoSanhTheoMa);
            }
            this.LoadListView();
            this.btnMacDinh.PerformClick();
        }

        private void tảiLạiDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ql = new QuanLySinhVien();
            ql.DocTuFile();
            LoadListView();
            MessageBox.Show("Đã tải lại danh sách!");
        }

        private void pbHinh_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void mtxtMSSV_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtxtSDT_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void rbNu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtHinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtHVT_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ctxtmnChucNang_Opening(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
