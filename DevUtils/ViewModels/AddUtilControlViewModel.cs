﻿using System;
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
        private UtilData _utilData = null;

        public AddUtilControlViewModel(IContainerExtension container)
        {
            _container = container;
            _utilData = container.Resolve<AppData>().UtilsData;

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

            var newModel = new UtilViewModel(utilModel);
            _utilData.AllUtils.Add(newModel);

            var newClassifiedUtil = new ClassifiedUtil
            {
                Type = utilModel.Type,
                Utils = new ObservableCollection<UtilViewModel>() { newModel }
            };

            var isFinish = false;
            for (int i = 0; i < _utilData.ClassifiedUtils.Count; i++)
            {
                var classifiedUtil = _utilData.ClassifiedUtils[i];
                if (classifiedUtil.Type == utilModel.Type)
                {
                    classifiedUtil.Utils.Add(newModel);
                    return;
                }

                if (!isFinish && classifiedUtil.Type > utilModel.Type)
                {
                    newClassifiedUtil.Index = i;
                    _utilData.ClassifiedUtils.Insert(i, newClassifiedUtil);
                    isFinish = true;
                    continue;
                }

                if (isFinish)
                    classifiedUtil.Index++;
            }

            if (!isFinish)
            {
                newClassifiedUtil.Index = _utilData.ClassifiedUtils.Count;
                _utilData.ClassifiedUtils.Add(newClassifiedUtil);
            }
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
