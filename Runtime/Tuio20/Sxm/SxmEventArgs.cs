using System;

namespace TuioUnity.Tuio20.Sxm
{
    public class SxmEventArgs : EventArgs
    {
        public string RoomId { get; set; }
        public string MqttUrl { get; set; }
    }
}