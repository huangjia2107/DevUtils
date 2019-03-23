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
using DevUtils.Datas;
using System.Collections.ObjectModel;
using DevUtils.Helps;
using Prism.Ioc;
using CommonServiceLocator;
using Prism.Events;
using DevUtils.Events;

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

        private IContainerExtension _container = null;
        private IEventAggregator _eventAggregator = null;
        private AddUtilEvent _addUtilEvent = null;

        private UtilData _utilData = null;

        public AddUtilControlViewModel(UtilType utilType)
        {
            _type = utilType;

            _container = ServiceLocator.Current.GetInstance<IContainerExtension>();
            _utilData = _container.Resolve<AppData>().UtilsData;

            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _addUtilEvent = _eventAggregator.GetEvent<AddUtilEvent>();

            AddCommand = new DelegateCommand(Add, CanAdd);
            ScanCommand = new DelegateCommand(Scan); 
        }

        private bool CanAdd()
        {
            return _location != null && !string.IsNullOrEmpty(_location.Trim());
        }

        private void Add()
        {
            if (SelectedIndex == 0)
            {
                Add(new ShortcutUtilModel(_location, _description, _type));
            }
            else
            {
                var module = DynamicModuleHelps.Instance().LoadModule(_location);
                if (module != null)
                {
                    var utilModel = _container.Resolve<IUtilModel>(module.ModuleName);
                    utilModel.Location = _location;

                    //copy files
                    Add(utilModel);
                }
            }
        }

        private void Add(IUtilModel utilModel)
        {
            //TO DO: 检测文件是否已经添加过
            _utilData.AllUtils.Add(utilModel);
            _addUtilEvent.Publish(utilModel); 
        }

        private void Scan()
        {
            var ofd = new OpenFileDialog
            {
                Filter = SelectedIndex == 0 ? "可执行文件 (*.exe)|*.exe" : "插件文件 (*.dll)|*.dll",
                RestoreDirectory = true,
                Multiselect = SelectedIndex != 0
            };

            if (ofd.ShowDialog() == DialogResult.OK)
                Location = ofd.FileName;
        }
    }
}
