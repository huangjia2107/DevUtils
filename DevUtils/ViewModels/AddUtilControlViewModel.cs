using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using UtilModelService;
using DevUtils.Models;
using System.Windows.Forms;

namespace DevUtils.ViewModels
{
    public class AddUtilControlViewModel : BindableBase
    {
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }

        private UtilType _type;
        public UtilType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set 
            {
                SetProperty(ref _location, value); 
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ScanCommand { get; set; }

        public AddUtilControlViewModel()
        {
            AddCommand = new DelegateCommand(Add, CanAdd);
            ScanCommand = new DelegateCommand(Scan);
        }
        
        private bool CanAdd()
        {
            return _location != null && !string.IsNullOrEmpty(_location.Trim());
        }

        private void Add()
        {
            new ShortcutUtilModel(_location, _description, _type);
        }

        private void Scan()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "可执行文件 (*.exe)|*.exe";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
               Location = ofd.FileName;
        }
    }
}
