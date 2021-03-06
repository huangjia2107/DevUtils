﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Theme.Controls;

namespace DevUtils.Views
{
    /// <summary>
    /// Interaction logic for CommonWindow.xaml
    /// </summary>
    public partial class CommonWindow : UserWindow
    {
        public CommonWindow()
        {
            InitializeComponent();
        }

        private void CommonWindow_OnClosing(object sender, CancelEventArgs e)
        {
            BindingOperations.ClearAllBindings(this);
        }
    }
}
