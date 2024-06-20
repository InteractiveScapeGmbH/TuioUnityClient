using SxmMqttBridge;
using TuioNet.Common;
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
            if (_session.TuioVersion != TuioVersion.Tuio20)
            {
                Debug.LogWarning("Scape X Mobile only works with Tuio 2.0");
                return;
            }
            _bridge = new MqttBridge(_session.IpAddress);
        }

        private void OnApplicationQuit()
        {
            if(isActiveAndEnabled && _bridge != null)
                _bridge.Dispose();
        }
    }
}