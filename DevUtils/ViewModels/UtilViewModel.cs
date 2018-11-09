using Prism.Mvvm;
using UtilModelService;

namespace DevUtils.ViewModels
{
    public class UtilViewModel : BindableBase
    {
        private IUtilModel _model = null;

        public UtilViewModel(IUtilModel utilModel)
        {
            _model = utilModel;
        }

        private bool _isMine;
        public bool IsMine
        {
            get { return _isMine; }
            set { SetProperty(ref _isMine, value); }
        }

        public string Name
        {
            get { return _model.Name; }
        }

        public string Discription
        {
            get { return _model.Discription; }
        }

        public UtilType Type
        {
            get { return _model.Type; }
        }

        public void Run()
        {
            _model.Run();
        }
    }
}
