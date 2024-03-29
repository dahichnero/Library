﻿using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public Book Bookk { get; set; }
        public List<Genre> Genres { get; set; }
        public Genre Genree { get; set; }
        public Genre GenreeMy { get; set; }
        public Provisioner Provisionere { get; set; }
        public List<Provisioner> Provisioners { get; set; }
        BookMain bookMain = new BookMain(true, new Book());
        public AddBookWindow(Book book)
        {
            InitializeComponent();
            Bookk = book;
            Genres = Session.Instance.Context.Genres.ToList();
            Provisioners = Session.Instance.Context.Provisioners.ToList();
            if (Bookk.Isbn == null)
            {
                choose.Content = "Добавление книги";
            }
            else
            {
                Provisionere = Bookk.ProvisionerNavigation;
                choose.Content = "Изменение книги";
                isbnText.IsEnabled=false;
                izdText.IsEnabled = false;
                Genree = Bookk.GenreiNavigation;
            }
            DataContext = this;
        }

        private void Backto(object sender, RoutedEventArgs e)
        {
            if (choose.Content == "Изменение книги")
            {
                Session.Instance.Context.Entry(Bookk).Reload();
            }
            BackToMain();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ren(object sender, RoutedEventArgs e)
        {
            if (choose.Content == "Изменение книги")
            {
                Session.Instance.Context.Entry(Bookk).Reload();
            }
            BackToMain();
        }

        bool IsValidBook()
        {
            Regex regex = new Regex(@"978\d{10}");
            bool res = regex.IsMatch(isbnText.Text);
            Regex regex1 = new Regex(@"^https?://\S+(?:jpg|jpeg|png)$");
            bool res1 = regex1.IsMatch(imageText.Text);
            if (imageText.Text == "")
            {
                if (Bookk.Isbn != null && res==true && Bookk.BookName.Length <= 50 && Bookk.BookName.Length > 0 && Bookk.Author.Length <= 50 && Bookk.Author.Length > 0
                && Convert.ToInt32(yearText.Text) < 2024 && Convert.ToInt32(yearText.Text) > 100 && Bookk.GenreiNavigation != null && Bookk.ProvisionerNavigation != null
                && Bookk.Isbn.Contains(Bookk.ProvisionerNavigation.ProvisionerId.ToString()) && Bookk.Count >= 1 && Bookk.Price >= 100 && Bookk.Price <= 1000)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (Bookk.Isbn != null && res == true && res1==true && Bookk.BookName.Length <= 50 && Bookk.BookName.Length > 0 && Bookk.Author.Length <= 50 && Bookk.Author.Length > 0
                && Convert.ToInt32(yearText.Text) < 2024 && Convert.ToInt32(yearText.Text) > 100 && Bookk.GenreiNavigation != null && Bookk.ProvisionerNavigation != null
                && Bookk.Isbn.Contains(Bookk.ProvisionerNavigation.ProvisionerId.ToString()) && Bookk.Count >= 1 && Bookk.Price >= 100 && Bookk.Price <= 1000)
                {
                    return true;
                }
                return false;
            }
        }
        private void addorupdate(object sender, RoutedEventArgs e)
        {
            
            if (IsValidBook()==true)
            {
                if (choose.Content == "Добавление книги")
                {
                    
                    Session.Instance.Context.Books.Add(Bookk);

                }
                try
                {
                    Session.Instance.Context.SaveChanges();
                    MessageBox.Show("Успех!");
                    BookMain bookMainTrue = new BookMain(true, Bookk);
                    bookMainTrue.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                    if (choose.Content == "Добавление книги")
                    {
                        Session.Instance.Context.Remove(Bookk);
                    }
                }
            }
            else
            {
                MessageBox.Show("Введены некорректные данные...");
            }
        }

        void BackToMain()
        {
            bookMain.Show();
            this.Close();
        }

    }
}
