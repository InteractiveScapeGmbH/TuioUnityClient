using System;
using SxmMqttBridge;
using TuioNet.Common;
using TuioUnity.Common;
using TuioUnity.Utils;
using UnityEngine;

namespace TuioUnity.Tuio20.Sxm
{
    [RequireComponent(typeof(TuioSessionBehaviour))]
    public class ScapeXMobile : MonoBehaviour
    {
        private MqttBridge _bridge;
        private TuioSessionBehaviour _session;

        public event EventHandler<SxmEventArgs> OnConfigUpdate;
        private UnityLogger _logger;

        private void Start()
        {
            _logger = new UnityLogger();
            _session = GetComponent<TuioSessionBehaviour>();
            if (_session.TuioVersion != TuioVersion.Tuio20)
            {
                Debug.LogWarning("[Tuio Client] Scape X Mobile only works with Tuio 2.0");
                return;
            }
            _bridge = new MqttBridge(_logger, _session.IpAddress);
            _bridge.OnConfigUpdate += UpdateConfig;
        }

        private void UpdateConfig(object sender, MqttConfig config)
        {
            var sxmConfig = new SxmEventArgs
            {
                RoomId = config.RoomId,
                MqttUrl = config.MqttUrl.Authority
            };
            OnConfigUpdate?.Invoke(this, sxmConfig);
        }

        private void OnApplicationQuit()
        {
            if(isActiveAndEnabled && _bridge != null)
                _bridge.Dispose();
        }
    }
}