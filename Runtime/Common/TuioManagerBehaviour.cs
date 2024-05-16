using System;
using TuioNet.Common;
using TuioUnity.Tuio11;
using TuioUnity.Tuio20;
using UnityEngine;

namespace TuioUnity.Common
{
    public class TuioManagerBehaviour : MonoBehaviour
    {
        [SerializeField] private TuioType _tuioType = TuioType.Tuio11;
        [SerializeField] private TuioManagerSettings _tuioManagerSettings;

        private ITuioManager _tuioManager;

        public ITuioManager TuioManager
        {
            get
            {
                if (_tuioManager is null)
                {
                    _tuioManager = _tuioType switch
                    {
                        TuioType.Tuio11 => new Tuio11Manager(),
                        TuioType.Tuio20 => new Tuio20Manager(),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                return _tuioManager;
            }
        }
        
        private TuioClient _tuioClient;
        private bool _isInitialized;

        private void Awake()
        {
            Initialize(_tuioManagerSettings);
        }
        
        private void Initialize(TuioManagerSettings settings)
        {
            if (!_isInitialized)
            {
                if (settings is null)
                {
                    settings = ScriptableObject.CreateInstance<TuioManagerSettings>();
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
                TuioManager.SetupProcessor(_tuioClient);
                _tuioClient.Connect();
                _isInitialized = true;
            }
        }

        private void OnEnable()
        {
            TuioManager.RegisterCallbacks();
        }

        private void OnDisable()
        {
            TuioManager.UnregisterCallbacks();
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