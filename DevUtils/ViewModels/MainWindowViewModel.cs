using Prism.Mvvm;

namespace DevUtils.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "DevUtils";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
