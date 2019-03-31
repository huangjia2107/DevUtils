using System;
using System.Xml.Serialization;

namespace UtilModelService
{ 
    public abstract class UtilModel
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract UtilType Type { get; }

        public abstract string Location { get; set; }

        public abstract void Run();
        //string GetIconPath();

    }
}
