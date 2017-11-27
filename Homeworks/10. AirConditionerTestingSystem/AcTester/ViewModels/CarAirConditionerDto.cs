﻿namespace ACTester.ViewModels
{
    using System;
    using System.Text;
    using AcTester.Helpers.Utilities;

    public class CarAirConditionerDto : VehicleAirConditionerDto
    {
        public CarAirConditionerDto(string manufacturer, string model, int volumeCoverage) : base(manufacturer, model, volumeCoverage)
        {
        }

        public override bool Test()
        {
            double sqrtVolume = Math.Sqrt(this.VolumeCovered);
            if (sqrtVolume < Constants.MinCarVolume)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder(base.ToString());
            print.Append("====================");
            return print.ToString();
        }
    }
}
