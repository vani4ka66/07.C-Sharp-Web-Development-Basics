using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyWorkshop.Models
{
    [Table("MirrorlessCameras")]
    public class MirrorlessCamera : Camera
    {
        public string MaxVideoResolution { get; set; }
        public int MaxFrameRate { get; set; }
    }
}
