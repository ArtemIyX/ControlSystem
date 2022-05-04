using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SelectedTemp = 0;
            PowerItems = new ObservableCollection<string>(new List<string>(
                Enum.GetValues(typeof(Models.EPower)).Cast<int>().Select(x => (x * 100).ToString())
                ));
        }
        public Models.WashingMachine MachineModel;

        public string WindowTitle { get; set; }
        
        public ObservableCollection<string> TempItems { get; set; }
        public int SelectedTemp { get; set; }

        public ObservableCollection<string> PowerItems { get; set; }
        public int SelectedPower { get; set; }
    }
}
