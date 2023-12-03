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



namespace Lab6_Basic_Command
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = RestaurantManagement; integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand =  sqlConnection.CreateCommand();
            string query = "SELECT ID, Name, Type FROM Category";
            sqlCommand.CommandText = query;
            sqlConnection.Open();
            SqlDataReader sqlDataReader =sqlCommand.ExecuteReader();
            this.DisplayCategory(sqlDataReader);
            sqlConnection.Close();
        }
        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                lvCategory.Items.Add(item);
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());


            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.;database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO Category(Name, [Type])" + "VALUES (N'" + txtName.Text + "'," + txtType.Text + ")";
            sqlConection.Open();
            int numOfRowEffected = sqlCommand.ExecuteNonQuery();
            sqlConection.Close();
            if (numOfRowEffected == 1)
            {
                MessageBox.Show("Thêm món ăn thành công!");
                btnLoad.PerformClick();
                txtName.Text = "";
                txtType.Text = "";
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại!");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.;database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConection.CreateCommand();
            sqlCommand.CommandText = "UPDATE Category SET Name = N'" + txtName.Text + ", [Type] = " + txtType.Text + "WHERE ID = " + txtID.Text;
            sqlConection.Open();
            int numOfRowEffected = sqlCommand.ExecuteNonQuery();
            sqlConection.Close();
            if (numOfRowEffected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                item.SubItems[1].Text = txtName.Text;
                item.SubItems[2].Text = txtType.Text;
                txtName.Text = "";
                txtID.Text = "";
                txtType.Text = "";
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                MessageBox.Show("Cập nhật nhóm món ăn thành công!");
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại!");
            }
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvCategory.SelectedItems[0];
            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[1].Text ==  "0" ? "Thức uống" : "Đồ ăn";

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.;database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM Category WHERE ID = " + txtID.Text;
            sqlConection.Open();
            int numOfRowEffected = sqlCommand.ExecuteNonQuery();
            sqlConection.Close();
            if (numOfRowEffected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                lvCategory.Items.Remove(item);
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                MessageBox.Show("Xóa món ăn thành công!");
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại!");
            }
        }




        private void tsmDel_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0)
                btnDelete.PerformClick();
        }

        private void tsmView_Click(object sender, EventArgs e)
        {
            if(txtID.Text != "")
            {
                frmFood FoodForm = new frmFood();
                FoodForm.Show(this);
                FoodForm.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }
    }
}
