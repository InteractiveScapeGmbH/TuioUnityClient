using SxmMqttBridge;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio20.Sxm
{
    [RequireComponent(typeof(TuioSessionBehaviour))]
    public class ScapeXMobile : MonoBehaviour
    {
        private MqttBridge _bridge;
        private TuioSessionBehaviour _session;

        private void Start()
        {
            _session = GetComponent<TuioSessionBehaviour>();
            _bridge = new MqttBridge(_session.IpAddress);
        }

        private void OnApplicationQuit()
        {
            if(isActiveAndEnabled)
                _bridge.Dispose();
        }
    }
}