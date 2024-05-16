using System;
using TuioNet.Common;
using TuioUnity.Tuio11;
using TuioUnity.Tuio20;
using UnityEngine;

namespace TuioUnity.Common
{
    /// <summary>
    /// A Tuio-Session is responsible for the communication between the tuio sender and the unity application. It establishes
    /// a connection via UDP or Websocket depending on the given network settings and registers the appropriate callbacks on
    /// the events based on the used tuio version.
    /// </summary>
    public class TuioSession : MonoBehaviour
    {
        [field:SerializeField] public TuioVersion TuioVersion { get; set; } = TuioVersion.Tuio11;
        [SerializeField] private TuioNetworkSettings _tuioNetworkSettings;

        private ITuioDispatcher _tuioDispatcher;

        internal ITuioDispatcher TuioDispatcher
        {
            get
            {
                if (_tuioDispatcher is null)
                {
                    _tuioDispatcher = TuioVersion switch
                    {
                        TuioVersion.Tuio11 => new Tuio11Dispatcher(),
                        TuioVersion.Tuio20 => new Tuio20Dispatcher(),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                return _tuioDispatcher;
            }
        }
        
        private TuioClient _tuioClient;
        private bool _isInitialized;

        private void Awake()
        {
            Initialize(_tuioNetworkSettings);
        }
        
        private void Initialize(TuioNetworkSettings settings)
        {
            if (!_isInitialized)
            {
                if (settings is null)
                {
                    settings = ScriptableObject.CreateInstance<TuioNetworkSettings>();
                }

                var address = "0.0.0.0";
                var port = 3333;
                switch (settings.TuioConnectionType)
                {
                    case TuioConnectionType.UDP:
                        port = settings.UdpPort;
                        break;
                    case TuioConnectionType.Websocket:
                        address = settings.WebsocketAddress;
                        port = settings.WebsocketPort;
                        break;
                }

                _tuioClient = new TuioClient(settings.TuioConnectionType, address, port, false);
                TuioDispatcher.SetupProcessor(_tuioClient);
                _tuioClient.Connect();
                _isInitialized = true;
            }
        }

        private void OnEnable()
        {
            TuioDispatcher.RegisterCallbacks();
        }

        private void OnDisable()
        {
            TuioDispatcher.UnregisterCallbacks();
        }

        private void Update()
        {
            _tuioClient.ProcessMessages();
        }

        private void OnApplicationQuit()
        {
            _tuioClient.Disconnect();
        }

    }
}