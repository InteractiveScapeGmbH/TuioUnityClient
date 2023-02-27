using TuioNet.Common;
using TuioNet.Tuio20;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    public class Tuio20Manager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TuioManagerSettings tuioManagerSettings;
        
        private bool _isInitialized;
        private Tuio20Client _tuio20Client;
        private TuioReceiver _tuioReceiver;

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
                if (tuioManagerSettings is null)
                {
                    tuioManagerSettings = ScriptableObject.CreateInstance<TuioManagerSettings>();
                }
                switch (tuioManagerSettings.TuioConnectionType)
                {
                    case TuioConnectionType.UDP:
                        _tuioReceiver = new UdpTuioReceiver(tuioManagerSettings.UdpPort, false);
                        break;
                    case TuioConnectionType.Websocket:
                        _tuioReceiver = new WebsocketTuioReceiver(tuioManagerSettings.WebsocketAddress, tuioManagerSettings.WebsocketPort, false);
                        break;
                }
                _tuio20Client = new Tuio20Client(_tuioReceiver);
                _tuio20Client.Connect();
                _isInitialized = true;
            }
        }
        
        public Tuio20Client tuio20Client => _tuio20Client;
        
        public Vector2 GetWorldPosition(Vector2 tuioPosition)
        {
            tuioPosition.y = (1 - tuioPosition.y);
            return _camera.ViewportToWorldPoint(tuioPosition);
        }

        public void AddTuio20Listener(Tuio20Listener listener)
        {
            _tuio20Client.AddTuioListener(listener);
        }

        public void RemoveTuio20Listener(Tuio20Listener listener)
        {
            _tuio20Client.RemoveTuioListener(listener);
        }

        public void Update()
        {
            _tuioReceiver.ProcessMessages();
        }
    }
}
