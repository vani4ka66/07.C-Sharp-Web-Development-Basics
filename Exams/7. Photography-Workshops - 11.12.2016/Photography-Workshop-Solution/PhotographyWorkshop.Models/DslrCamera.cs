using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyWorkshop.Models
{
    [Table("DslrCameras")]
    public class DslrCamera : Camera
    {
        public int MaxShutterSpeed { get; set; }
    }
}
