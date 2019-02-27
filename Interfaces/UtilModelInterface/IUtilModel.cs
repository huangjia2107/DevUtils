namespace UtilModelService
{
    public interface IUtilModel
    {
        string Name { get; }
        string Description { get; }
        UtilType Type { get; }

        string Location { get; set; }

        void Run();
        //string GetIconPath();

    }
}
