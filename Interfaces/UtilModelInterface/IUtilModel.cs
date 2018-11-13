namespace UtilModelService
{
    public interface IUtilModel
    {
        string Name { get;}
        string Description { get; }
        UtilType Type { get; }

        void Run();
        //string GetIconPath();

    }
}
