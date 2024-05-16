using System;
using TuioNet.Common;
using TuioUnity.Tuio11;
using TuioUnity.Tuio20;
using UnityEngine;

namespace TuioUnity.Common
{
    public class TuioSession : MonoBehaviour
    {
        [SerializeField] private TuioVersion _tuioVersion = TuioVersion.Tuio11;
        [SerializeField] private TuioNetworkSettings _tuioNetworkSettings;

        private ITuioDispatcher _tuioDispatcher;

        internal ITuioDispatcher TuioDispatcher
        {
            get
            {
                if (_tuioDispatcher is null)
                {
                    _tuioDispatcher = _tuioVersion switch
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