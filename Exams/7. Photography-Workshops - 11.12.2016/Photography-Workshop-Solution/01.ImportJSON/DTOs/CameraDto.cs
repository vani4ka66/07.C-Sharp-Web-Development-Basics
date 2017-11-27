﻿namespace _01.ImportJSON.DTOs
{
    internal class CameraDto
    {
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool IsFullFrame { get; set; }
        public int MinISO { get; set; }
        public int MaxISO { get; set; }
        public int MaxShutterSpeed { get; set; }
        public string MaxVideoResolution { get; set; }
        public int MaxFrameRate { get; set; }
    }
}
