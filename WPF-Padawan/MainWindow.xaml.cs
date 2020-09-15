using System;
using System.Collections.Generic;
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
using LiteDB;
using WPF_Padawan.Models;

namespace WPF_Padawan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateGrid();
        }
        
        private void UpdateGrid()
        {
            using (LiteDatabase db = new LiteDatabase("Filename=UserDB.db"))
            {
                List<User> users = db.GetCollection<User>().FindAll().ToList();

                WindowDataGrid.ItemsSource = users;
            }
        }

        private void Btn_Insert(object sender, RoutedEventArgs e)
        {
            DataBase.DataBase.AddUser(txtName.Text, txtAdress.Text, txtPhoneNumber.Text);
            UpdateGrid();
        }
        
    }
}
