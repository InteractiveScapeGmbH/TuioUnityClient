using Tuio.Common;
using UnityEngine;

[CreateAssetMenu(menuName = "TUIO/TUIO Manager Settings")]
public class TuioManagerSettings : ScriptableObject
{
    [SerializeField] public TuioConnectionType tuioConnectionType = TuioConnectionType.Websocket;
    [SerializeField] public int udpPort = 3333;
    [SerializeField] public string websocketAddress = "10.0.0.20";
    [SerializeField] public int websocketPort = 3343;
    [SerializeField] public Vector2 scale = new Vector2(0.001f, 0.001f);
}
