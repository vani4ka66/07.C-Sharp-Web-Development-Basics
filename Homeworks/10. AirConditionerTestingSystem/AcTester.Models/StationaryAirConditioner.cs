namespace AcTester.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using AcTester.Helpers.Enumerations;
    using AcTester.Models.Attributes;

    [Table("StationaryAirConditioners")]
    public class StationaryAirConditioner : AirConditioner
    {
        [PowerUsage, Order(Order = 4)]
        public int PowerUsage
        {
            get; set;
        }

        [Order(Order = 3)]
        public EnergyEfficiencyRating RequiredEnergyEfficiencyRating { get; set; }
    }
}
