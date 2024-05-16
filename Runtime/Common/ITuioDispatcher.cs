using TuioNet.Common;

namespace TuioUnity.Common
{
    internal interface ITuioDispatcher
    {
        public void SetupProcessor(TuioClient tuioClient);
        public void RegisterCallbacks();
        public void UnregisterCallbacks();
    }
}