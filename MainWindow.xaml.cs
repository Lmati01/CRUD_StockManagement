using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        public bool isvalid()
        {
            if (login_txt.Text != "admin" || password_txt.Password != "admin")
            {
                MessageBox.Show("Login or Password incorrect ", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    if (isvalid()) 
                    { 
                        crud crud = new crud();
                        crud.Show();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: ", ex.Message);
                }
            
        }

    }
}
