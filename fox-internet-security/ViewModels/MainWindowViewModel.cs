using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace fox_internet_security.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase _currentPage;

        //byd，记住这里的命名和侧边管理器的文件，不要用View做分割名，不然会变得不行，看viewlocator代码就知道了
        //写在注释里，警钟撅烂
        private readonly OverviewViewModel _overviewView = new();
        private readonly UpdateViewModel _updateView = new();
        private readonly ScanViewModel _scanView = new();
        private readonly ToolViewModel _toolView = new();
        private readonly SettingViewModel _settingView = new();

        public MainWindowViewModel()
        {
            CurrentPage = _overviewView;
        }


        [RelayCommand]
        private void GoToOverview()
        {
            CurrentPage = _overviewView;
        }

        [RelayCommand]
        private void GoToUpdate()
        {
            CurrentPage = _updateView;
        }

        [RelayCommand]
        private void GoToScan()
        {
            CurrentPage = _scanView;
        }

        [RelayCommand]
        private void GoToTool()
        {
            CurrentPage = _toolView;
        }

        [RelayCommand]
        private void GoToSetting()
        {
            CurrentPage = _settingView;
        }
    }
}
