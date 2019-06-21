using System.IO;
using DevUtils.Datas;
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

        private Status _status = Status.Normal;
        public Status Status
        {
            get { return File.Exists(Model.Location) ? _status : Status.NoExist; }
            set { SetProperty(ref _status, value); }
        }

        public string Token => Model.Token;

        public string Name => Model.Name;

        public string Description => Model.Description;

        public UtilType Type => Model.Type;

        public string Location => Model.Location;

        public void Run()
        {
            Model.Run();
        }
    }
}
