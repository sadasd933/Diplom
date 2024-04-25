﻿using System;
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

namespace QualificationTest
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTB.Text.ToString().Trim();
            string pass = passwordTB.Password.ToString().Trim();

            if (login.Length < 5)
            {
                loginTB.ToolTip = "Поле введено некорректно!";
                loginTB.Background = Brushes.DarkRed;
            }
            else if (pass.Length<5)
            {
                passwordTB.ToolTip = "Поле введено некорректно!";
                passwordTB.Background = Brushes.DarkRed;
            }
            else
            {
                loginTB.ToolTip = "!";
                loginTB.Background = Brushes.Transparent;
                passwordTB.ToolTip = "!";
                passwordTB.Background = Brushes.Transparent;

                Employee authEmployee = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authEmployee = db.Employees.Where(b=>b.EmployeeLogin==login && b.EmployeePassword==pass).FirstOrDefault();
                }
                if(authEmployee != null)
                {
                    MessageBox.Show("Добро пожаловать!");
                    NavigationService.Navigate(new MainProgram());

                }
            }

        }
    }
}
