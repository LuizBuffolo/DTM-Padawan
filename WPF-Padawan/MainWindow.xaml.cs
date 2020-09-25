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

        private void ClearWindow()
        {
            txtName.Text = "";
            btnInsert.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void UpdateTextbox(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                txtAdress.Text = "";
                txtPhoneNumber.Text = "";
                btnInsert.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }

            else
            {
                using (LiteDatabase db = new LiteDatabase("Filename=UserDB.db"))
                {
                    var user = db.GetCollection<User>().FindOne(x => x.Name == txtName.Text);

                    if (user != null)
                    {
                        txtAdress.Text = user.Adress;
                        txtPhoneNumber.Text = user.PhoneNumber;

                        //Se  há usuário criado no banco, bloqueia botão Insert
                        btnInsert.IsEnabled = false;
                        btnUpdate.IsEnabled = true;
                        btnDelete.IsEnabled = true;
                    }
                    else
                    {
                        //Se não há usuário criado no banco, bloqueia botões Update e Delete
                        btnInsert.IsEnabled = true;
                        btnUpdate.IsEnabled = false;
                        btnDelete.IsEnabled = false;
                    }
                }
            }
        }

        private void Btn_Insert(object sender, RoutedEventArgs e)
        {
            if (txtAdress.Text == "" || txtPhoneNumber.Text == "")
            {
                namePopUp.IsOpen = true;
            }
            else
            {
                if (!int.TryParse(txtPhoneNumber.Text, out _)){
                    numericPopUp.IsOpen = true;
                }
                else { 
                    DataBase.DataBase.AddUser(txtName.Text, txtAdress.Text, txtPhoneNumber.Text);
                    UpdateGrid();
                    ClearWindow();
                }
            }
        }

        private void Btn_Update(object sender, RoutedEventArgs e)
        {
            if (txtAdress.Text == "" || txtPhoneNumber.Text == "")
            {
                namePopUp.IsOpen = true;
            }
            else
            {
                if (!int.TryParse(txtPhoneNumber.Text, out _))
                {
                    numericPopUp.IsOpen = true;
                }
                else
                {
                    DataBase.DataBase.UpdateUser(txtName.Text, txtAdress.Text, txtPhoneNumber.Text);
                    UpdateGrid();
                    ClearWindow();
                }
            }
        }

        private void Btn_Delete(object sender, RoutedEventArgs e)
        {
            deletePopUp.IsOpen = true;
        }

        private void Btn_yesDeletePopUp(object sender, RoutedEventArgs e)
        {
            DataBase.DataBase.DeleteUser(txtName.Text);
            deletePopUp.IsOpen = false;
            ClearWindow();
            UpdateGrid();
        }

        private void Btn_ClosePopUp(object sender, RoutedEventArgs e)
        {
            namePopUp.IsOpen = false;
            deletePopUp.IsOpen = false;
            numericPopUp.IsOpen = false;
        }
    }
}
