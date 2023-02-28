using TuioNet.Common;
using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11Manager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TuioManagerSettings _tuioManagerSettings;
        
        private bool _isInitialized;
        public Tuio11Client TuioClient { get; private set; }

        private static Tuio11Manager _instance;
        public static Tuio11Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Tuio11Manager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("TUIO 1.1 Manager").AddComponent<Tuio11Manager>();
                    }
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        public void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!_isInitialized)
            {
                if (_tuioManagerSettings is null)
                {
                    _tuioManagerSettings = ScriptableObject.CreateInstance<TuioManagerSettings>();
                }

                string address = "0.0.0.0";
                int port = 3333;
                switch (_tuioManagerSettings.TuioConnectionType)
                {
                    case TuioConnectionType.UDP:
                        port = _tuioManagerSettings.UdpPort;
                        break;
                    case TuioConnectionType.Websocket:
                        address = _tuioManagerSettings.WebsocketAddress;
                        port = _tuioManagerSettings.WebsocketPort;
                        break;
                }

                TuioClient = new Tuio11Client(_tuioManagerSettings.TuioConnectionType, address, port, false);
                TuioClient.Connect();
                _isInitialized = true;
            }
        }

        
        public Vector2 GetWorldPosition(Vector2 tuioPosition)
        {
            tuioPosition.y = (1 - tuioPosition.y);
            return _camera.ViewportToWorldPoint(tuioPosition);
        }
        
        public Vector2 GetScreenPosition(Vector2 tuioPosition)
        {
            tuioPosition.y = (1 - tuioPosition.y);
            return _camera.ViewportToScreenPoint(tuioPosition);
        }

        public void AddTuio11Listener(ITuio11Listener listener)
        {
            TuioClient.AddTuioListener(listener);
        }

        public void RemoveTuio11Listener(ITuio11Listener listener)
        {
            TuioClient.RemoveTuioListener(listener);
        }

        public void Update()
        {
            TuioClient.ProcessMessages();
        }

        private void OnApplicationQuit()
        {
            TuioClient.Disconnect();
        }
    }
}
