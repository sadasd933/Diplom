using QualificationTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.Height = 400;
            Application.Current.MainWindow.Width = 400;


            MainFrame.Content = new Authorization();
            Application.Current.MainWindow.MaxWidth = 400;
            Application.Current.MainWindow.MaxHeight = 400;
            Application.Current.MainWindow.MinHeight = 400;
            Application.Current.MainWindow.MinWidth = 400;

        }


    }
}

