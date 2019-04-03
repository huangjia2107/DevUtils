using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using UtilModelService;

namespace DevUtils.Models
{ 
    public class UtilModel : IUtilModel
    {
        #region IUtilModel Members 

        public string Token
        {
            get
            {
                if (string.IsNullOrEmpty(_token))
                    _token = Guid.NewGuid().ToString();

                return _token;
            }    
        }

        public string Name => (string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_location)) ? Path.GetFileNameWithoutExtension(_location) : _name;

        public string Description => _description;

        public UtilType Type => _type;

        public string Location => _location;

        public void Run()
        { 
            if (File.Exists(_location))
                Process.Start(_location);
            else
                MessageBox.Show("Do not find " + Name);
        }

        #endregion

        private string _token = null;
        private string _name = null;
        private string _description = null;
        private UtilType _type;

        private string _location = null;

        public UtilModel(string location, string description, UtilType type)
        {
            _location = location;
            _type = type;
            _description = description;
        }
         
        [JsonConstructor]
        public UtilModel(string token, string name, string description, string location, UtilType type)
        {
            _token = token;
            _name = name;
            _description = description;
            _location = location;
            _type = type;
        }
    }
}
