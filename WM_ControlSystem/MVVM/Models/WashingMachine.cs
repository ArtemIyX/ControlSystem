using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM_ControlSystem.MVVM.Models
{
    public enum ETemperature
    {
        Thirty = 3,
        Forty = 4,
        Fifty = 5,
        Sixty = 6,
        Seventy = 7,
        Eighty = 8,
        Ninety = 9
    }
    public enum EPower
    {
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10
    }
    public class WashingMachine
    {
        public WashingMachine()
        {
            _currentTemperature = ETemperature.Thirty;
            _currentPower = EPower.Five;
            _turned = false;
            _clothes = false;
            _powder = false;
        }

        private ETemperature _currentTemperature;
        public ETemperature CurrentTemperature { get => _currentTemperature; }
        public int CurrentTemperatureInt { get => ((int)(_currentTemperature)) * 10; }

        private EPower _currentPower;
        public EPower CurentPower { get => _currentPower; }
        public int CurrentPowerInt { get => ((int)(_currentPower)) * 100; }

        private bool _turned;
        public bool Turned { get => _turned; set => _turned = value; }

        private bool _clothes;
        public bool Clothes { get => _clothes; set => _clothes = value; }

        private bool _powder;
        public bool Powder { get => _powder; set => _powder = value; }
    }
}
