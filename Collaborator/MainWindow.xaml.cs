﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Collaborator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Connection.Instance.MainWindow = this;

            Thread Connect = new Thread(new ThreadStart(Connection.Instance.CheckInternetConnection));
            Connect.Start();

            if(User.Instance.Saved())
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
                dashboard.MainWindowInstance = this;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void CloseIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseIcon.Foreground = Brushes.Red;
        }

        private void CloseIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseIcon.Foreground = Brushes.White;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register(UsernameBox.Text, FullNameBox.Text, PasswordBox.Password);
            if (register.Perform())
            {
                LogIn();
            }
            else
            {
                MessageBox.Show("Registration failed");
            }
        }

        private void RegisterBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TopLoginPanel.Visibility = Visibility.Collapsed;
            BottomLoginPanel.Visibility = Visibility.Collapsed;
            TopRegisterPanel.Visibility = Visibility.Visible;
            FullNamePanel.Visibility = Visibility.Visible;
            BottomRegisterPanel.Visibility = Visibility.Visible;
        }

        private void LoginBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TopRegisterPanel.Visibility = Visibility.Collapsed;
            FullNamePanel.Visibility = Visibility.Collapsed;
            BottomRegisterPanel.Visibility = Visibility.Collapsed;
            TopLoginPanel.Visibility = Visibility.Visible;
            BottomLoginPanel.Visibility = Visibility.Visible;
        }
        private void LogIn()
        {
            Login login = new Login(UsernameBox.Text, PasswordBox.Password);
            if (login.Perform())
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
                dashboard.MainWindowInstance = this;
            }
            else
            {
                MessageBox.Show("Login failed");
            }
        }

    }
}
