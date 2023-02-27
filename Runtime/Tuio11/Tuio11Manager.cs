using System;
using TuioNet.Common;
using TuioNet.Tuio11;
using UnityEngine;

public class Tuio11Manager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TuioManagerSettings tuioManagerSettings;
    
    private bool _isInitialized;
    private Tuio11Client _tuio11Client;
    private TuioReceiver _tuioReceiver;

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
            _tuio11Client = new Tuio11Client(_tuioReceiver);
            _tuio11Client.Connect();
            _isInitialized = true;
        }
    }

    public Tuio11Client tuio11Client => _tuio11Client;
    
    public Vector2 GetWorldPosition(Vector2 tuioPosition)
    {
        tuioPosition.y = (1 - tuioPosition.y);
        return _camera.ViewportToWorldPoint(tuioPosition);
    }

    public void AddTuio11Listener(Tuio11Listener listener)
    {
        _tuio11Client.AddTuioListener(listener);
    }

    public void RemoveTuio11Listener(Tuio11Listener listener)
    {
        _tuio11Client.RemoveTuioListener(listener);
    }

    public void Update()
    {
        _tuioReceiver.ProcessMessages();
    }

    private void OnApplicationQuit()
    {
        _tuio11Client.Disconnect();
    }
}