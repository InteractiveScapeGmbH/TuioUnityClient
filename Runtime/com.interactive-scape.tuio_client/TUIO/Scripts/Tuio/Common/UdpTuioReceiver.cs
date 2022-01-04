using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Tuio.Common
{
    public class UdpTuioReceiver : TuioReceiver
    {
        private int _port;

        public UdpTuioReceiver(int port, bool isAutoProcess) : base(isAutoProcess)
        {
            _port = port;
        }
        
        public override void Connect()
        {
            CancellationToken cancellationToken = _cancellationTokenSource.Token;
            Task.Run(async () =>
            {
                using (var udpClient = new UdpClient(_port))
                {
                    _isConnected = true;
                    while (true)
                    {
                        try
                        {
                            var receivedResults = await udpClient.ReceiveAsync();
                            OnBuffer(receivedResults.Buffer, receivedResults.Buffer.Length);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }
            });
            _isConnected = false;
        }
    }
}