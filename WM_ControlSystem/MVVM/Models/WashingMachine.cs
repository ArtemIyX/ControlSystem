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
    public enum EMode
    {
        Cotton,
        Synthetics,
        Wool,
        Quick,
        KidsClothes
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

        public static ETemperature GetTemperatureForMode(EMode mode)
        {
            switch (mode)
            {
                case EMode.Cotton: return ETemperature.Ninety;
                case EMode.Synthetics: return ETemperature.Forty;
                case EMode.Wool: return ETemperature.Forty;
                case EMode.Quick: return ETemperature.Thirty;
                case EMode.KidsClothes: return ETemperature.Ninety;
            }
            return ETemperature.Fifty;
        }
        public static EPower GetPowerForMode(EMode mode)
        {
            switch (mode)
            {
                case EMode.Cotton: return EPower.Ten;
                case EMode.Synthetics: return EPower.Eight;
                case EMode.Wool: return EPower.Seven;
                case EMode.Quick: return EPower.Ten;
                case EMode.KidsClothes: return EPower.Five;
            }
            return EPower.Five;
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
