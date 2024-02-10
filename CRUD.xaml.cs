using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace firstProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class crud : Window
    {
        public crud()
        {
            InitializeComponent();
            loadData();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-T878PBG\\SQLEXPRESS;Initial Catalog=stock;Integrated Security=True");

        public void clearData()
        {
            name_txt.Clear();
            age_txt.Clear();
            gender_txt.Clear();
            city_txt.Clear();
            ID_txt.Clear();
        }

        public void loadData()
        {
            SqlCommand cmd = new SqlCommand("Select * from firstTable", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dataGrid.ItemsSource = dt.DefaultView;
            conn.Close();
        }

        public bool isValid()
        {
            string name = name_txt.Text;
            int age = int.Parse(age_txt.Text);
            string gender = gender_txt.Text;
            string city = city_txt.Text;

            if (name_txt.Text == String.Empty)
            {
                MessageBox.Show("Name is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (age_txt.Text == String.Empty)
            {
                MessageBox.Show("Age is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (gender_txt.Text == String.Empty)
            {
                MessageBox.Show("Gender is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (city_txt.Text == String.Empty)
            {
                MessageBox.Show("City is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO firstTable(NAME,AGE,GENDER,CITY) VALUES(@NAME,@AGE,@GENDER,@CITY) ", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NAME", name_txt.Text);
                    cmd.Parameters.AddWithValue("@AGE", age_txt.Text);
                    cmd.Parameters.AddWithValue("@GENDER", gender_txt.Text);
                    cmd.Parameters.AddWithValue("@CITY", city_txt.Text);
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadData();
                    MessageBox.Show("Successfully Registred", "saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    int ID = int.Parse(ID_txt.Text);
                    int age = int.Parse(age_txt.Text);
                    SqlCommand cmd = new SqlCommand("UPDATE firstTable SET NAME=@NAME,AGE=@AGE,GENDER=@GENDER,CITY=@CITY WHERE ID=@ID", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@NAME", name_txt.Text);
                    cmd.Parameters.AddWithValue("@AGE", age);
                    cmd.Parameters.AddWithValue("@GENDER", gender_txt.Text);
                    cmd.Parameters.AddWithValue("@CITY", city_txt.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully Updated", "saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    loadData();
                    clearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = int.Parse(ID_txt.Text);
                SqlCommand cmd = new SqlCommand("DELETE FROM firstTable WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Successfully Deleted", "saved", MessageBoxButton.OK, MessageBoxImage.Information);
                loadData();
                clearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        
    }
}
