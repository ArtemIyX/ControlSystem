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
using System.Windows.Threading;
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
            PutPowderCommand = new LambdaCommand(PutPowder, CanPutPowder);
            StartCommand = new LambdaCommand(StartMachine, CanStart);

            ParamsEnabled = true;
            ProgressBarValue = 0.0;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

        }

        private DispatcherTimer dispatcherTimer;

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
        private bool _paramsEnabled;
        public bool ParamsEnabled { get => _paramsEnabled; set => Set(ref _paramsEnabled, value); }

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

        private double _progressBarValue;
        public double ProgressBarValue { get => _progressBarValue; set => Set(ref _progressBarValue, value); }


        public LambdaCommand PutClothesCommand { get; set; }
        public LambdaCommand PutPowderCommand { get; set; }
        public LambdaCommand StartCommand { get; set; }

        Color GetColor(bool red) => Color.FromRgb((byte)(red ? 255 : 0), (byte)(red ? 0 : 255), 0);
        void SetClothesColor(bool red) => ClothesState = new SolidColorBrush(GetColor(red));
        void SetPowderColor(bool red) => PowderState = new SolidColorBrush(GetColor(red));
        void SetMachineColor(bool red) => MachineState = new SolidColorBrush(GetColor(red));

        private bool CanPutClothes(object param) => !MachineModel.Turned;
        private void PutClothes(object param)
        {
            MachineModel.Clothes = true;
            SetClothesColor(false);
        }
        private bool CanPutPowder(object param) => !MachineModel.Turned;
        private void PutPowder(object param)
        {
            MachineModel.Powder = true;
            SetPowderColor(false);
        }
        private bool CanStart(object param) => MachineModel.Clothes && MachineModel.Powder;
        private void StartMachine(object param)
        {
            if (CanStart(param))
            {
                MachineModel.Turned = true;
                SetMachineColor(false);
                ParamsEnabled = false;
                ProgressBarValue += 10.0;
                dispatcherTimer.Start();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ProgressBarValue += 10.0;
            if(ProgressBarValue >= 100.0)
            {
                dispatcherTimer.Stop();
                ProgressBarValue = 0.0;

                MachineModel.Turned = false;
                ParamsEnabled = true;
                SetMachineColor(true);
                SetClothesColor(true);
                SetPowderColor(true);
                MessageBox.Show("washing completed");
            }
        }

    }
}
