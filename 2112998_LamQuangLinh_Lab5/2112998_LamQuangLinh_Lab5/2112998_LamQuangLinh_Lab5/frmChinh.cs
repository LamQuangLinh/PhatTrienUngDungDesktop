using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2112998_LamQuangLinh_Lab5
{
    public partial class frmChinh : Form
    {
        public frmChinh()
        {
            InitializeComponent();
        }
        QuanLySinhVIen qlsv=new QuanLySinhVIen();
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            List<string> monhoc = new List<string>();
            sv.MSSV = mtxtMSSV.Text;
            sv.HoVaTenLot = txtHVTL.Text;
            sv.NgaySinh = dtpNgaySinh.Value;
            sv.SoCMND = mtxtSoCMND.Text;
            sv.DiaChiLienlac = txtDiaChi.Text;
            sv.Ten = txtTen.Text;
            sv.Lop = cbbLop.Text;
            sv.SoDienThoai = mtxtSoDT.Text;

            if (rdNu.Checked)
                gt= false;
            sv.GioiTinh = gt;
            for (int i = 0; i < clbmonhoc.Items.Count; i++)
                if (clbmonhoc.GetItemChecked(i))
                    monhoc.Add(clbmonhoc.Items[i].ToString());
            sv.monHoc = monhoc;
            return sv;

        }

        private void ThemSV( SinhVien sv)
        {
            ListViewItem lvitem  = new ListViewItem(sv.MSSV);
            lvitem.SubItems.Add(sv.HoVaTenLot);
            lvitem.SubItems.Add(sv.Ten);
            lvitem.SubItems.Add(sv.SoCMND);
            lvitem.SubItems.Add(sv.DiaChiLienlac);
            if(sv.GioiTinh)
                lvitem.SubItems.Add("Nam");
            else
                lvitem.SubItems.Add("Nu");
            lvitem.SubItems.Add(sv.NgaySinh.ToShortTimeString());
            lvitem.SubItems.Add(sv.Lop);
            lvitem.SubItems.Add(sv.SoDienThoai);
            string mh = "";
            foreach (string s in sv.monHoc)
                mh += s + ",";
            mh = mh.Substring(0, mh.Length - 1);
            lvitem.SubItems.Add(mh);
            this.lvSinhVien.Items.Add(lvitem);
        }
        private void LoadLV()
        {
            this.lvSinhVien.Items.Clear();
            foreach (var item in qlsv.dssv)
            {
                ThemSV(item);
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            SinhVien sv=GetSinhVien();
            SinhVien kq = qlsv.Tim(sv.MSSV);
            if (kq != null)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi thêm dữ liệu",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                qlsv.Them(sv);
                LoadLV();
            }
        }

        private void frmChinh_Load(object sender, EventArgs e)
        {
            qlsv.DocTuFileTXT();
            LoadLV();
        }
    }

}
