using TuioNet.Common;
using UnityEngine;

namespace TuioUnity.Common
{
    [CreateAssetMenu(fileName = "New TUIO Network Settings", menuName = "TUIO/TUIO Network Settings")]
    public class TuioNetworkSettings : ScriptableObject
    {
       public TuioConnectionType TuioConnectionType = TuioConnectionType.Websocket;
        public int UdpPort = 3333;
        public string WebsocketAddress = "10.0.0.20";
        public int WebsocketPort = 3343;
    }
}
