using MVVMCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WM_ControlSystem.MVVM.ViewModels
{
    public class MainVM : MVVMCore.ViewModel
    {
        public MainVM(string Title)
        {
            MachineModel = new Models.WashingMachine();
            WindowTitle = Title;
            TempItems = new ObservableCollection<string>(new List<string>(
                Enum.GetValues(typeof(Models.ETemperature)).Cast<int>().Select(x => (x * 10).ToString() + " ℃")
                ));
            PowerItems = new ObservableCollection<string>(new List<string>(
                Enum.GetValues(typeof(Models.EPower)).Cast<int>().Select(x => (x * 100).ToString())
                ));
            ModeItems = new ObservableCollection<string>(new List<string>(
                Enum.GetNames(typeof(Models.EMode))
                ));
            SelectedTemp = 0;
            SelectedPower = 0;
            SelectedMode = 0;

            ClothesState = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            PowderState = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            MachineStatus = new SolidColorBrush(Color.FromRgb(255, 0, 0)); 

            PutClothesCommand = new LambdaCommand(PutClothes, CanPutClothes);
        }
        public Models.WashingMachine MachineModel;

        public string WindowTitle { get; set; }

        public ObservableCollection<string> TempItems { get; set; }
        private int _selectedTemp;
        public int SelectedTemp { get => _selectedTemp; set => Set(ref _selectedTemp, value); }

        public ObservableCollection<string> PowerItems { get; set; }
        private int _selectedPower;
        public int SelectedPower { get => _selectedPower; set => Set(ref _selectedPower, value); }

        public ObservableCollection<string> ModeItems { get; set; }
        private int _selectedMode;
        public int SelectedMode
        {
            get
            {
                return _selectedMode;
            }
            set
            {
                Set(ref _selectedMode, value);

                Models.ETemperature needTemp = Models.WashingMachine.GetTemperatureForMode((Models.EMode)(_selectedMode));
                RecomendTemp = ((int)(needTemp) * 10).ToString() + " ℃";

                Models.EPower needPower = Models.WashingMachine.GetPowerForMode((Models.EMode)(_selectedMode));
                RecomendPower = ((int)(needPower) * 100).ToString();
            }
        }
        private string _recomendTemp;
        public string RecomendTemp { get => _recomendTemp; set => Set(ref _recomendTemp, value); }

        private string _recomendPower;
        public string RecomendPower { get => _recomendPower; set => Set(ref _recomendPower, value); }

        private Brush _clothesState;
        public Brush ClothesState { get => _clothesState; set => Set(ref _clothesState, value); }

        private Brush _powderState;
        public Brush PowderState { get => _powderState; set => Set(ref _powderState, value); }

        private Brush _machineStatus;
        public Brush MachineStatus { get => _machineStatus; set => Set(ref _machineStatus, value); }

        public LambdaCommand PutClothesCommand { get; set; }

        private bool CanPutClothes(object param) => true;
        private void PutClothes(object param)
        {
            MessageBox.Show("gg");
        }


    }
}
