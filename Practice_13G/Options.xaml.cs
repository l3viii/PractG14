﻿using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Practice_13G
{
    /// <summary>
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
        }
        private void SetTableSize_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("config.ini"))
            {
                writer.WriteLine(column.Text);
                writer.WriteLine(row.Text);
                Close();
            }
        }
    }
}
