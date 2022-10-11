using Tuio.Common;
using Tuio.Tuio11;
using UnityEngine;

public class Tuio11Manager : MonoBehaviour
{
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
            switch (tuioManagerSettings.tuioConnectionType)
            {
                case TuioConnectionType.UDP:
                    _tuioReceiver = new UdpTuioReceiver(tuioManagerSettings.udpPort, false);
                    break;
                case TuioConnectionType.Websocket:
                    _tuioReceiver = new WebsocketTuioReceiver(tuioManagerSettings.websocketAddress, tuioManagerSettings.websocketPort, false);
                    break;
            }
            _tuio11Client = new Tuio11Client(_tuioReceiver);
            _tuio11Client.Connect();
            _isInitialized = true;
        }
    }

    public Tuio11Client tuio11Client => _tuio11Client;
    
    public Vector2 GetDimensions()
    {
        var height = 900;
        var width = 1600;
        return new Vector2(width * tuioManagerSettings.scale.x, height * tuioManagerSettings.scale.y);
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