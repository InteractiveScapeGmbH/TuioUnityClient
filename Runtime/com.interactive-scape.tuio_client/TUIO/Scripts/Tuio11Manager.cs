using Tuio.Common;
using Tuio.Tuio11;
using UnityEngine;

public class Tuio11Manager : MonoBehaviour
{
    [SerializeField] private TuioConnectionType tuioConnectionType = TuioConnectionType.Websocket;
    [SerializeField] private int udpPort = 3333;
    [SerializeField] private string websocketAddress = "10.0.0.20";
    [SerializeField] private int websocketPort = 3343;
    [SerializeField] private Vector2 scale = new Vector2(0.001f, 0.001f);
    
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
                    _instance = new GameObject("Tuio 1.1 Manager").AddComponent<Tuio11Manager>();
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
            switch (tuioConnectionType)
            {
                case TuioConnectionType.UDP:
                    _tuioReceiver = new UdpTuioReceiver(udpPort, false);
                    break;
                case TuioConnectionType.Websocket:
                    _tuioReceiver = new WebsocketTuioReceiver(websocketAddress, websocketPort, false);
                    break;
            }
            _tuio11Client = new Tuio11Client(_tuioReceiver);
            _tuio11Client.Connect();
            _isInitialized = true;
        }
    }

    public Vector2 GetDimensions()
    {
        // var dim = _tuio11Client.dim;
        // var height = dim >> 16;
        // var width = dim & 0x0000FFFF;
        var height = 900;
        var width = 1600;
        return new Vector2(width * scale.x, height * scale.y);
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
}