namespace TuioUnity.Tuio20.Sxm
{
    /// <summary>
    /// Holds the configuration about Room Id and Mqtt Server Url set in Touch and Object Assistant.
    /// </summary>
    public struct SxmConfig
    {
        /// <summary>
        /// RoomId is a unique name of the multitouch table.
        /// </summary>
        public string RoomId { get; }
        
        /// <summary>
        /// The Url of the used MQTT broker.
        /// </summary>
        public string MqttUrl { get; }

        public SxmConfig(string roomId, string mqttUrl)
        {
            RoomId = roomId;
            MqttUrl = mqttUrl;
        }
    }
}