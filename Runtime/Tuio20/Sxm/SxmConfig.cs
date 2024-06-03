namespace TuioUnity.Tuio20.Sxm
{
    public struct SxmConfig
    {
        public string RoomId { get; }
        public string MqttUrl { get; }

        public SxmConfig(string roomId, string mqttUrl)
        {
            RoomId = roomId;
            MqttUrl = mqttUrl;
        }
    }
}