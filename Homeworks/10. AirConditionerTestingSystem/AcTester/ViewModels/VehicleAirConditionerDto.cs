namespace ACTester.ViewModels
{
    using System;
    using System.Text;
    using AcTester.Helpers.Utilities;
    using ACTester.ViewModels;

    public abstract class VehicleAirConditionerDto : AirConditionerDto
    {
        private int volumeCovered;

        protected VehicleAirConditionerDto(string manufacturer, string model, int volumeCoverage) : base(manufacturer, model)
        {
            this.VolumeCovered = volumeCoverage;
        }


        public int VolumeCovered
        {
            get
            {
                return this.volumeCovered;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Volume Covered"));
                }

                this.volumeCovered = value;
            }
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Volume Covered: {0}", this.VolumeCovered));
            return print.ToString();
        }
    }
}
