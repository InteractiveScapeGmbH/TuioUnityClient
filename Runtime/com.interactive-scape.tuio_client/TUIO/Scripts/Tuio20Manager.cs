using Tuio.Common;
using Tuio.Tuio20;
using UnityEngine;

public class Tuio20Manager : MonoBehaviour
{
    [SerializeField] private TuioConnectionType tuioConnectionType = TuioConnectionType.Websocket;
    [SerializeField] private int udpPort = 3333;
    [SerializeField] private string websocketAddress = "10.0.0.20";
    [SerializeField] private int websocketPort = 3343;
    [SerializeField] private Vector2 scale = new Vector2(0.001f, 0.001f);
    
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
                    _instance = new GameObject("Tuio 2.0 Manager").AddComponent<Tuio20Manager>();
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
            _tuio20Client = new Tuio20Client(_tuioReceiver);
            _tuio20Client.Connect();
            _isInitialized = true;
        }
    }

    public Vector2 GetDimensions()
    {
        var dim = _tuio20Client.dim;
        var height = dim >> 16;
        var width = dim & 0x0000FFFF;
        return new Vector2(width * scale.x, height * scale.y);
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