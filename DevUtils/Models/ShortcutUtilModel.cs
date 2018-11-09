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
            get { return Path.GetFileNameWithoutExtension(_targetPath); }
        }

        public string Discription
        {
            get { return _discription; }
        }

        public UtilType Type
        {
            get { return _type; }
        }

        public void Run()
        {
            MessageBox.Show("Run " + Name);
        }

        #endregion

        private string _targetPath = null;
        private string _discription = null;
        private UtilType _type;

        public ShortcutUtilModel(string targetPath, string discription, UtilType type)
        {
            _targetPath = targetPath;
            _type = type;
            _discription = discription;
        }
    }
}
