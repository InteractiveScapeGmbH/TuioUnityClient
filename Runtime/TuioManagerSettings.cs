using TuioNet.Common;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New TUIO Manager Settings", menuName = "TUIO/TUIO Manager Settings")]
public class TuioManagerSettings : ScriptableObject
{
   public TuioConnectionType TuioConnectionType = TuioConnectionType.Websocket;
    public int UdpPort = 3333;
    public string WebsocketAddress = "10.0.0.20";
    public int WebsocketPort = 3343;
}
