namespace AcTester.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using AcTester.Models.Attributes;

    [Table("VehicleAirConditioners")]
    public abstract class VehicleAirConditioner : AirConditioner
    {
        [Order(Order = 3)]
        public int VolumeCovered { get; set; }
    }
}
