using Prism.Mvvm;
using UtilModelService;

namespace DevUtils.ViewModels
{
    public class UtilViewModel : BindableBase
    {
        public IUtilModel Model { get; private set; }

        public UtilViewModel(IUtilModel utilModel)
        {
            Model = utilModel;
        }

        private bool _isMine;
        public bool IsMine
        {
            get { return _isMine; }
            set { SetProperty(ref _isMine, value); }
        }

        public string Name
        {
            get { return Model.Name; }
        }

        public string Description
        {
            get { return Model.Description; }
        }

        public UtilType Type
        {
            get { return Model.Type; }
        }

        public string Location
        {
            get { return Model.Location; }
        }

        public void Run()
        {
            Model.Run();
        }
    }
}
