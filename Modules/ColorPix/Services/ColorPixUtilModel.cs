﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UtilModelService;

namespace ColorPix.Services
{
    public class ColorPixUtilModel : IUtilModel
    {
        #region IUtilModel Members

        public string Name
        {
            get { return "Color Pix"; }
        }

        public string Discription
        {
            get { return "this is a discription about Color Pix"; }
        }

        public UtilType Type
        {
            get { return UtilType.Color; }
        }

        public void Run()
        {
            MessageBox.Show("Run ColorPix");
        }

        #endregion
    }
}