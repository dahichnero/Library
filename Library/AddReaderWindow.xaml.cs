using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для AddReaderWindow.xaml
    /// </summary>
    public partial class AddReaderWindow : Window
    {
        public Reader Readerr { get; set; }
        BookMain bookMain = new BookMain(true, new Book());
        public AddReaderWindow(Reader reader)
        {
            Readerr = reader;
            InitializeComponent();
        }

        private void backTo(object sender, RoutedEventArgs e)
        {
            if (Readerr.ReaderId != 0)
            {
                Session.Instance.Context.Entry(Readerr).Reload();
            }
            BackToMain();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        bool IsValidEmailPhone()
        {
            bool email = false;
            bool phone = false;
            if (Readerr.ReaderEmail != null && Readerr.Phone != null)
            {
                Regex regex = new Regex(@"[A-Za-z]+[A-Za-z0-9]*@[A-Za-z]+\.[A-Za-z]+");
                Regex regex1 = new Regex(@"\+7\d{10}");
                email = regex.IsMatch(Readerr.ReaderEmail);
                phone = regex1.IsMatch(Readerr.Phone);
            }
            if (Readerr.ReaderName != null && Readerr.ReaderName.Length > 0 && Readerr.ReaderName.Length <= 50 && Readerr.LastName != null &&
                Readerr.LastName.Length > 0 && Readerr.LastName.Length <= 50 &&
                email == true && phone == true && Readerr.SurName != null && Readerr.SurName.Length > 0 && Readerr.SurName.Length <= 50)
            {
                return true;
            }
            return false;
        }

        private void add(object sender, RoutedEventArgs e)
        {
            
            if (IsValidEmailPhone()==true)
            {
                if (Readerr.ReaderId == 0)
                {
                    Session.Instance.Context.Readers.Add(Readerr);
                }
                try
                {
                    Session.Instance.Context.SaveChanges();
                    MessageBox.Show("Успех");
                    BackToMain();
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                    Session.Instance.Context.Readers.Remove(Readerr);
                }
            }
            else
            {
                MessageBox.Show("Введены неверные данные...");
            }
        }

        private void rem(object sender, RoutedEventArgs e)
        {
            if (Readerr.ReaderId != 0)
            {
                Session.Instance.Context.Entry(Readerr).Reload();
            }
            BackToMain();
        }
        void BackToMain()
        {
            bookMain.Show();
            this.Close();
        }
    }
}
