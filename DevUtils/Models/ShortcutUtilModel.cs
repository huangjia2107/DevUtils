using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using UtilModelService;

namespace DevUtils.Models
{
    [Serializable]
    public class ShortcutUtilModel : UtilModel
    {
        #region IUtilModel Members 

        public override string Name => Path.GetFileNameWithoutExtension(_location);

        public override string Description => _description;

        public override UtilType Type => _type;

        public override string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public override void Run()
        {
            //MessageBox.Show("Run " + Name);
            if (File.Exists(_location))
                Process.Start(_location);
            else
                MessageBox.Show("Do not find " + Name);
        } 

        #endregion

        private string _location = null;
        private string _description = null;
        private UtilType _type;

        public ShortcutUtilModel(string location, string description, UtilType type)
        {
            _location = location;
            _type = type;
            _description = description;
        }
    }
}
