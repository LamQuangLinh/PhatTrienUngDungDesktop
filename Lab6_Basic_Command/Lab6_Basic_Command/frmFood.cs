using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6_Basic_Command
{
    public partial class frmFood : Form
    {
        public frmFood()
        {
            InitializeComponent();
            
        }

        int categoryID;
        private void frmFood_Load(object sender, EventArgs e)
        {

        }
        public void LoadFood(int categoryID)
        {
            //tạo đối tượng kết nối
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            //Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            //thiết lập truy vấn cho đối tượng Command
            sqlCommand.CommandText = "SELECT Name FROM Category where ID = " + categoryID;
            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();
            //Gán tên nhóm sản phẩm cho tiêu đề
            string catName = sqlCommand.ExecuteScalar().ToString();
            this.Text = "Danh sách món ăn thuộc nhóm: " + catName;
            sqlCommand.CommandText = "SELECT * FROM Food WHERE FoodCategoryID = " + categoryID;
            //tạo đối tượng  dataAdapter
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            //Tạo DataTable để chứa dữ liệu
            DataTable dt = new DataTable("Food");
            da.Fill(dt);
            //Hiện thị danh sách món ăn lên form
            dgvFood.DataSource = dt;
            //Đóng kết nối và giải phóng bộ nhớ
            sqlConnection.Close();
            sqlConnection.Dispose();
            da.Dispose();
        }
    }
}
