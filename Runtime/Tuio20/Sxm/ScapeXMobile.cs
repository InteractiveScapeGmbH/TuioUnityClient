using System;
using OSC.NET;
using TuioNet.Common;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio20.Sxm
{
    /// <summary>
    /// This is a special component for touch tables made by Interactive Scape. It registers a message listener to get
    /// the configured Room Id and the url of the used MQTT Broker.
    /// </summary>
    [RequireComponent(typeof(TuioSession))]
    public class ScapeXMobile : MonoBehaviour
    {
        public SxmConfig Config { get; private set; }

        /// <summary>
        /// This event gets invoked everytime you change the Scape X Mobile configuration in the Touch & Object Assistant.
        /// </summary>
        public event Action<SxmConfig> OnConfigUpdate;
        
        private const string ScapeXMobileProfile = "/scape_x_mobile/def";

        private void Start()
        {
            var session = GetComponent<TuioSession>();
            var listener = new MessageListener(ScapeXMobileProfile, OnMessage);
            session.AddMessageListener(listener);
        }

        private void OnMessage(OSCMessage message)
        {
            var roomId = (string)message.Values[1];
            var mqttUrl = (string)message.Values[2];

            if (roomId == Config.RoomId && mqttUrl == Config.MqttUrl) return;
            Config = new SxmConfig(roomId, mqttUrl);
            OnConfigUpdate?.Invoke(Config);
        }
    }
}