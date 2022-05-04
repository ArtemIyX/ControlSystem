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

            //ClothesState = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            SetClothesColor(true);
            //PowderState = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            SetPowderColor(true);
            //MachineState = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            SetMachineColor(true);

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

        private Brush _machineState;
        public Brush MachineState { get => _machineState; set => Set(ref _machineState, value); }

        public LambdaCommand PutClothesCommand { get; set; }

        Color GetColor(bool red) => Color.FromRgb((byte)(red ? 255 : 0), (byte)(red ? 0 : 255), 0);
        void SetClothesColor(bool red) => ClothesState = new SolidColorBrush(GetColor(red));
        void SetPowderColor(bool red) => PowderState = new SolidColorBrush(GetColor(red));
        void SetMachineColor(bool red) => MachineState = new SolidColorBrush(GetColor(red));


        private bool CanPutClothes(object param) => true;
        private void PutClothes(object param)
        {
            MachineModel.Clothes = true;
            ClothesState = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }
        private bool CanPutPowder(object param) => true;
        private void PutPowder(object param)
        {
            MachineModel.Powder = true;
            SetClothesColor(true);
        }



    }
}
