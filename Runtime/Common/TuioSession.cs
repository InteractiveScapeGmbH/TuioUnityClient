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
        [field: SerializeField] public TuioVersion TuioVersion { get; set; } = TuioVersion.Tuio11;
        [field: SerializeField] public TuioConnectionType ConnectionType { get; set; } = TuioConnectionType.UDP;
        [SerializeField] private string _ipAddress = "10.0.0.20";
        [field: SerializeField] public int UdpPort { get; set; }= 3333;

        private ITuioDispatcher _tuioDispatcher;

        internal ITuioDispatcher TuioDispatcher
        {
            get
            {
                return _tuioDispatcher ??= TuioVersion switch
                {
                    TuioVersion.Tuio11 => new Tuio11Dispatcher(),
                    TuioVersion.Tuio20 => new Tuio20Dispatcher(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        private TuioClient _tuioClient;
        private bool _isInitialized;

        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            if (!_isInitialized)
            {
                int port = UdpPort;
                if (ConnectionType == TuioConnectionType.Websocket)
                {
                    port = TuioVersion switch
                    {
                        TuioVersion.Tuio11 => 3333,
                        TuioVersion.Tuio20 => 3343
                    };
                }

                _tuioClient = new TuioClient(ConnectionType, _ipAddress, port, false);
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