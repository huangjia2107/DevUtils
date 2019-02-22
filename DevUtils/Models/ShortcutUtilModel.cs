using System.Diagnostics;
using System.IO;
using System.Windows;
using UtilModelService;

namespace DevUtils.Models
{
    public class ShortcutUtilModel : IUtilModel
    {
        #region IUtilModel Members

        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(_location); }
        }

        public string Description
        {
            get { return _description; }
        }

        public UtilType Type
        {
            get { return _type; }
        }

        public void Run()
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
