namespace UtilModelService
{
    public interface IUtilModel
    {
        string Name { get;}
        string Discription { get; }
        UtilType Type { get; }

        void Run();
        //string GetIconPath();

    }
}
