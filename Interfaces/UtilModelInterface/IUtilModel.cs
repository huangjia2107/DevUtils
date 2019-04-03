using System;
using System.Xml.Serialization;

namespace UtilModelService
{ 
    public interface IUtilModel
    { 
        string Token { get; }

        string Name { get; }
        string Description { get; }
        UtilType Type { get; }

        string Location { get; }

        void Run();
        //string GetIconPath();

    }
}
