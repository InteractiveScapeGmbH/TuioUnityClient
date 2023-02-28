using TuioNet.Common;
using TuioNet.Tuio20;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    public class Tuio20Manager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TuioManagerSettings _tuioManagerSettings;
        
        public Tuio20Client TuioClient { get; private set; }

        private bool _isInitialized;
        private static Tuio20Manager _instance;
        public static Tuio20Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Tuio20Manager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("TUIO 2.0 Manager").AddComponent<Tuio20Manager>();
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
                TuioClient = new Tuio20Client(_tuioManagerSettings.TuioConnectionType, address, port, false);
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

        public void AddTuio20Listener(ITuio20Listener listener)
        {
            TuioClient.AddTuioListener(listener);
        }

        public void RemoveTuio20Listener(ITuio20Listener listener)
        {
            TuioClient.RemoveTuioListener(listener);
        }

        public void Update()
        {
            TuioClient.ProcessMessages();
        }
    }
}
