using System;
using OSC.NET;
using TuioNet.Common;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio20.Sxm
{
    [RequireComponent(typeof(TuioSession))]
    public class ScapeXMobile : MonoBehaviour
    {
        public SxmConfig Config { get; private set; }

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