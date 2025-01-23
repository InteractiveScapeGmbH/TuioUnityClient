using System;
using TuioNet.Common;
using TuioUnity.Utils;
using UnityEngine;

namespace TuioUnity.Common
{
    /// <summary>
    /// A Tuio-Session is responsible for the communication between the tuio sender and the unity application. It establishes
    /// a connection via UDP or Websocket depending on the given network settings and registers the appropriate callbacks on
    /// the events based on the used tuio version.
    /// </summary>
    public class TuioSessionBehaviour : MonoBehaviour
    {
        [field: SerializeField] public TuioVersion TuioVersion { get; set; } = TuioVersion.Tuio11;
        [field: SerializeField] public TuioConnectionType ConnectionType { get; set; } = TuioConnectionType.UDP;
        [SerializeField] private string _ipAddress = "10.0.0.20";
        [field: SerializeField] public int UdpPort { get; set; }= 3333;

        private TuioSession _session;
        private bool _isInitialized;
        private UnityLogger _logger;

        public ITuioDispatcher TuioDispatcher
        {
            get
            {
                if (_session == null)
                {
                    Initialize();
                }

                return _session.TuioDispatcher;
            }
        }

        public string IpAddress => _ipAddress;

        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            if (_isInitialized) return;
            var port = UdpPort;
            if (ConnectionType == TuioConnectionType.Websocket)
            {
                port = TuioVersion switch
                {
                    TuioVersion.Tuio11 => 3333,
                    TuioVersion.Tuio20 => 3343,
                    _ => throw new ArgumentOutOfRangeException($"{typeof(TuioVersion)} has no value of {TuioVersion}.")
                };
            }

            _logger = new UnityLogger();
            _session = new TuioSession(_logger, TuioVersion, ConnectionType, _ipAddress, port, false);
            _isInitialized = true;
        }

        public void AddMessageListener(MessageListener listener)
        {
            _session.AddMessageListener(listener);
        }

        public void RemoveMessageListener(string messageProfile)
        {
            _session.RemoveMessageListener(messageProfile);
        }

        public void RemoveMessageListener(MessageListener listener)
        {
            RemoveMessageListener(listener.MessageProfile);
        }

        private void Update()
        {
            _session.ProcessMessages();
        }

        private void OnApplicationQuit()
        {
            _session.Dispose();
        }

    }
}