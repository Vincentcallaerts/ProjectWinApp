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

namespace ProjectWinApp
{
    /// <summary>
    /// Interaction logic for Overvieuw.xaml
    /// </summary>
    public partial class Overvieuw : Page
    {
        public Overvieuw()
        {
            InitializeComponent();
        }

        private void btnSeeUsers_Click(object sender, RoutedEventArgs e)
        {
            fOvervieuw.Content = new SeeUsers();
        }

        private void btnSeeCustomers_Click(object sender, RoutedEventArgs e)
        {
            fOvervieuw.Content = new SeeCustomers();
        }

        private void btnSeeMagazijns_Click(object sender, RoutedEventArgs e)
        {
            fOvervieuw.Content = new SeeMagazijns();
        }
    }
}
